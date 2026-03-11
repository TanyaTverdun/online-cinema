namespace onlineCinema.Application.DTOs.Session
{
    public class MovieScheduleDto
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int Runtime { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public List<DailyScheduleDto> Schedule { get; set; } = new();
    }
}
