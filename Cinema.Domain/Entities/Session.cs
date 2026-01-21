namespace onlineCinema.Domain.Entities;

public class Session
{
    public int SessionId { get; set; }
    public int MovieId { get; set; }
    public int HallId { get; set; }

    public DateTime ShowingDateTime { get; set; }
    public decimal BasePrice { get; set; }

    public Movie Movie { get; set; } = null!;
    public Hall Hall { get; set; } = null!;
}
