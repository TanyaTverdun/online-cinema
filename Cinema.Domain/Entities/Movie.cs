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
        public decimal Rating { get; set; }
        public string? PosterImage { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = [];
        public ICollection<MovieCast> MovieCasts { get; set; } = [];
        public ICollection<DirectorMovie> MovieDirectors { get; set; } = [];
        public ICollection<LanguageMovie> MovieLanguages { get; set; } = [];
        public ICollection<MovieFeature> MovieFeatures { get; set; } = [];
        public ICollection<Session> Sessions { get; set; } = [];
    }
}
