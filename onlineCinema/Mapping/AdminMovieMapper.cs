using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminMovieMapper
    {
        [MapperIgnoreTarget(nameof(MovieFormDto.Runtime))]
        private partial MovieFormDto MapToDtoBase(MovieFormViewModel viewModel);

        [MapperIgnoreSource(nameof(MovieFormDto.Runtime))]
        private partial MovieFormViewModel MapToViewModelBase(MovieFormDto dto);
        public MovieFormDto ToDto(MovieFormViewModel viewModel)
        {
            var dto = MapToDtoBase(viewModel);
            if (viewModel.Runtime.HasValue)
            {
                dto.Runtime = (int)viewModel.Runtime.Value.TotalMinutes;
            }
            return dto;
        }

        public MovieFormViewModel ToViewModel(MovieFormDto dto)
        {
            var vm = MapToViewModelBase(dto);
            vm.Runtime = TimeSpan.FromMinutes(dto.Runtime);
            return vm;
        }

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