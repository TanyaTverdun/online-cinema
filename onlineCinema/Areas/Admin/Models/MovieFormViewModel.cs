using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace onlineCinema.Areas.Admin.Models
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Додайте опис")]
        public string? Description { get; set; }

        public MovieStatus Status { get; set; } = MovieStatus.ComingSoon;
        public AgeRating AgeRating { get; set; }

        [Range(1, 1000)]
        public int Runtime { get; set; }

        [DataType(DataType.Date)]
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

        [ValidateNever]
        public IEnumerable<SelectListItem> GenresList { get; set; } = Enumerable.Empty<SelectListItem>();

        [ValidateNever]
        public IEnumerable<SelectListItem> ActorsList { get; set; } = Enumerable.Empty<SelectListItem>();

        [ValidateNever]
        public IEnumerable<SelectListItem> DirectorsList { get; set; } = Enumerable.Empty<SelectListItem>();

        [ValidateNever]
        public IEnumerable<SelectListItem> LanguagesList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}