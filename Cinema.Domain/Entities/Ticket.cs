namespace onlineCinema.Domain.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public decimal Price { get; set; }

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
}