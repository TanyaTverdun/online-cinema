namespace onlineCinema.Domain.Entities;

public class MerchOrder
{
    // Зовнішній ключ для товару
    public int ProductId { get; set; }
    public virtual StudioMerch StudioMerch { get; set; } = null!;

    // Зовнішній ключ для бронювання
    public int BookingId { get; set; }
    public virtual CostumeBooking CostumeBooking { get; set; } = null!;

    // Кількість замовлених одиниць
    public byte Quantity { get; set; }
}