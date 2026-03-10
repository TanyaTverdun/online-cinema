namespace onlineCinema.Application.DTOs
{
    public class SessionFormDto
    {
        public int? Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
        public bool GenerateForWeek { get; set; }
    }
}
