namespace Bioscoop;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello World");
        Movie movie = new("Test");
        MovieScreening screening = new(movie, new DateTime(2023, 10, 10, 20, 0, 0), 10);

        MovieTicket ticket1 = new(screening, false, 1, 1);
        MovieTicket ticket2 = new(screening, false, 1, 2);

        Order order = new(1, false);
        order.AddSeatReservation(ticket1);
        order.AddSeatReservation(ticket2);
        Console.WriteLine(order.CalculatePrice());

        //TODO: tests & CICD pipeline to run SonarCloud
    }
}
