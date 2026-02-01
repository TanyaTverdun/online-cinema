namespace onlineCinema.ViewModels
{
    public class SnackItemViewModel
    {
        public int SnackId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
