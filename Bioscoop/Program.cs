namespace Bioscoop;

public class Program
{
    public static void Main()
    {
        Order order = new Order(1, true);
        Movie movie = new Movie("The Matrix");
        MovieScreening screening = new MovieScreening(movie, new DateTime(2021, 10, 10, 20, 0, 0), 10);
        MovieTicket ticket = new MovieTicket(screening, true, 1, 1);
        order.AddSeatReservation(ticket);
        Console.WriteLine(ticket.getDateAndTime());
        Console.WriteLine(order.CalculatePrice());

    }
}
