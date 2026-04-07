namespace onlineCinema.Domain.Entities;

public class HallEquipment
{
    // Зовнішній ключ для вимоги/обладнання
    public int RequirementId { get; set; }

    // Виправив назву властивості на Requirement (було Requriment)
    public virtual Requirement Requirement { get; set; } = null!;

    // Зовнішній ключ для залу
    public int HallId { get; set; }
    public virtual DanceHall DanceHall { get; set; } = null!;
}