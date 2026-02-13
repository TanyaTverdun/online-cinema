using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class DirectorMapping
    {
        public partial DirectorFormDto? ToDto(Director? director);

        public partial Director ToEntity(DirectorFormDto dto);

        public partial void UpdateEntityFromDto(
            DirectorFormDto dto, 
            Director director);

        public partial IEnumerable<DirectorFormDto> 
            ToDtoList(IEnumerable<Director> directors);
    }
}