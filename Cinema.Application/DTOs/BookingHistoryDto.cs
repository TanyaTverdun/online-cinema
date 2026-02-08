namespace onlineCinema.Application.DTOs
{
    public class BookingHistoryDto
    {
        public int BookingId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime SessionDateTime { get; set; }
        public byte[]? MoviePoster { get; set; }
        public string HallName { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public List<TicketInfoDto> Tickets { get; set; } = new();

        public bool CanRefund { get; set; }
    }
}

