namespace onlineCinema.Domain.Entities;

public class ChoreographerPerformance
{
    // Зовнішній ключ для хореографа
    public int ChoreographerId { get; set; }
    public virtual Choreographer Choreographer { get; set; } = null!;

    // Зовнішній ключ для виступу/програми
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;
}