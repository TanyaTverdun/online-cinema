namespace onlineCinema.Application.DTOs;

public class DanceClassEditDto // Було SessionEditDto
{
    public int Id { get; set; }
    public int PerformanceId { get; set; } // Було MovieId
    public int HallId { get; set; }
    public DateTime StartDateTime { get; set; } // Було ShowingDateTime
    public decimal BasePrice { get; set; }
}