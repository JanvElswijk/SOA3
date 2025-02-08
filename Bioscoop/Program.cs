namespace Bioscoop;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello World");
        Movie movie = new("Test");
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
        Console.WriteLine(order.CalculatePrice());
        Console.WriteLine(screening.getDateAndTime().DayOfWeek);

        //TODO: tests & CICD pipeline to run SonarCloud
    }
}
