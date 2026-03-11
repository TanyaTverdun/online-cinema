using onlineCinema.Application.DTOs.Common;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminGenreMapper
    {
        public partial GenreDto ToDto(GenreFormViewModel viewModel);
        public partial GenreFormViewModel ToViewModel(GenreDto dto);
        public partial IEnumerable<GenreFormViewModel> ToViewModelList(
            IEnumerable<GenreDto> dtos);
    }
}
