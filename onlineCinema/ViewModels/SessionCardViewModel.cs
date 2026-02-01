namespace onlineCinema.ViewModels
{
    public class SessionCardViewModel
    {
        public int SessionId { get; set; }
        public string Time { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public string Price { get; set; } = string.Empty;
    }
}
