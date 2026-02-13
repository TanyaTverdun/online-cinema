namespace onlineCinema.Areas.Admin.Models
{
    public class AdminStatisticsViewModel
    {
        //public int TotalTicketsSold { get; set; }
        //public decimal TotalRevenue { get; set; }

        //public List<string> SnackLabels { get; set; } = new();
        //public List<decimal> SnackRevenueData { get; set; } = new();

        //public List<string> MovieLabels { get; set; } = new();
        //public List<decimal> MovieRevenueData { get; set; } = new();

        //public List<string> DaysLabels { get; set; } = new();
        //public List<decimal> DailyRevenueData { get; set; } = new();

        // заповненість залів (назва + інфа)
        public List<string> OccupancyLabels { get; set; } = new();
        public List<double> OccupancyData { get; set; } = new();

        // найпопулярніші фільми (назва + інфа)
        public List<string> PopularMoviesLabels { get; set; } = new();
        public List<int> PopularMoviesData { get; set; } = new();

        // найменш популярні фільми (назва + інфа)
        public List<string> LeastMoviesLabels { get; set; } = new();
        public List<int> LeastMoviesData { get; set; } = new();

        // найменш популярні снеки (назва + інфа)
        public List<string> LeastSnacksLabels { get; set; } = new();
        public List<int> LeastSnacksData { get; set; } = new();
    }
}
