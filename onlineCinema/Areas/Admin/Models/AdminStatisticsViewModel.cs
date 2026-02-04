namespace onlineCinema.Areas.Admin.Models
{
    public class AdminStatisticsViewModel
    {
        public int TotalTicketsSold { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<string> SnackLabels { get; set; } = new();
        public List<decimal> SnackRevenueData { get; set; } = new();

        public List<string> MovieLabels { get; set; } = new();
        public List<decimal> MovieRevenueData { get; set; } = new();

        public List<string> DaysLabels { get; set; } = new();
        public List<decimal> DailyRevenueData { get; set; } = new();
    }
}
