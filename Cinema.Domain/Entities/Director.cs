namespace onlineCinema.Domain.Entities;

public class Director
{
    public int DirectorId { get; set; }
    public string DirectorFirstName { get; set; } = string.Empty;
    public string DirectorLastName { get; set; } = string.Empty;
    public string? DirectorMiddleName { get; set; }
    public ICollection<DirectorMovie> DirectorMovies { get; set; } = new List<DirectorMovie>();
}