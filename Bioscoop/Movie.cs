namespace Bioscoop;

public class Movie {
    private String _title;
    private ICollection<MovieScreening> _screenings;

    public Movie(String title) {
        this._title = title;
        this._screenings = new List<MovieScreening>();
    }

    public void AddScreening(MovieScreening movieScreening) {
        this._screenings.Add(movieScreening); }

    public override String ToString() {
        return this._title;
    }
}