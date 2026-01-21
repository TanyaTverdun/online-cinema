using OnlineCinema.Domain.Entities;

namespace onlineCinema.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string EmailAddress { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }

        
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<SnackBooking> SnackBookings { get; set; } = new List<SnackBooking>();
    }
}
