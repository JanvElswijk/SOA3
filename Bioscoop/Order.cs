using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bioscoop;

public class Order
{
    [JsonInclude]
    [JsonPropertyName("orderNr")]
    private int _orderNr;

    [JsonInclude]
    [JsonPropertyName("isStudentOrder")]
    private bool _isStudentOrder;

    [JsonInclude]
    [JsonPropertyName("tickets")]
    private ICollection<MovieTicket> _tickets;

    public Order(int orderNr, bool isStudentOrder)
    {
        this._orderNr = orderNr;
        this._isStudentOrder = isStudentOrder;
        this._tickets = new List<MovieTicket>();
    }

    public int GetOrderNr()
    {
        return this._orderNr;
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        _tickets.Add(ticket);
    }

    public double CalculatePrice()
    {
        // 2nd ticket free for students always, 2nd ticket free for everyone else on monday through thursday
        // Group discount: 10% off for groups of 6 or more on weekends
        // Premium tickets are 2 euro more expensive for students and 3 euro more expensive for everyone else

        double totalPrice = 0;
        DayOfWeek day = _tickets.First().getDateAndTime().DayOfWeek;
        bool isWeekend = day is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday;

        // Calculate total price of all tickets
        foreach (MovieTicket ticket in _tickets)
        {
            double ticketPrice = ticket.GetPrice();
            if (_isStudentOrder && ticket.IsPremiumTicket())
            {
                ticketPrice -= 1;
            }
            totalPrice += ticketPrice;
        }

        // 2nd ticket free calculation
        if (_tickets.Count >= 2 && (_isStudentOrder || !isWeekend))
        {
            double discount = _tickets
                // Order by price
                .OrderBy(t => t.GetPrice())
                // Take the first half of the tickets
                .Take(_tickets.Count / 2)
                // Sum the prices
                .Sum(t => t.GetPrice());
            totalPrice -= discount;
        }

        // Group discount for everyone
        if (isWeekend && _tickets.Count >= 6)
        {
            totalPrice *= 0.9;
        }

        return totalPrice;
    }

    public override String ToString()
    {
        return "Order number: "
            + this._orderNr
            + " Student: "
            + this._isStudentOrder
            + " Tickets: "
            + string.Join(", ", _tickets);
    }

    public void Export(TicketExportFormat format)
    {
        string fileName = "order" + _orderNr;

        switch (format)
        {
            case TicketExportFormat.JSON:
                fileName += ".json";
                string jsonString = JsonSerializer.Serialize(
                    this,
                    new JsonSerializerOptions { WriteIndented = true }
                );
                File.WriteAllText(fileName, jsonString);
                break;
            case TicketExportFormat.PLAINTEXT:
                fileName += ".txt";
                File.WriteAllText(fileName, this.ToString());
                break;
            default:
                throw new Exception("Invalid export format");
        }
    }
}
