using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities;

public class Performance
{
    // Первинний ключ
    public int PerformanceId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Перерахування (Enums)
    public PerformanceStatus Status { get; set; }
    public AgeCategory AgeCategory { get; set; }

    // Характеристики
    public int Duration { get; set; } // Тривалість у хвилинах
    public DateTime StartDate { get; set; }

    // Медіа-контент
    public string? VideoLink { get; set; }
    public string? CoverImage { get; set; }

    // Зв'язки (Навігаційні властивості)
    // Використовуємо virtual для Lazy Loading
    public virtual ICollection<PerformanceStyle> PerformanceStyles { get; set; } = new List<PerformanceStyle>();
    public virtual ICollection<PerformanceDancers> PerformanceDancers { get; set; } = new List<PerformanceDancers>();
    public virtual ICollection<ChoreographerPerformance> ChoreographerPerformances { get; set; } = new List<ChoreographerPerformance>();
    public virtual ICollection<PerformanceLevel> PerformanceLevels { get; set; } = new List<PerformanceLevel>();
    public virtual ICollection<PerformanceRequirement> PerformanceRequirements { get; set; } = new List<PerformanceRequirement>();
    public virtual ICollection<DanceClass> DanceClasses { get; set; } = new List<DanceClass>();
}