namespace onlineCinema.Application.DTOs.Snack
{
    public class SnackInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}
