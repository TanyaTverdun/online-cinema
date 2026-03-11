namespace onlineCinema.Application.DTOs.Booking
{
    public class CreateBookingDto
    {
        public int SessionId { get; set; }
        public List<int> SeatIds { get; set; } = [];
        public string UserId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime? UserDateOfBirth { get; set; }
    }
}
