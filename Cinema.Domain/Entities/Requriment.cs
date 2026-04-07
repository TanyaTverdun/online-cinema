namespace onlineCinema.Domain.Entities;

// Виправив назву з Requriment на Requirement
public class Requirement
{
    // Первинний ключ
    public int RequirementId { get; set; }

    // Коротка назва вимоги (наприклад, "Балетний станок")
    public string RequirementName { get; set; } = string.Empty;

    // Детальний опис (наприклад, "Дерев'яний станок довжиною 3 метри")
    public string? RequirementDescription { get; set; }

    // Зв'язки (навігаційні властивості)
    // Зв'язок із вимогами конкретних виступів
    public virtual ICollection<PerformanceRequirement> PerformanceRequirements { get; set; } = new List<PerformanceRequirement>();

    // Зв'язок із обладнанням залів
    public virtual ICollection<HallEquipment> HallEquipments { get; set; } = new List<HallEquipment>();
}