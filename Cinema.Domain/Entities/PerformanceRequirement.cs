namespace onlineCinema.Domain.Entities;

public class PerformanceRequirement
{
    // Зовнішній ключ для виступу
    public int PerformanceId { get; set; }
    public virtual Performance Performance { get; set; } = null!;

    // Зовнішній ключ для вимоги
    public int RequirementId { get; set; }

    // Виправив назву властивості на Requirement (було Requriment)
    public virtual Requirement Requirement { get; set; } = null!;
}