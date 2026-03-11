using Microsoft.AspNetCore.Http;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Movie
{
    public class MovieFormDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public MovieStatus Status { get; set; } = MovieStatus.ComingSoon;
        public AgeRating AgeRating { get; set; }

        public decimal Rating { get; set; }

        public int Runtime { get; set; }

        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        public string? TrailerLink { get; set; }

        public string? PosterUrl { get; set; }
        public IFormFile? PosterFile { get; set; }

        public string? GenresInput { get; set; }

        public string? ActorsInput { get; set; }

        public string? DirectorsInput { get; set; }
        public string? LanguagesInput { get; set; }
        public string? FeaturesInput { get; set; }

        public List<int> GenreIds { get; set; } = [];
        public List<int> CastIds { get; set; } = [];
        public List<int> DirectorIds { get; set; } = [];
        public List<int> LanguageIds { get; set; } = [];
        public List<int> FeatureIds { get; set; } = [];
    }
}
