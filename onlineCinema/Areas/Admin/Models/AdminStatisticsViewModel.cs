namespace onlineCinema.Areas.Admin.Models
{
    public class AdminStatisticsViewModel
    {
        public List<string> OccupancyLabels { get; set; } = new();
        public List<double> OccupancyData { get; set; } = new();

        public List<string> PopularMoviesLabels { get; set; } = new();
        public List<int> PopularMoviesData { get; set; } = new();

        public List<string> LeastMoviesLabels { get; set; } = new();
        public List<int> LeastMoviesData { get; set; } = new();

        public List<string> LeastSnacksLabels { get; set; } = new();
        public List<int> LeastSnacksData { get; set; } = new();
    }
}
