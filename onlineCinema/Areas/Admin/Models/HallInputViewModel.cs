using onlineCinema.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models
{
    public class HallInputViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Номер залу")]
        public int HallNumber { get; set; }

        [Display(Name = "Кількість рядів")]
        public int RowCount { get; set; }

        [Display(Name = "Місць у ряду")]
        public int SeatInRowCount { get; set; }

        public List<int> SelectedFeatureIds { get; set; } = new();

        public List<FeatureCheckboxViewModel> AvailableFeatures { get; set; } = new();

        [Display(Name = "Кількість VIP рядів (з кінця)")]
        public int VipRowCount { get; set; }

        [Display(Name = "Коефіцієнт ціни для VIP місць")]
        public float VipCoefficient { get; set; } = 1.5f;
    }
}
