using onlineCinema.ViewModels;

namespace onlineCinema.Areas.Admin.Models
{
    public class HallInputViewModel
    {
        public int Id { get; set; }
        public int HallNumber { get; set; }
        public int RowCount { get; set; }
        public int SeatInRowCount { get; set; }

        public List<int> SelectedFeatureIds { get; set; } = new();

        public List<FeatureCheckboxViewModel> AvailableFeatures { get; set; }
            = new();

        public int VipRowCount { get; set; }
        public float VipCoefficient { get; set; } = 1.5f;
    }
}
