using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace onlineCinema.Areas.Admin.Models
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public MovieStatus Status { get; set; } = MovieStatus.ComingSoon;
        public AgeRating AgeRating { get; set; }
        public TimeSpan? Runtime { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        public string? TrailerLink { get; set; }
        public string? PosterUrl { get; set; }
        public IFormFile? PosterFile { get; set; }

        public List<int> GenreIds { get; set; } = new();
        public List<int> CastIds { get; set; } = new();
        public List<int> DirectorIds { get; set; } = new();
        public List<int> LanguageIds { get; set; } = new();

        public string? GenresInput { get; set; }
        public string? ActorsInput { get; set; }
        public string? DirectorsInput { get; set; }
        public string? LanguagesInput { get; set; }
        public List<int> FeatureIds { get; set; } = new();
        public string? FeaturesInput { get; set; }

        public IEnumerable<SelectListItem> FeaturesList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> GenresList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> ActorsList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> DirectorsList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> LanguagesList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}