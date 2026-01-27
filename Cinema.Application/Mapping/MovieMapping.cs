using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public static partial class MovieMapping
    {
        private const string PlaceholderImage = "/images/no-poster.png";

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieCardDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieCardDto.GenreSummary), Use = nameof(MapGenreSummary))]
       
        [MapProperty(nameof(Movie.ReleaseDate), nameof(MovieCardDto.ReleaseYear), Use = nameof(MapReleaseYear))]
       
        [MapProperty(nameof(Movie.AgeRating), nameof(MovieCardDto.AgeRating))]
        public static partial MovieCardDto ToCardDto(this Movie movie);

      
        [MapProperty(nameof(Movie.PosterImage), nameof(MovieDetailsDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieDetailsDto.Genres), Use = nameof(MapGenresList))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieDetailsDto.Actors), Use = nameof(MapActorsList))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieDetailsDto.Directors), Use = nameof(MapDirectorsList))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieDetailsDto.Languages), Use = nameof(MapLanguagesList))]
        public static partial MovieDetailsDto ToDetailsDto(this Movie movie);

      
        [MapProperty(nameof(Movie.PosterImage), nameof(MovieFormDto.PosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieFormDto.GenreIds), Use = nameof(MapGenreIds))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieFormDto.CastIds), Use = nameof(MapCastIds))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieFormDto.DirectorIds), Use = nameof(MapDirectorIds))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieFormDto.LanguageIds), Use = nameof(MapLanguageIds))]
        public static partial MovieFormDto ToFormDto(this Movie movie);

       
        public static partial Movie ToEntity(this MovieFormDto dto);

      
        public static partial void UpdateFromDto(this Movie movie, MovieFormDto dto);


      
        private static string MapPosterUrl(string? posterImage)
            => string.IsNullOrEmpty(posterImage) ? PlaceholderImage : posterImage;

        private static string MapGenreSummary(ICollection<MovieGenre> genres)
            => genres == null ? "" : string.Join(", ", genres.Select(mg => mg.Genre.GenreName).Take(2));

        private static List<string> MapGenresList(ICollection<MovieGenre> genres)
            => genres.Select(mg => mg.Genre.GenreName).ToList();

        private static List<string> MapActorsList(ICollection<MovieCast> casts)
            => casts.Select(mc => $"{mc.CastMember.CastFirstName} {mc.CastMember.CastLastName}").ToList();

        private static List<string> MapDirectorsList(ICollection<DirectorMovie> directors)
            => directors.Select(md => $"{md.Director.DirectorFirstName} {md.Director.DirectorLastName}").ToList();

        private static List<string> MapLanguagesList(ICollection<LanguageMovie> languages)
            => languages.Select(ml => ml.Language.LanguageName).ToList();

      
        private static List<int> MapGenreIds(ICollection<MovieGenre> genres) => genres.Select(x => x.GenreId).ToList();
        private static List<int> MapCastIds(ICollection<MovieCast> casts) => casts.Select(x => x.CastId).ToList();
        private static List<int> MapDirectorIds(ICollection<DirectorMovie> directors) => directors.Select(x => x.DirectorId).ToList();
        private static List<int> MapLanguageIds(ICollection<LanguageMovie> languages) => languages.Select(x => x.LanguageId).ToList();
        private static int MapReleaseYear(DateTime date) => date.Year;
    }
}