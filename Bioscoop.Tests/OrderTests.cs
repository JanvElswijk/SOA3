using Bioscoop;

public class OrderTest
{

    [Fact]
    public void CalculatePrice_NotStudentOrder_NotWeekday_FullPrice()
    {
        // Arrange
        Movie movie = new("The Matrix");
        MovieScreening screening = new(movie, new DateTime(2023, 10, 7, 20, 0, 0), 10);

        MovieTicket ticket1 = new(screening, false, 1, 1);
        MovieTicket ticket2 = new(screening, false, 1, 2);

        Order order = new(1, false);

        order.AddSeatReservation(ticket1);
        order.AddSeatReservation(ticket2);

        // Act
        double price = order.CalculatePrice();
        DayOfWeek day = ticket1.getDateAndTime().DayOfWeek;
        bool isWeekday = day is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday;

        // Assert
        Assert.Equal(20, price);
        Assert.False(isWeekday);
    }

    [Fact]
    public void CalculatePrice_NotStudentOrder_IsWeekday_SecondFree()
    {
      // Arrange
        Movie movie = new("The Matrix");
        MovieScreening screening = new(movie, new DateTime(2023, 10, 10, 20, 0, 0), 10);
        
        MovieTicket ticket1 = new(screening, false, 1, 1);
        MovieTicket ticket2 = new(screening, false, 1, 2);

        Order order = new(1, false);

        order.AddSeatReservation(ticket1);
        order.AddSeatReservation(ticket2);

        // Act
        double price = order.CalculatePrice();
        DayOfWeek day = ticket1.getDateAndTime().DayOfWeek;
        bool isWeekday = day is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday;


        // Assert
        Assert.Equal(10, price);
        Assert.True(isWeekday);
    }

    [Fact]
    public void CalculatePrice_NotStudentOrder_IsWeekend_GroupDiscount(){
        // Arrange
        Movie movie = new("The Matrix");
        MovieScreening screening = new(movie, new DateTime(2023, 10, 7, 20, 0, 0), 10);

        MovieTicket ticket1 = new(screening, false, 1, 1);
        MovieTicket ticket2 = new(screening, false, 1, 2);
        MovieTicket ticket3 = new(screening, false, 1, 3);
        MovieTicket ticket4 = new(screening, false, 1, 4);
        MovieTicket ticket5 = new(screening, false, 1, 5);
        MovieTicket ticket6 = new(screening, false, 1, 6);

        Order order = new(1, false);

        order.AddSeatReservation(ticket1);
        order.AddSeatReservation(ticket2);
        order.AddSeatReservation(ticket3);
        order.AddSeatReservation(ticket4);
        order.AddSeatReservation(ticket5);
        order.AddSeatReservation(ticket6);

        // Act
        double price = order.CalculatePrice();
        DayOfWeek day = ticket1.getDateAndTime().DayOfWeek;
        bool isWeekday = day is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday;


        // Assert
        Assert.Equal(54, price);
        Assert.False(isWeekday);
    }

    //TODO:
    // Use theory?
    // Mocking or something idk
    // NotStudentOrder IsWeekend NoGroupDiscount
    // NotStudentOrder Premium Ticket
    // StudentOrder Premium Ticket
}

// •	Elk 2e ticket is gratis voor studenten (elke dag van de week) of als het een voorstelling betreft op een doordeweekse dag (ma/di/wo/do) voor iedereen.
// •	In het weekend betaal je als niet-student de volle prijs, tenzij de bestelling uit 6 kaartjes of meer bestaat, dan krijg je 10% groepskorting.
// •	Een premium ticket is voor studenten 2,- duurder dan de standaardprijs per stoel van de voorstelling, voor niet-studenten 3,-. Deze worden in de kortingen verrekend (dus bij een 2e ticket dat gratis is, ook geen extra kosten; bij 10% korting ook 10% van de extra kosten).
// •	Om de casus niet nog complexer te maken, gaan we ervan uit dat bij een studenten-order alle tickets voor studenten zijn; vandaar het isStudentOrder attribuut in de Order klasse en niet in MovieTicket.
