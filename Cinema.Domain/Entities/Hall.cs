namespace onlineCinema.Domain.Entities;

public class Hall
{
    public int HallId { get; set; }
    public int CinemaId { get; set; }

    public byte HallNumber { get; set; }
    public byte RowCount { get; set; }
    public byte SeatInRowCount { get; set; }

    public Cinema Cinema { get; set; } = null!;
    public ICollection<Seat> Seats { get; set; } = [];
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<HallFeature> HallFeatures { get; set; } = [];
}
