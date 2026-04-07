namespace onlineCinema.Application.DTOs;

public class PerformanceDto // Було MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Назва виступу або курсу

    // Список ID особливостей або вимог до виступу
    public List<int> FeatureIds { get; set; } = new();
}