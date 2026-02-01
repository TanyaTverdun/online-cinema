using System.Collections.Generic;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class DirectorMapping
    {

        [MapperIgnoreSource(nameof(Director.DirectorMovies))]
        public partial DirectorFormDto? ToDto(Director? director);

        [MapperIgnoreTarget(nameof(Director.DirectorMovies))]
        public partial Director ToEntity(DirectorFormDto dto);

        [MapperIgnoreTarget(nameof(Director.DirectorMovies))]
        public partial void UpdateEntityFromDto(DirectorFormDto dto, Director director);

        public partial IEnumerable<DirectorFormDto> ToDtoList(IEnumerable<Director> directors);
    }
}