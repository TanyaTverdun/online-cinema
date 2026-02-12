using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminFeatureMapper
    {
        [MapProperty(
            nameof(FeatureDto.Id),
            nameof(FeatureViewModel.FeatureId))]
        [MapProperty(
            nameof(FeatureDto.Name),
            nameof(FeatureViewModel.FeatureName))]
        [MapProperty(
            nameof(FeatureDto.Description), 
            nameof(FeatureViewModel.FeatureDescription))]
        public partial FeatureViewModel ToViewModel(FeatureDto dto);

        public partial List<FeatureViewModel> ToViewModelList(
            IEnumerable<FeatureDto> dtos);

        [MapProperty(
            nameof(FeatureViewModel.FeatureId),
            nameof(FeatureDto.Id))]
        [MapProperty(
            nameof(FeatureViewModel.FeatureName), 
            nameof(FeatureDto.Name))]
        [MapProperty(
            nameof(FeatureViewModel.FeatureDescription), 
            nameof(FeatureDto.Description))]
        public partial FeatureDto ToDto(FeatureViewModel model);
    }

}