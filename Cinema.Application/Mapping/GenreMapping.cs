using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class GenreMapping
    {
        public partial GenreFormDto? ToDto(Genre? genre);
        public partial Genre ToEntity(GenreFormDto dto);
        public partial void UpdateEntityFromDto(GenreFormDto dto, Genre genre);
        public partial IEnumerable<GenreFormDto> ToDtoList(IEnumerable<Genre> genres);
    }
}
