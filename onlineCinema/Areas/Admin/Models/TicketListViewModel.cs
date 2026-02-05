namespace onlineCinema.Areas.Admin.Models
{
    public class TicketListViewModel
    {
        public List<TicketAdminViewModel> Tickets { get; set; } = new();
        public int? LastId { get; set; }
        public int? NextId { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalCount { get; set; }

        // Поля для пошуку
        public string? SearchEmail { get; set; }
        public string? SearchMovie { get; set; }
        public DateTime? SearchDate { get; set; }
    }
}
