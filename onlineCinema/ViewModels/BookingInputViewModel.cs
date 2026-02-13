namespace onlineCinema.ViewModels
{
    public class BookingInputViewModel
    {
        public int SessionId { get; set; }
        public List<int> SelectedSeatIds { get; set; } = new();
    }
}
