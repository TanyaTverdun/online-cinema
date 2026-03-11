namespace onlineCinema.Application.DTOs.Session
{
    public class DailyScheduleDto
    {
        public DateTime Date { get; set; }
        public List<SessionScheduleDto> Sessions { get; set; } = new();
    }
}
