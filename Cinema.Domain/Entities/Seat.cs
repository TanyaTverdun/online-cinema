using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities;

public class Seat
{
    public int SeatId { get; set; }
    public int HallId { get; set; }

    public byte RowNumber { get; set; }
    public byte SeatNumber { get; set; }
    public SeatType Type { get; set; }
    public float Coefficient { get; set; }

    public Hall Hall { get; set; } = null!;

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
