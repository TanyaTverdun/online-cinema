namespace onlineCinema.ViewModels
{
    public class MovieScheduleViewModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int Runtime { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public List<ScheduleDayViewModel> Days { get; set; } = new();
    }
}
