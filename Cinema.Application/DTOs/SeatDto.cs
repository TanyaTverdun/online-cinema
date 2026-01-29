using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public SeatType Type { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }
    }
}
