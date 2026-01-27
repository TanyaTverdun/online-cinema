using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Movie
{
    public class MovieCardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string GenreSummary { get; set; } = string.Empty;

        public string PosterUrl { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }
        public AgeRating AgeRating { get; set; }
        public MovieStatus Status { get; set; }
    }
}