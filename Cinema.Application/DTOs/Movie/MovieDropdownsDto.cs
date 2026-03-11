using onlineCinema.Application.DTOs.Common;
using onlineCinema.Application.DTOs.Person;

namespace onlineCinema.Application.DTOs.Movie;

public class MovieDropdownsDto
{
    public List<GenreDto> Genres { get; set; } = new();
    public List<PersonDto> Actors { get; set; } = new();
    public List<PersonDto> Directors { get; set; } = new();
    public List<LanguageDto> Languages { get; set; } = new();
    public List<FeatureDto> Features { get; set; } = new();
}