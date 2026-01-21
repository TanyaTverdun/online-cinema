namespace onlineCinema.Domain.Entities;

public class DirectorMovie
{
    public int DirectorId { get; set; }
    public Director Director { get; set; } = null!;
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}