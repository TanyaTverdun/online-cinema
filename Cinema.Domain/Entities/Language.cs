namespace onlineCinema.Domain.Entities;

public class Language
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = string.Empty;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    public ICollection<LanguageMovie> LanguageMovies { get; set; } = new List<LanguageMovie>();
}