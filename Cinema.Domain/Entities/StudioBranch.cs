namespace onlineCinema.Domain.Entities;

public class StudioBranch
{
    // Первинний ключ
    public int BranchId { get; set; }

    // Назва філії (наприклад, "Центральна", "Sky Studio")
    public string BranchName { get; set; } = string.Empty;

    // Адреса
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;

    // Змінив int на string для номера будинку (може бути "12А", "4/1")
    public string Building { get; set; } = string.Empty;

    // Графік роботи
    public TimeSpan TimeOpen { get; set; }
    public TimeSpan TimeClose { get; set; }

    // Навігаційна властивість для залів цієї філії
    public virtual ICollection<DanceHall> DanceHalls { get; set; } = new List<DanceHall>();
}