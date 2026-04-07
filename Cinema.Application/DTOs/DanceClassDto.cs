namespace onlineCinema.Application.DTOs;

public class DanceClassDto // Було SessionDto
{
    public int Id { get; set; }

    public int PerformanceId { get; set; } // Було MovieId
    public string PerformanceTitle { get; set; } = string.Empty; // Було MovieTitle

    public int HallId { get; set; }
    public string HallName { get; set; } = string.Empty; // Замість HallNumber краще HallName (наприклад, "Золотий зал")

    public DateTime StartDateTime { get; set; } // Було ShowingDateTime
    public decimal BasePrice { get; set; }
}