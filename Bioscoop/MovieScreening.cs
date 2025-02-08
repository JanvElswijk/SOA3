using System.Text.Json.Serialization;

namespace Bioscoop;

public class MovieScreening(Movie movie, DateTime dateAndTime, double pricePerSeat) {
    
    [JsonInclude]
    [JsonPropertyName("dateAndTime")]
    private DateTime _dateAndTime = dateAndTime;
    [JsonInclude]
    [JsonPropertyName("pricePerSeat")]
    private double _pricePerSeat = pricePerSeat;
    [JsonInclude]
    [JsonPropertyName("movie")]
    private Movie _movie = movie;

    public double GetPricePerSeat() {
        return _pricePerSeat;
    }
    
    public DateTime getDateAndTime() {
        return _dateAndTime;
    }

    public override String ToString() {
        // return "Movie: " + this._movie + " Date and time: " + this._dateAndTime + " Price per seat: " + this._pricePerSeat;
        
        return this._movie + " Date and time: " + this._dateAndTime + " Price per seat: " + this._pricePerSeat;
    }
}