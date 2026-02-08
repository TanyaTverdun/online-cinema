using System;
using System.Collections.Generic;
using System.Linq;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class MovieMapping
    {
        public partial MovieDto MapToDto(Movie movie);

        public partial IEnumerable<MovieDto> MapToDtoList(IEnumerable<Movie> movies);

        private const string PlaceholderImage = "/images/no-poster.png";

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieCardDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieCardDto.GenreSummary), Use = nameof(MapGenreSummary))]
        [MapProperty(nameof(Movie.ReleaseDate), nameof(MovieCardDto.ReleaseYear), Use = nameof(MapReleaseYear))]
        [MapProperty(nameof(Movie.AgeRating), nameof(MovieCardDto.AgeRating))]
        [MapProperty(nameof(Movie.MovieFeatures), nameof(MovieCardDto.Features), Use = nameof(MapFeaturesList))] 
        public partial MovieCardDto ToCardDto(Movie movie);

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieDetailsDto.PosterUrl), Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieDetailsDto.Genres), Use = nameof(MapGenresList))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieDetailsDto.Actors), Use = nameof(MapActorsList))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieDetailsDto.Directors), Use = nameof(MapDirectorsList))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieDetailsDto.Languages), Use = nameof(MapLanguagesList))]
        [MapProperty(nameof(Movie.MovieFeatures), nameof(MovieDetailsDto.Features), Use = nameof(MapFeaturesList))] 
        public partial MovieDetailsDto ToDetailsDto(Movie movie);

        [MapProperty(nameof(Movie.PosterImage), nameof(MovieFormDto.PosterUrl))]
        [MapProperty(nameof(Movie.MovieGenres), nameof(MovieFormDto.GenreIds), Use = nameof(MapGenreIds))]
        [MapProperty(nameof(Movie.MovieCasts), nameof(MovieFormDto.CastIds), Use = nameof(MapCastIds))]
        [MapProperty(nameof(Movie.MovieDirectors), nameof(MovieFormDto.DirectorIds), Use = nameof(MapDirectorIds))]
        [MapProperty(nameof(Movie.MovieLanguages), nameof(MovieFormDto.LanguageIds), Use = nameof(MapLanguageIds))]
        [MapProperty(nameof(Movie.MovieFeatures), nameof(MovieFormDto.FeatureIds), Use = nameof(MapFeatureIds))]
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
            movie.Rating = dto.Rating;
        }

        [MapProperty(nameof(Feature.Id), nameof(FeatureDto.Id))]
        [MapProperty(nameof(Feature.Name), nameof(FeatureDto.Name))]
        public partial FeatureDto ToFeatureDto(Feature feature);

        [MapProperty(nameof(Genre.GenreId), nameof(GenreDto.GenreId))]
        [MapProperty(nameof(Genre.GenreName), nameof(GenreDto.GenreName))]
        public partial GenreDto ToGenreDto(Genre genre);

        [MapProperty(nameof(Language.LanguageId), nameof(LanguageDto.LanguageId))]
        [MapProperty(nameof(Language.LanguageName), nameof(LanguageDto.LanguageName))]
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

        private List<string> MapFeaturesList(ICollection<MovieFeature> features) =>
            features?.Select(mf => mf.Feature.Name).ToList() ?? new List<string>();

        private List<int> MapGenreIds(ICollection<MovieGenre> genres) =>
            genres.Select(x => x.GenreId).ToList();

        private List<int> MapCastIds(ICollection<MovieCast> casts) =>
            casts.Select(x => x.CastId).ToList();

        private List<int> MapDirectorIds(ICollection<DirectorMovie> directors) =>
            directors.Select(x => x.DirectorId).ToList();

        private List<int> MapLanguageIds(ICollection<LanguageMovie> languages) =>
            languages.Select(x => x.LanguageId).ToList();

        private List<int> MapFeatureIds(ICollection<MovieFeature> features) =>
            features.Select(x => x.FeatureId).ToList();

        private int MapReleaseYear(DateTime date) => date.Year;
    }
}