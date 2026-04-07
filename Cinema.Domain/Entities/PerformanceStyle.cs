namespace onlineCinema.Domain.Entities;

public class PerformanceStyle
{
    // Зовнішній ключ для виступу
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;

    // Зовнішній ключ для стилю танцю
    public int StyleId { get; set; }
    public virtual DanceStyle DanceStyle { get; set; } = null!;
}