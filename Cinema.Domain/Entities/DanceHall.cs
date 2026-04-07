namespace onlineCinema.Domain.Entities;

public class DanceHall
{
    // Первинний ключ
    public int HallId { get; set; }

    // Зв'язок із філією студії
    public int BranchId { get; set; }
    public virtual StudioBranch StudioBranch { get; set; } = null!;

    // Характеристики залу
    public byte HallNumber { get; set; }
    public decimal AreaSize { get; set; } // Змінив float на decimal для точних розрахунків площі
    public int MaxPeople { get; set; }

    // Зв'язки
    // Виправив назву колекції на plural форму Inventory
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<DanceClass> DanceClasses { get; set; } = new List<DanceClass>();

    // Виправив назву (прибрав зайву S в кінці)
    public virtual ICollection<HallEquipment> HallEquipments { get; set; } = new List<HallEquipment>();
}