namespace onlineCinema.Application.DTOs.Session
{
    public class SessionDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int HallId { get; set; }
        public int HallNumber { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
    }
}
