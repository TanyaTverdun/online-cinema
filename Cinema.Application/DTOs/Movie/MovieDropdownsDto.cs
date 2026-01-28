using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.Movie
{
    public class MovieDropdownsDto
    {
        public List<GenreDto> Genres { get; set; } = new();
        public List<PersonDto> Actors { get; set; } = new();
        public List<PersonDto> Directors { get; set; } = new();
        public List<LanguageDto> Languages { get; set; } = new();
    }

    public class GenreDto { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
    public class PersonDto { public int Id { get; set; } public string FullName { get; set; } = string.Empty; }
    public class LanguageDto { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
}