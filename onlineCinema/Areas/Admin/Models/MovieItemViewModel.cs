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
        public PerformanceStatus Status { get; set; }
        public AgeCategory AgeRating { get; set; }
    }
}