namespace onlineCinema.Areas.Admin.Models
{
    public class TicketAdminViewModel
    {
        public int TicketId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string ShowingDateTimeFormatted { get; set; } = string.Empty;
        public string SeatInfo { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string PurchaseDateFormatted { get; set; } = string.Empty;
    }
}
