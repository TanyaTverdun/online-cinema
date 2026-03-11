namespace onlineCinema.Areas.Admin.Models
{
    public class HallViewModel
    {
        public int Id { get; set; }
        public int HallNumber { get; set; }
        public int RowCount { get; set; }
        public int SeatInRowCount { get; set; }
        public int TotalSeats { get; set; }
        public string FeaturesList { get; set; } = string.Empty;

        public List<onlineCinema.ViewModels.SessionViewModel>
            Sessions
        { get; set; } = new();
    }
}
