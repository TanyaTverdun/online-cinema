using System.Collections.Generic;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class GenreMapping
    {
        [MapperIgnoreSource(nameof(DanceStyle.MovieGenres))]
        public partial GenreFormDto? ToDto(DanceStyle? genre);

        [MapperIgnoreTarget(nameof(DanceStyle.MovieGenres))]
        public partial DanceStyle ToEntity(GenreFormDto dto);

        [MapperIgnoreTarget(nameof(DanceStyle.MovieGenres))]
        public partial void UpdateEntityFromDto(
            GenreFormDto dto, 
            DanceStyle genre);

        public partial IEnumerable<GenreFormDto> 
            ToDtoList(IEnumerable<DanceStyle> genres);
    }
}