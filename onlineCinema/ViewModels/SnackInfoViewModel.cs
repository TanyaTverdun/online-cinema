namespace onlineCinema.ViewModels
{
    public class SnackInfoViewModel
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}