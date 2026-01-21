namespace onlineCinema.Domain.Entities;

public class LanguageMovie
{
    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}