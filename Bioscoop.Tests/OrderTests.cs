using System.Transactions;
using Bioscoop;

public class OrderTest
{
    //Create MovieScreening
    private static MovieScreening CreateScreening(DateTime date, double price) =>
        new(new Movie("The Matrix"), date, price);

    //Create Order
    private static Order CreateOrder(
        MovieScreening screening,
        bool isStudentOrder,
        MovieTicket[] tickets
    )
    {
        var order = new Order(1, isStudentOrder);
        foreach (var ticket in tickets)
        {
            order.AddSeatReservation(ticket);
        }
        return order;
    }

    private static bool IsWeekday(MovieTicket ticket)
    {
        DayOfWeek day = ticket.getDateAndTime().DayOfWeek;
        return day
            is DayOfWeek.Monday
                or DayOfWeek.Tuesday
                or DayOfWeek.Wednesday
                or DayOfWeek.Thursday;
    }

[Theory]
[InlineData("2023-10-07T20:00:00", 10, false, new[] { false, false }, 20, false)] // Weekend, full price
[InlineData("2023-10-07T20:00:00", 10, true, new[] { false, false }, 10, false)] // Weekend, Student, second free
[InlineData("2023-10-10T20:00:00", 10, false, new[] { false, false }, 10, true)]  // Weekday, second free
[InlineData("2023-10-07T20:00:00", 10, false, new[] { false, false, false, false, false, false }, 54, false)] // Weekend, group discount
[InlineData("2023-10-07T20:00:00", 10, true, new[] { false, false, false, false, false, false }, 27, false)] // Weekend, Student, group discount
[InlineData("2023-10-07T20:00:00", 10, false, new[] { true, false }, 23, false)]  // 1 premium ticket (+3 extra)
[InlineData("2023-10-07T20:00:00", 10, true, new[] { true, false}, 12, false)]  // 1 premium ticket (+2 extra), Student
[InlineData("2023-10-10T20:00:00", 10, false, new[] { false, true}, 13, true)]  // Weekday, second free, 1 premium ticket (+3 extra)
[InlineData("2023-10-07T20:00:00", 10, false, new[] { true, true, true, false, false, false }, 62.1, false)] // Group discount, 3 premium tickets (+9 extra)
[InlineData("2023-10-07T20:00:00", 10, true, new[] { true, true, true, false, false, false }, 29.7, false)] // Group discount, Student, 3 premium tickets (+6 extra)
    public void CalculatePrice_NotStudentOrder_CorrectPrice(
        string date,
        double pricePerTicket,
        bool isStudentOrder,
        bool[] isPremiumArray,
        double expectedPrice,
        bool expectedIsWeekday
    )
    {
        // Arrange
        var screening = CreateScreening(DateTime.Parse(date), pricePerTicket);

        // Convert bool[] to MovieTicket[]
        var tickets = isPremiumArray
            .Select((isPremium, index) => new MovieTicket(screening, isPremium, 1, index + 1))
            .ToArray();

        var order = CreateOrder(screening, isStudentOrder, tickets);

        // Act
        double actualPrice = order.CalculatePrice();
        bool isWeekday = IsWeekday(tickets[0]);

        // Assert
        // Assert with a tolerance of 0.01 to handle floating-point precision issues
        Assert.Equal(expectedPrice, actualPrice, 2);
        Assert.Equal(expectedIsWeekday, isWeekday);
    }
}

// •	Elk 2e ticket is gratis voor studenten (elke dag van de week) of als het een voorstelling betreft op een doordeweekse dag (ma/di/wo/do) voor iedereen.
// •	In het weekend betaal je als niet-student de volle prijs, tenzij de bestelling uit 6 kaartjes of meer bestaat, dan krijg je 10% groepskorting.
// •	Een premium ticket is voor studenten 2,- duurder dan de standaardprijs per stoel van de voorstelling, voor niet-studenten 3,-. Deze worden in de kortingen verrekend (dus bij een 2e ticket dat gratis is, ook geen extra kosten; bij 10% korting ook 10% van de extra kosten).
// •	Om de casus niet nog complexer te maken, gaan we ervan uit dat bij een studenten-order alle tickets voor studenten zijn; vandaar het isStudentOrder attribuut in de Order klasse en niet in MovieTicket.
