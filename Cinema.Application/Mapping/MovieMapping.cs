using System;
using System.Collections.Generic;
using System.Linq;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class MovieMapping
    {
        private const string PlaceholderImage = "/images/no-poster.png";

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieCardDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieCardDto.GenreSummary), Use = nameof(MapGenreSummary))]
        [MapProperty(nameof(Movie.ReleaseDate), nameof(MovieCardDto.ReleaseYear), Use = nameof(MapReleaseYear))]
        [MapProperty(nameof(Movie.AgeRating), nameof(MovieCardDto.AgeRating))]
        public partial MovieCardDto ToCardDto(Movie movie);

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieDetailsDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieDetailsDto.Genres), Use = nameof(MapGenresList))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieDetailsDto.Actors), Use = nameof(MapActorsList))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieDetailsDto.Directors), Use = nameof(MapDirectorsList))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieDetailsDto.Languages), Use = nameof(MapLanguagesList))]
        public partial MovieDetailsDto ToDetailsDto(Movie movie);

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieFormDto.PosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieFormDto.GenreIds), Use = nameof(MapGenreIds))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieFormDto.CastIds), Use = nameof(MapCastIds))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieFormDto.DirectorIds), Use = nameof(MapDirectorIds))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieFormDto.LanguageIds), Use = nameof(MapLanguageIds))]
        public partial MovieFormDto ToFormDto(Movie movie);

        public partial Movie ToEntity(MovieFormDto dto);

        public void UpdateEntityFromDto(Movie movie, MovieFormDto dto)
        {
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.Runtime = dto.Runtime;
            movie.Status = dto.Status;
            movie.AgeRating = dto.AgeRating;
            movie.TrailerLink = dto.TrailerLink;
        }

        [MapProperty(nameof(Genre.GenreId), nameof(GenreDto.Id))]
        [MapProperty(nameof(Genre.GenreName), nameof(GenreDto.Name))]
        public partial GenreDto ToGenreDto(Genre genre);

        [MapProperty(nameof(Language.LanguageId), nameof(LanguageDto.Id))]
        [MapProperty(nameof(Language.LanguageName), nameof(LanguageDto.Name))]
        public partial LanguageDto ToLanguageDto(Language language);

        public PersonDto ToPersonDto(CastMember actor) =>
            new PersonDto { Id = actor.CastId, FullName = $"{actor.CastFirstName} {actor.CastLastName}" };

        public PersonDto ToPersonDto(Director director) =>
            new PersonDto { Id = director.DirectorId, FullName = $"{director.DirectorFirstName} {director.DirectorLastName}" };

        private string MapPosterUrl(string? posterImage) =>
            string.IsNullOrEmpty(posterImage) ? PlaceholderImage : posterImage;

        private string MapGenreSummary(ICollection<MovieGenre> genres) =>
            genres == null ? "" : string.Join(", ", genres.Select(mg => mg.Genre.GenreName).Take(2));

        private List<string> MapGenresList(ICollection<MovieGenre> genres) =>
            genres.Select(mg => mg.Genre.GenreName).ToList();

        private List<string> MapActorsList(ICollection<MovieCast> casts) =>
            casts.Select(mc => $"{mc.CastMember.CastFirstName} {mc.CastMember.CastLastName}").ToList();

        private List<string> MapDirectorsList(ICollection<DirectorMovie> directors) =>
            directors.Select(md => $"{md.Director.DirectorFirstName} {md.Director.DirectorLastName}").ToList();

        private List<string> MapLanguagesList(ICollection<LanguageMovie> languages) =>
            languages.Select(ml => ml.Language.LanguageName).ToList();

        private List<int> MapGenreIds(ICollection<MovieGenre> genres) =>
            genres.Select(x => x.GenreId).ToList();

        private List<int> MapCastIds(ICollection<MovieCast> casts) =>
            casts.Select(x => x.CastId).ToList();

        private List<int> MapDirectorIds(ICollection<DirectorMovie> directors) =>
            directors.Select(x => x.DirectorId).ToList();

        private List<int> MapLanguageIds(ICollection<LanguageMovie> languages) =>
            languages.Select(x => x.LanguageId).ToList();

        private int MapReleaseYear(DateTime date) => date.Year;
    }
}