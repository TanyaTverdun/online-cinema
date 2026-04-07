using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities;

// Виправив назву з Inventary на Inventory
public class Inventory
{
    // Первинний ключ (краще назвати InventoryId для стандарту, але залишаю ItemId як на схемі)
    public int ItemId { get; set; }

    // Зовнішній ключ для залу, де знаходиться цей інвентар
    public int HallId { get; set; }
    public virtual DanceHall DanceHall { get; set; } = null!;

    // Категорія (наприклад: "Килимок", "Шафка", "Станок")
    public string Category { get; set; } = string.Empty;

    // Індивідуальний номер (наприклад, номер шафки)
    // Виправив одрук: було IdentifiertNumber
    public int IdentifierNumber { get; set; }

    // Стан (наприклад: Новий, Вживаний, Потребує ремонту)
    // Змінив назву з Type на Condition, щоб не плутати з категорією
    public ConditionStatus Condition { get; set; }

    // Зв'язок із журналом відвідувань (якщо інвентар видається на час заняття)
    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}