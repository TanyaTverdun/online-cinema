namespace onlineCinema.Domain.Entities;

public class Hall
{
    public int HallId { get; set; }
    public int CinemaId { get; set; }

    public byte HallNumber { get; set; }
    public byte RowCount { get; set; }
    public byte SeatInRowCount { get; set; }
    public int VipRowCount { get; set; }
    public float VipCoefficient { get; set; }

    public Cinema Cinema { get; set; } = null!;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<HallFeature> HallFeatures { get; set; } = new List<HallFeature>();
}
