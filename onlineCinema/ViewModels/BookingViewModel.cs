namespace onlineCinema.ViewModels
{
    public class BookingViewModel
    {
        public int SessionId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public DateTime ShowingDate { get; set; }
        public List<SeatViewModel> Seats { get; set; } = new List<SeatViewModel>();
        public List<int> SelectedSeatIds { get; set; } = new List<int>();
    }
}
