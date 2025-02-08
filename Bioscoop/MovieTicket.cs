using System.Text.Json.Serialization;

namespace Bioscoop;

public class MovieTicket {
    [JsonInclude]
    [JsonPropertyName("rowNr")]
    private int _rowNr;
    [JsonInclude]
    [JsonPropertyName("seatNr")]
    private int _seatNr;
    [JsonInclude]
    [JsonPropertyName("isPremium")]
    private bool _isPremium;
    [JsonInclude]
    [JsonPropertyName("screening")]
    private MovieScreening _screening;

    public MovieTicket(MovieScreening screening, bool isPremiumReservation, int seatRow, int seatNr) {
        this._screening = screening;
        this._isPremium = isPremiumReservation;
        this._rowNr = seatRow;
        this._seatNr = seatNr;
    }

    public bool IsPremiumTicket() {
        return _isPremium;
    }

    public DateTime getDateAndTime() {
        return this._screening.getDateAndTime();
    }

    public double GetPrice() {
        //Premium Ticket
        if (this._isPremium) {
            return this._screening.GetPricePerSeat() + 3;
        } else {
            return this._screening.GetPricePerSeat();
        }
    }
    
    public override String ToString() {
        // return "Movie: " + this._screening.ToString() + " Seat: " + this._rowNr +"/"+ this._seatNr + " Premium: " + this._isPremium;
        
        return "Movie: " + this._screening.ToString() + " Seat: " + this._rowNr +"/"+ this._seatNr + " Premium: " + this._isPremium;
    }
}