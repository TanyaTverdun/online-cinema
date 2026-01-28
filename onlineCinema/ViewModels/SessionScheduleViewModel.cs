namespace onlineCinema.ViewModels
{
    public class SessionScheduleViewModel
    {
        public int SessionId { get; set; }
        public string MovieTitle { get; set; } = default!;
        public string MoviePoster { get; set; } = default!;
        public string StartTime { get; set; } = default!; 
        public string StartDate { get; set; } = default!; 
        public string HallName { get; set; } = default!; 
        public string Price { get; set; } = default!;
    }
}
