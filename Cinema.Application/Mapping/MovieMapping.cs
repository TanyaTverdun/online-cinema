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
        [MapProperty(nameof(Performance.MovieFeatures), 
            nameof(MovieDto.FeatureIds), 
            Use = nameof(MapFeaturesToIds))]
        public partial MovieDto MapToDto(Performance movie);

        public partial IEnumerable<MovieDto> 
            MapToDtoList(IEnumerable<Performance> movies);

        private const string PlaceholderImage = "/images/no-poster.png";

        [MapProperty(nameof(Performance.PosterImage), 
            nameof(MovieCardDto.PosterUrl), 
            Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Performance.MovieGenres), 
            nameof(MovieCardDto.GenreSummary), 
            Use = nameof(MapGenreSummary))]
        [MapProperty(nameof(Performance.ReleaseDate), 
            nameof(MovieCardDto.ReleaseYear), 
            Use = nameof(MapReleaseYear))]
        [MapProperty(nameof(Performance.AgeRating), 
            nameof(MovieCardDto.AgeRating))]
        [MapProperty(nameof(Performance.MovieFeatures), 
            nameof(MovieCardDto.Features), 
            Use = nameof(MapFeaturesList))] 
        public partial MovieCardDto ToCardDto(Performance movie);

        [MapProperty(nameof(Performance.PosterImage), 
            nameof(MovieDetailsDto.PosterUrl), 
            Use = nameof(MapPosterUrl))]
        [MapProperty(nameof(Performance.MovieGenres), 
            nameof(MovieDetailsDto.Genres), 
            Use = nameof(MapGenresList))]
        [MapProperty(nameof(Performance.MovieCasts), 
            nameof(MovieDetailsDto.Actors), 
            Use = nameof(MapActorsList))]
        [MapProperty(nameof(Performance.MovieDirectors), 
            nameof(MovieDetailsDto.Directors), 
            Use = nameof(MapDirectorsList))]
        [MapProperty(nameof(Performance.MovieLanguages), 
            nameof(MovieDetailsDto.Languages), 
            Use = nameof(MapLanguagesList))]
        [MapProperty(nameof(Performance.MovieFeatures), 
            nameof(MovieDetailsDto.Features), 
            Use = nameof(MapFeaturesList))] 
        public partial MovieDetailsDto ToDetailsDto(Performance movie);

        [MapProperty(nameof(Performance.PosterImage), 
            nameof(MovieFormDto.PosterUrl))]
        [MapProperty(nameof(Performance.MovieGenres), 
            nameof(MovieFormDto.GenreIds), 
            Use = nameof(MapGenreIds))]
        [MapProperty(nameof(Performance.MovieCasts), 
            nameof(MovieFormDto.CastIds), 
            Use = nameof(MapCastIds))]
        [MapProperty(nameof(Performance.MovieDirectors), 
            nameof(MovieFormDto.DirectorIds), 
            Use = nameof(MapDirectorIds))]
        [MapProperty(nameof(Performance.MovieLanguages), 
            nameof(MovieFormDto.LanguageIds), 
            Use = nameof(MapLanguageIds))]
        [MapProperty(nameof(Performance.MovieFeatures), 
            nameof(MovieFormDto.FeatureIds), 
            Use = nameof(MapFeatureIds))]
        public partial MovieFormDto ToFormDto(Performance movie);

        public partial Performance ToEntity(MovieFormDto dto);

        public void UpdateEntityFromDto(Performance movie, MovieFormDto dto)
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

        [MapProperty(nameof(Requriment.Id), nameof(FeatureDto.Id))]
        [MapProperty(nameof(Requriment.Name), nameof(FeatureDto.Name))]
        public partial FeatureDto ToFeatureDto(Requriment feature);

        [MapProperty(nameof(DanceStyle.GenreId), 
            nameof(GenreDto.GenreId))]
        [MapProperty(nameof(DanceStyle.GenreName), 
            nameof(GenreDto.GenreName))]
        public partial GenreDto ToGenreDto(DanceStyle genre);

        [MapProperty(nameof(SkillLevel.LanguageId), 
            nameof(LanguageDto.LanguageId))]
        [MapProperty(nameof(SkillLevel.LanguageName), 
            nameof(LanguageDto.LanguageName))]
        public partial LanguageDto ToLanguageDto(SkillLevel language);

        public PersonDto ToPersonDto(Dancer actor) =>
            new PersonDto { Id = actor.CastId, FullName = 
                $"{actor.CastFirstName} {actor.CastLastName}" };

        public PersonDto ToPersonDto(Choreographer director) =>
            new PersonDto { Id = director.DirectorId, FullName = 
                $"{director.DirectorFirstName} {director.DirectorLastName}" };

        private string MapPosterUrl(string? posterImage) =>
            string.IsNullOrEmpty(posterImage) ? PlaceholderImage : posterImage;

        private string MapGenreSummary(ICollection<PerformanceStyle> genres) =>
            genres == null ? "" : string.Join(", ", genres
                .Select(mg => mg.Genre.GenreName).Take(2));

        private List<string> MapGenresList(ICollection<PerformanceStyle> genres) =>
            genres.Select(mg => mg.Genre.GenreName).ToList();

        private List<string> MapActorsList(ICollection<PerformanceDancers> casts) =>
            casts.Select(mc => $"{mc.CastMember.CastFirstName} " +
            $"{mc.CastMember.CastLastName}").ToList();

        private List<string> MapDirectorsList(ICollection<ChoreographerPerformance> directors) =>
            directors.Select(md => $"{md.Director.DirectorFirstName} " +
            $"{md.Director.DirectorLastName}").ToList();

        private List<string> MapLanguagesList(ICollection<PerformanceLevel> languages) =>
            languages.Select(ml => ml.Language.LanguageName).ToList();

        private List<string> MapFeaturesList(ICollection<PerformanceRequirement> features) =>
            features?.Select(mf => mf.Feature.Name).ToList() ?? new List<string>();

        private List<int> MapGenreIds(ICollection<PerformanceStyle> genres) =>
            genres.Select(x => x.GenreId).ToList();

        private List<int> MapCastIds(ICollection<PerformanceDancers> casts) =>
            casts.Select(x => x.CastId).ToList();

        private List<int> MapDirectorIds(ICollection<ChoreographerPerformance> directors) =>
            directors.Select(x => x.DirectorId).ToList();

        private List<int> MapLanguageIds(ICollection<PerformanceLevel> languages) =>
            languages.Select(x => x.LanguageId).ToList();

        private List<int> MapFeatureIds(ICollection<PerformanceRequirement> features) =>
            features.Select(x => x.FeatureId).ToList();

        private int MapReleaseYear(DateTime date) => date.Year;

        private List<int> MapFeaturesToIds(ICollection<PerformanceRequirement> features)
            => features.Select(f => f.FeatureId).ToList();
    }
}