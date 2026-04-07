using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class PerformanceFormDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введіть назву виступу/курсу")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Додайте опис програми")]
    public string? Description { get; set; }

    public PerformanceStatus Status { get; set; } = PerformanceStatus.InPreparation;
    public AgeCategory AgeRating { get; set; }

    [Range(0, 10)]
    public decimal Rating { get; set; }

    [Range(1, 1000)]
    public int DurationMinutes { get; set; } // Було Runtime

    [DataType(DataType.Date)]
    public DateTime PremiereDate { get; set; } = DateTime.Today; // Було ReleaseDate

    public string? VideoLink { get; set; } // Було TrailerLink (відео-презентація танцю)

    public string? ImageUrl { get; set; } // Було PosterUrl
    public IFormFile? ImageFile { get; set; } // Було PosterFile (для завантаження фото)

    // Поля для швидкого введення текстом (якщо використовуєш таку логіку)
    public string? StylesInput { get; set; } // Було GenresInput
    public string? DancersInput { get; set; } // Було ActorsInput
    public string? ChoreographersInput { get; set; } // Було DirectorsInput
    public string? LanguagesInput { get; set; }
    public string? FeaturesInput { get; set; }

    // Списки ID для зв'язку в базі (Many-to-Many)
    public List<int> StyleIds { get; set; } = new(); // Було GenreIds
    public List<int> DancerIds { get; set; } = new(); // Було CastIds
    public List<int> ChoreographerIds { get; set; } = new(); // Було DirectorIds
    public List<int> LanguageIds { get; set; } = new();
    public List<int> FeatureIds { get; set; } = new();
}