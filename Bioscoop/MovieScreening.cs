namespace Bioscoop;

public class MovieScreening(Movie movie, DateTime dateAndTime, double pricePerSeat) {

    private DateTime _dateAndTime = dateAndTime;
    private double _pricePerSeat = pricePerSeat;
    private Movie _movie = movie;

    public double GetPricePerSeat() {
        return _pricePerSeat;
    }
    
    public DateTime getDateAndTime() {
        return _dateAndTime;
    }

    public override String ToString() {
        return "Movie: " + this._movie + " Date and time: " + this._dateAndTime + " Price per seat: " + this._pricePerSeat;
    }
}