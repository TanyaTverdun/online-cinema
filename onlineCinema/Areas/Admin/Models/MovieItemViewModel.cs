using onlineCinema.Domain.Enums;

namespace onlineCinema.Areas.Admin.Models
{
    public class MovieItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public int ReleaseYear { get; set; }
        public string GenreSummary { get; set; } = string.Empty;
        public MovieStatus Status { get; set; }
        public AgeRating AgeRating { get; set; }
    }
}