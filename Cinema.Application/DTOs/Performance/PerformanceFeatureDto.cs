namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class PerformanceFeatureDto // Було FeatureDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Назва (наприклад, "Дзеркальний зал")
    public string Description { get; set; } = string.Empty; // Опис переваги
}