using onlineCinema.Application.DTOs.Hall;

namespace onlineCinema.Application.DTOs.Session
{
    public class SessionSeatMapDto
    {
        public int SessionId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int HallNumber { get; set; }
        public DateTime ShowingDate { get; set; }
        public decimal BasePrice { get; set; }
        public List<SeatDto> Seats { get; set; } = new();
    }
}
