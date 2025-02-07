using System;

namespace Bioscoop;

public class Program
{
    public static void Main()
    {
        Order order = new Order(1, false);
        Movie movie = new Movie("The Matrix");
        MovieScreening screening = new MovieScreening(movie, new DateTime(2021, 10, 7, 20, 0, 0), 10);
        MovieTicket ticket = new MovieTicket(screening, true, 1, 1);
        MovieTicket ticket2 = new MovieTicket(screening, true, 1, 1);

        order.AddSeatReservation(ticket);
        order.AddSeatReservation(ticket2);
        Console.WriteLine(ticket.getDateAndTime().DayOfWeek);
        Console.WriteLine(order.CalculatePrice());

    }
}
