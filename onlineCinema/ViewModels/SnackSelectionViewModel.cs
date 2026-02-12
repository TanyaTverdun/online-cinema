namespace onlineCinema.ViewModels
{
    public class SnackSelectionViewModel
    {
        public int BookingId { get; set; }
        public List<SnackItemViewModel> AvailableSnacks { get; set; } = new();
        public decimal TotalSnacksPrice =>
            AvailableSnacks.Sum(s => s.Price * s.Quantity);
        public decimal SeatsTotalPrice { get; set; }
        public decimal GrandTotal => SeatsTotalPrice + TotalSnacksPrice;
        public DateTime LockUntil { get; set; }
        public string initialSeconds { get; set; }
    }
}
