using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminGenreMapper
    {
        public partial GenreFormDto ToDto(GenreFormViewModel viewModel);
        public partial GenreFormViewModel ToViewModel(GenreFormDto dto);
        public partial IEnumerable<GenreFormViewModel> ToViewModelList(IEnumerable<GenreFormDto> dtos);
    }
}
