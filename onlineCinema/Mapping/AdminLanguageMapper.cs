using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminLanguageMapper
    {
        public partial LanguageViewModel ToViewModel(LanguageDto dto);
        public partial List<LanguageViewModel> ToViewModelList(
            IEnumerable<LanguageDto> dtos);
        public partial LanguageDto ToDto(LanguageViewModel model);
    }
}