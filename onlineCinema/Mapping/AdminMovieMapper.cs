using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminMovieMapper
    {
        public partial MovieFormDto ToDto(MovieFormViewModel viewModel);

        public partial MovieFormViewModel ToViewModel(MovieFormDto dto);

        public partial MovieItemViewModel ToViewModel(MovieCardDto dto);

        [MapProperty(nameof(MovieDropdownsDto.Genres), nameof(MovieFormViewModel.GenresList))]
        [MapProperty(nameof(MovieDropdownsDto.Actors), nameof(MovieFormViewModel.ActorsList))]
        [MapProperty(nameof(MovieDropdownsDto.Directors), nameof(MovieFormViewModel.DirectorsList))]
        [MapProperty(nameof(MovieDropdownsDto.Languages), nameof(MovieFormViewModel.LanguagesList))]
        public partial void Fill(MovieDropdownsDto dto, MovieFormViewModel vm);

        private SelectListItem MapGenreToItem(GenreDto source)
            => new SelectListItem(source.Name, source.Id.ToString());

        private SelectListItem MapPersonToItem(PersonDto source)
            => new SelectListItem(source.FullName, source.Id.ToString());

        private SelectListItem MapLanguageToItem(LanguageDto source)
            => new SelectListItem(source.Name, source.Id.ToString());
    }
}