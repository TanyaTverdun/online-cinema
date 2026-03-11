namespace onlineCinema.Application.DTOs.Session
{
    public class SessionScheduleDto
    {
        public int SessionId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string HallName { get; set; } = string.Empty;
        public List<string> FeatureNames { get; set; } = new();
        public decimal BasePrice { get; set; }
    }
}
