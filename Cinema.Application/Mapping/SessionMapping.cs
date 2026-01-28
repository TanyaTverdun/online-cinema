using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using onlineCinema.Domain.Entities;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Mapping
{
    [Mapper] 
    public partial class SessionMapping 
    {
     
            [MapProperty(nameof(Session.Movie.Title), nameof(SessionDto.MovieTitle))]
            [MapProperty(nameof(Session.Movie.PosterImage), nameof(SessionDto.MoviePosterImage))]
            [MapProperty(nameof(Session.Hall.HallNumber), nameof(SessionDto.HallName))]
            public partial SessionDto SessionToDto(Session session);
            public partial Session DtoToSession(SessionDto sessionDto);
        }
    }
    
