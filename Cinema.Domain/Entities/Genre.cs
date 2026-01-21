namespace onlineCinema.Domain.Entities;

public class Genre
{
    public int GenreId { get; set; }
    public string GenreName { get; set; } = string.Empty;
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}