namespace onlineCinema.ViewModels
{
    public class ScheduleDayViewModel
    {
        public string DateLabel { get; set; } = string.Empty;
        public string TabId { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<SessionCardViewModel> Sessions { get; set; } = new();
    }
}
