using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int? BookingId { get; set; }
    public Booking? Booking { get; set; }
    public PaymentStatus Status { get; set; }
}


