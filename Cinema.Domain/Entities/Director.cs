namespace onlineCinema.Domain.Entities;

public class Director
{
    public int DirectorId { get; set; }
    public string DirectorFirstName { get; set; } = string.Empty;
    public string DirectorLastName { get; set; } = string.Empty;
    public string DirectorMiddleName { get; set; } = string.Empty;
    public ICollection<DirectorMovie> DirectorMovies { get; set; } = new List<DirectorMovie>();
}