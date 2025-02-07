using System.Text.Json.Serialization;

namespace Bioscoop;

public class Movie {
    [JsonInclude]
    [JsonPropertyName("title")]
    private String _title;
    [JsonIgnore]
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