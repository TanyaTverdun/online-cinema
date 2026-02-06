namespace onlineCinema.Areas.Admin.Models
{
    public class PaymentAdminViewModel
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string FormattedDate { get; set; } = string.Empty;
        public string FormattedSessionDate { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public int TicketCount { get; set; }
        public bool IsRefundable { get; set; }

        public List<string> TicketsDetail { get; set; } = new();
        public List<string> SnacksDetail { get; set; } = new();
    }
}
