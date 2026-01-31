using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminDirectorMapper
    {
        public partial DirectorFormDto ToDto(DirectorFormViewModel viewModel);
        public partial DirectorFormViewModel ToViewModel(DirectorFormDto dto);
    }
}