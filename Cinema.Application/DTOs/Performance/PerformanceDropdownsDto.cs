namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class PerformanceDropdownsDto // Було MovieDropdownsDto
{
    public List<DanceStyleDto> Styles { get; set; } = new(); // Було GenreDto Genres
    public List<PersonDto> Dancers { get; set; } = new(); // Було Actors
    public List<PersonDto> Choreographers { get; set; } = new(); // Було Directors
    public List<PerformanceFeatureDto> Features { get; set; } = new(); // Було FeatureDto
}