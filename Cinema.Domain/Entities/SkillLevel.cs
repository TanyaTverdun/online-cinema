namespace onlineCinema.Domain.Entities;

public class SkillLevel
{
    // Первинний ключ
    public int LevelId { get; set; }

    // Назва рівня (наприклад, "Pro")
    public string LevelName { get; set; } = string.Empty;

    // Додав опис (наприклад, "Для тих, хто танцює більше 2-х років")
    public string? Description { get; set; }

    // Зв'язки (Навігаційні властивості)

    // Прямий зв'язок з виступами (якщо один виступ має один основний рівень)
    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    // Зв'язок через Join-table (якщо один виступ підходить для кількох рівнів одночасно)
    public virtual ICollection<PerformanceLevel> PerformanceLevels { get; set; } = new List<PerformanceLevel>();
}