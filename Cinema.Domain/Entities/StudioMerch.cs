namespace onlineCinema.Domain.Entities;

public class StudioMerch
{
    // Первинний ключ
    public int ProductId { get; set; }

    // Назва товару (наприклад, "Фірмова футболка", "Пляшка для води")
    public string ProductName { get; set; } = string.Empty;

    // Опис товару (розміри, колір, матеріал)
    public string? Description { get; set; }

    // Ціна (у БД буде тип money)
    public decimal Price { get; set; }

    // Зв'язки (Навігаційні властивості)
    // Список усіх замовлень, у яких фігурує цей товар
    public virtual ICollection<MerchOrder> MerchOrders { get; set; } = new List<MerchOrder>();
}