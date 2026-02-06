namespace onlineCinema.Areas.Admin.Models
{
    public class SessionListViewModel
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int HallNumber { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public decimal BasePrice { get; set; }
        public string FormattedDateTime => ShowingDateTime.ToString("dd.MM.yyyy HH:mm");
    }
}
