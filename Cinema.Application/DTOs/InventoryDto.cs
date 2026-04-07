using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs;

public class InventoryDto // Було SeatDto
{
    public int InventoryId { get; set; } // Було SeatId
    public string Name { get; set; } = string.Empty; // Наприклад: "Килимок для йоги", "Балетний станок"
    public int Number { get; set; } // Інвентарний номер
    public ConditionStatus Status { get; set; } // Стан (Новий, Б/В, Потребує ремонту)
    public decimal Price { get; set; } // Додаткова вартість оренди (якщо є)
    public bool IsAvailable { get; set; } // Чи вільний цей предмет зараз
}