namespace onlineCinema.Areas.Admin.Models
{
    public class SessionDeleteViewModel
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
    }
}
