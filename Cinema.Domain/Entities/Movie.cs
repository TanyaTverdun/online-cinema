using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public MovieStatus Status { get; set; }
        public AgeRating AgeRating { get; set; }
        public int Runtime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? TrailerLink { get; set; }
        public string? Description { get; set; }
        public string? PosterImage { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
        public ICollection<DirectorMovie> MovieDirectors { get; set; } = new List<DirectorMovie>();
        public ICollection<LanguageMovie> MovieLanguages { get; set; } = new List<LanguageMovie>();
        public ICollection<MovieFeature> ShowingFeatures { get; set; } = new List<MovieFeature>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
