using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.DTOs.Movie
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string? TrailerLink { get; set; }

        public int Runtime { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public AgeRating AgeRating { get; set; }
        public MovieStatus Status { get; set; }

        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Directors { get; set; } = new List<string>();
        public List<string> Actors { get; set; } = new List<string>();
        public List<string> Languages { get; set; } = new List<string>();
    }
}