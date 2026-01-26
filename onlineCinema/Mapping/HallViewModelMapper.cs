using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using onlineCinema.ViewModels;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class HallViewModelMapper
    {
        [MapProperty(nameof(HallDto.HallNumber), nameof(HallViewModel.HallNumber))]
        private partial HallViewModel MapToViewModelBase(HallDto dto);

        public HallViewModel MapToViewModel(HallDto dto)
        {
            var vm = MapToViewModelBase(dto);
            vm.FeaturesList = string.Join(", ", dto.FeatureNames);
            return vm;
        }

        public HallDto MapToDto(HallInputViewModel model)
        {
            return new HallDto
            {
                Id = model.Id,
                HallNumber = model.HallNumber,
                RowCount = model.RowCount,
                SeatInRowCount = model.SeatInRowCount,

                FeatureIds = model.SelectedFeatureIds ?? new List<int>()
            };
        }

        public HallInputViewModel PrepareInputViewModel(
            IEnumerable<Feature> allFeatures,
            HallDto? hallDto = null)
        {
            var vm = new HallInputViewModel();

            if (hallDto != null)
            {
                vm.Id = hallDto.Id;
                vm.HallNumber = hallDto.HallNumber;
                vm.RowCount = hallDto.RowCount;
                vm.SeatInRowCount = hallDto.SeatInRowCount;
                vm.SelectedFeatureIds = hallDto.FeatureIds;
            }

            vm.AvailableFeatures = allFeatures.Select(f => new FeatureCheckboxViewModel
            {
                Id = f.Id,
                Name = f.Name,
                IsSelected = vm.SelectedFeatureIds.Contains(f.Id)
            }).ToList();

            return vm;
        }
    }
}
