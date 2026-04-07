namespace onlineCinema.Domain.Entities;

public class PerformanceDancers
{
    // Зовнішній ключ для виступу
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;

    // Зовнішній ключ для танцюриста
    public int DancerId { get; set; }
    public virtual Dancer Dancer { get; set; } = null!;
}