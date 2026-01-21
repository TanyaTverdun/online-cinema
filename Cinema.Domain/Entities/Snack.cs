using onlineCinema.Domain.Entities;

namespace OnlineCinema.Domain.Entities;

public class Snack
{
    public int SnackId { get; set; }
    public string SnackName { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ICollection<SnackBooking> SnackBookings { get; set; } = new List<SnackBooking>();
}
