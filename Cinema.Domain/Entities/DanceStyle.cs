namespace onlineCinema.Domain.Entities;

public class DanceStyle
{
    // Первинний ключ
    public int StyleId { get; set; }

    // Назва стилю (наприклад, "Hip-Hop")
    public string StyleName { get; set; } = string.Empty;

    // Опис стилю (корисно для клієнтів на сайті)
    public string? Description { get; set; }

    // Зв'язок "багато-до-багатьох" із виступами/програмами
    public virtual ICollection<PerformanceStyle> PerformanceStyles { get; set; } = new List<PerformanceStyle>();
}