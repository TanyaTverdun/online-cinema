
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class HallViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Номер залу")]
        public int HallNumber { get; set; }

        [Display(Name = "Кількість рядів")]
        public int RowCount { get; set; }

        [Display(Name = "Місць у ряду")]
        public int SeatInRowCount { get; set; }

        [Display(Name = "Всього місць")]
        public int TotalSeats { get; set; }

        [Display(Name = "Особливості")]
        public string FeaturesList { get; set; } = string.Empty;

        [Display(Name = "Сессії")]
        public List<SessionViewModel> Sessions { get; set; } = new();
    }
}
