using onlineCinema.Domain.Enums;

namespace onlineCinema.ViewModels
{
    public class SeatViewModel
    {
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public SeatType Type { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }

        public string CssClass => IsBooked ? "seat-booked" : (Type == SeatType.VIP ? "seat-vip" : "");
    }
}
