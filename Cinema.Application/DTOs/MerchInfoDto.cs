namespace onlineCinema.Application.DTOs;

public class MerchInfoDto // Було SnackInfoDto
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; } // Ціна за одиницю товару
    public decimal TotalPrice => Price * Quantity;
}