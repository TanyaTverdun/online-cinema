using onlineCinema.Domain.Entities;

namespace OnlineCinema.Domain.Entities;

public class SnackBooking
{
    public int SnackId { get; set; }
    public Snack Snack { get; set; } = null!;

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
}
