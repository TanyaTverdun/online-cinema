namespace onlineCinema.Application.DTOs.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<int> FeatureIds { get; set; } = new();
}
