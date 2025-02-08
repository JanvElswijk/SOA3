using Bioscoop;

public class OrderTest
{
    [Fact]
    public void CalculatePrice_NotStudentOrder_NotWeekend_FullPrice()
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
        bool isWeekend = day is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday;

        // Assert
        Assert.Equal(20, price);
        Assert.False(isWeekend);
    }
}