using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public static partial class AdminMovieMapper
    {
        public static partial MovieFormDto ToDto(this MovieFormViewModel viewModel);

        public static partial MovieFormViewModel ToViewModel(this MovieFormDto dto);
    }
}