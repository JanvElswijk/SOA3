namespace Bioscoop;

public class Order {

    private int _orderNr;
    private bool _isStudentOrder;
    private ICollection<MovieTicket> _tickets;

    public Order(int orderNr, bool isStudentOrder) {
        this._orderNr = orderNr;
        this._isStudentOrder = isStudentOrder;
        this._tickets = new List<MovieTicket>();
    }

    public int GetOrderNr() {
        return this._orderNr;
    }

    public void AddSeatReservation(MovieTicket ticket) {
        _tickets.Add(ticket);
    }

    public double CalculatePrice() {
        double totalPrice = 0;
        //Calculate ticket cost by premium
        foreach (MovieTicket ticket in _tickets) {
            if (_isStudentOrder && ticket.IsPremiumTicket()) {
                totalPrice += ticket.GetPrice() - 1;
            } else {
                totalPrice += ticket.GetPrice();
            }

      

        }

        //Group discount
        if (_tickets.Count >= 6) {
            return totalPrice * 0.9;
        }

        DayOfWeek day = _tickets.First().getDateAndTime().DayOfWeek;
        //if isStudentOrder, second ticket free && if isWeekday, second ticket free
        if (_isStudentOrder || (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday && day != DayOfWeek.Friday) ) {
            return (totalPrice / (double) _tickets.Count * ((double) _tickets.Count/2 - (_tickets.Count % 2) * 0.5));
        }
        return totalPrice;

    }
    
    public override String ToString() {
        return "Order number: " + this._orderNr + " Student: " + this._isStudentOrder + " Tickets: " + this._tickets;
    }

    public void Export(TicketExportFormat format) {

    }
}