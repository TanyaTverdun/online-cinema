namespace onlineCinema.Areas.Admin.Models
{
    public class PaymentListViewModel
    {
        private const string DefaultTitle = "Журнал платежів";

        public List<PaymentAdminViewModel> Payments { get; set; } = new();
        public int? LastId { get; set; }
        public int? NextId { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalCount { get; set; }

        public string? SearchEmail { get; set; }
        public string? SearchMovie { get; set; }
        public DateTime? SearchDate { get; set; }

        public string Title { get; set; } = DefaultTitle;
        public string? SuccessMessage { get; set; }
        public bool HasFilters => !string.IsNullOrEmpty(SearchEmail) ||
                          !string.IsNullOrEmpty(SearchMovie) ||
                          SearchDate.HasValue;
    }
}
