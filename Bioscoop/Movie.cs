namespace Bioscoop;

public class Movie {
    private String _title;

    public Movie(String title) {
        this._title = title;
    }

    public void AddScreening(MovieScreening movieScreening) {
        //TODO: Implement
        //new MovieScreening(this, LocalDateTime.now, 1.1)
    }

    public override String ToString() {
        return this._title;
    }
}