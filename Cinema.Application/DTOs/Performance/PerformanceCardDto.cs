using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Performance; // Було .Movie

public class PerformanceCardDto // Було MovieCardDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Назва (наприклад, "Salsa Beginners")

    public string StyleSummary { get; set; } = string.Empty; // Було GenreSummary (наприклад, "Latin / Social")

    public string ImageUrl { get; set; } = string.Empty; // Було PosterUrl (прев'ю заняття)

    public int Year { get; set; } // Рік створення програми або сезону
    public AgeCategory AgeRating { get; set; } // Важливо: 6+, 12+, 18+ для танців
    public decimal Rating { get; set; } // Оцінка користувачів
    public PerformanceStatus Status { get; set; } // Статус: "Активно", "Скоро прем'єра" тощо
    public List<string> Features { get; set; } = new(); // Наприклад: "Solo", "Duet", "Props"
}