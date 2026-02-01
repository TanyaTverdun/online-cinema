using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminFeatureMapper
    {
        public partial FeatureViewModel ToViewModel(FeatureDto dto);
        public partial List<FeatureViewModel> ToViewModelList(IEnumerable<FeatureDto> dtos);
        public partial FeatureDto ToDto(FeatureViewModel model);
    }
}