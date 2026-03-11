namespace onlineCinema.Application.DTOs.AdminStatistics
{
    public class MovieOccupancyDto
    {
        public string MovieTitle { get; set; } = string.Empty;
        public double OccupancyPercentage { get; set; }
    }
}
