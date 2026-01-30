using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Movie
{
    public class MovieFormDto
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

        public string? GenresInput { get; set; }

       
        public string? ActorsInput { get; set; }

        
        public string? DirectorsInput { get; set; }
        public string? LanguagesInput { get; set; }
        public string? FeaturesInput { get; set; }

        public List<int> GenreIds { get; set; } = new List<int>();
        public List<int> CastIds { get; set; } = new List<int>();
        public List<int> DirectorIds { get; set; } = new List<int>();
        public List<int> LanguageIds { get; set; } = new List<int>();
        public List<int> FeatureIds { get; set; } = new List<int>();
    }
}