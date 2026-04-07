namespace onlineCinema.Domain.Entities;

public class PerformanceLevel
{
    // Зовнішній ключ для рівня навичок (SkillLevel)
    public int LevelId { get; set; }
    public virtual SkillLevel SkillLevel { get; set; } = null!;

    // Зовнішній ключ для виступу (Performance)
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;
}