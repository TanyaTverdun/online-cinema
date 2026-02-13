using System.Collections.Generic;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class GenreMapping
    {
        [MapperIgnoreSource(nameof(Genre.MovieGenres))]
        public partial GenreFormDto? ToDto(Genre? genre);

        [MapperIgnoreTarget(nameof(Genre.MovieGenres))]
        public partial Genre ToEntity(GenreFormDto dto);

        [MapperIgnoreTarget(nameof(Genre.MovieGenres))]
        public partial void UpdateEntityFromDto(
            GenreFormDto dto, 
            Genre genre);

        public partial IEnumerable<GenreFormDto> 
            ToDtoList(IEnumerable<Genre> genres);
    }
}