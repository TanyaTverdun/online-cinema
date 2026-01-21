namespace onlineCinema.Domain.Entities;

public class MovieCast
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public int CastId { get; set; }
    public CastMember CastMember { get; set; } = null!;
}