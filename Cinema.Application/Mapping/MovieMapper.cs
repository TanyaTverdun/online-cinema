using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class MovieMapper
    {
        public partial MovieDto MapToDto(Movie movie);

        public partial IEnumerable<MovieDto> MapToDtoList(IEnumerable<Movie> movies);
    }
}
