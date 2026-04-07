namespace onlineCinema.Application.DTOs.AdminStatistics;

public class PerformanceOccupancyDto // Було MovieOccupancyDto
{
    public string PerformanceTitle { get; set; } = string.Empty; // Було MovieTitle
    public double OccupancyPercentage { get; set; }
}