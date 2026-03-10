using System.Collections.Generic;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class GenreMapping
    {
        [MapperIgnoreSource(nameof(Genre.MovieGenres))]
        public partial GenreDto? ToDto(Genre? genre);

        [MapperIgnoreTarget(nameof(Genre.MovieGenres))]
        public partial Genre ToEntity(GenreDto dto);

        [MapperIgnoreTarget(nameof(Genre.MovieGenres))]
        public partial void UpdateEntityFromDto(
            GenreDto dto,
            Genre genre);

        public partial IEnumerable<GenreDto>
            ToDtoList(IEnumerable<Genre> genres);
    }
}