namespace Bioscoop;

public class Program
{
    public static void Main()
    {
        Movie movie = new Movie("The Matrix");
        MovieScreening screening = new MovieScreening(movie, DateTime.Now, 10.0);
        MovieTicket ticket = new MovieTicket(screening, true, 1, 1);
        Console.WriteLine(ticket.ToString());
        Console.WriteLine(ticket.GetPrice());
        
        Order order = new Order(1, true);
        order.AddSeatReservation(ticket);
        Console.WriteLine(order.CalculatePrice());
        
        order.Export(TicketExportFormat.JSON);
        order.Export(TicketExportFormat.PLAINTEXT);
    }
}
