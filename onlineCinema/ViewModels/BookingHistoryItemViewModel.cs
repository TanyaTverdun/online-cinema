namespace onlineCinema.ViewModels
{
    public class BookingHistoryItemViewModel
    {
        public int BookingId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime SessionDateTime { get; set; }
        public string? MoviePoster { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public List<TicketInfoViewModel> Tickets { get; set; } = new();

        public List<SnackInfoViewModel> Snacks { get; set; } = new();
        public bool CanRefund { get; set; }

    }
}

