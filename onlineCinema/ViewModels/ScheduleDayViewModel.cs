namespace onlineCinema.ViewModels
{
    public class ScheduleDayViewModel
    {
        public string DateLabel { get; set; } = string.Empty;
        public string TabId { get; set; } = string.Empty; // для групи фільмів за днем
        public bool IsActive { get; set; } // чи є група активна на вкладці
        public List<SessionCardViewModel> Sessions { get; set; } = new();
    }
}
