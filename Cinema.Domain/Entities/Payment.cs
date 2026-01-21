using onlineCinema.Domain.Entities;

namespace OnlineCinema.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public byte Status { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
