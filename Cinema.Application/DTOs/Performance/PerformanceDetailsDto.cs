using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class PerformanceDetailsDto // Було MovieDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Назва виступу/курсу
    public string? Description { get; set; } // Опис (про що танець, складність)
    public string ImageUrl { get; set; } = string.Empty; // Було PosterUrl
    public string? VideoLink { get; set; } // Було TrailerLink (відео виступу або запис уроку)

    public int DurationMinutes { get; set; } // Було Runtime (тривалість заняття або номеру)
    public DateTime PremiereDate { get; set; } // Було ReleaseDate (дата старту курсу або прем'єри)
    public AgeCategory AgeRating { get; set; }
    public decimal Rating { get; set; }
    public PerformanceStatus Status { get; set; }

    public List<string> Styles { get; set; } = new(); // Було Genres (напр. Hip-Hop, Jazz)
    public List<string> Choreographers { get; set; } = new(); // Було Directors
    public List<string> Dancers { get; set; } = new(); // Було Actors
    public List<string> Languages { get; set; } = new(); // Напр. мова викладання
    public List<string> Features { get; set; } = new(); // Особливості (соло, дует, спецефекти)
}