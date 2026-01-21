namespace onlineCinema.Domain.Entities;

public class Cinema
{
    public int CinemaId { get; set; }

    public string CinemaName { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public int Building { get; set; }

    public TimeSpan TimeOpen { get; set; }

    public TimeSpan TimeClose { get; set; }
    public ICollection<Hall> Halls { get; set; } = new List<Hall>();
}