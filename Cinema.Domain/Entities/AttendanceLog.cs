namespace onlineCinema.Domain.Entities;

public class AttendanceLog
{
    // Явний первинний ключ
    public int AttendanceId { get; set; }

    // Зв'язок із заняттям (обов'язково)
    public int ClassId { get; set; }
    public virtual DanceClass DanceClass { get; set; } = null!;

    // Зв'язок із танцюристом (хто прийшов)
    public int? DancerId { get; set; }
    public virtual Dancer? Dancer { get; set; }

    // Оренда інвентарю (необов'язково)
    public int? InventoryId { get; set; }
    public virtual Inventory? Inventory { get; set; }

    // Бронювання костюма (необов'язково)
    public int? BookingId { get; set; }
    public virtual CostumeBooking? CostumeBooking { get; set; }

    // Фінанси та час
    public decimal ActualPrice { get; set; }
    public DateTime VisitDateTime { get; set; }

    // Статус відвідування
    public bool IsPresent { get; set; }
    public DateTime? LockUntil { get; set; }
}