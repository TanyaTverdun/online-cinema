using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class HallInputViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть номер залу")]
        [Range(1, 255)]
        [Display(Name = "Номер залу")]
        public int HallNumber { get; set; }

        [Required]
        [Range(1, 255)]
        [Display(Name = "Кількість рядів")]
        public int RowCount { get; set; }

        [Required]
        [Range(1, 255)]
        [Display(Name = "Місць у ряду")]
        public int SeatInRowCount { get; set; }

        public List<int> SelectedFeatureIds { get; set; } = new();

        public List<FeatureCheckboxViewModel> AvailableFeatures { get; set; } = new();
    }
}
