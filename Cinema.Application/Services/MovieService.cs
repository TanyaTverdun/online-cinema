using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MovieMapping _mapper;

        public MovieService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, MovieMapping mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieCardDto>> GetMoviesForShowcaseAsync()
        {
            var movies = await _unitOfWork.Movie.GetAllAsync(
                filter: m => m.Status != MovieStatus.Archived,
                includeProperties: "MovieGenres.Genre"
            );

            return movies
                .OrderBy(m => m.Status)
                .ThenByDescending(m => m.ReleaseDate)
                .Select(m => _mapper.ToCardDto(m));
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(id);
            if (movie == null || movie.Status == MovieStatus.Archived)
            {
                return null;
            }

            return _mapper.ToDetailsDto(movie);
        }

        public async Task<MovieFormDto?> GetMovieForEditAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(id);
            if (movie == null)
            {
                return null;
            }

            return _mapper.ToFormDto(movie);
        }

        public async Task<MovieDropdownsDto> GetMovieDropdownsValuesAsync()
        {
            var response = new MovieDropdownsDto();

            var genres = await _unitOfWork.Genre.GetAllAsync();
            response.Genres = genres.Select(g => _mapper.ToGenreDto(g)).ToList();

            var actors = await _unitOfWork.CastMember.GetAllAsync();
            response.Actors = actors.Select(a => _mapper.ToPersonDto(a)).ToList();

            var directors = await _unitOfWork.Director.GetAllAsync();
            response.Directors = directors.Select(d => _mapper.ToPersonDto(d)).ToList();

            var languages = await _unitOfWork.Language.GetAllAsync();
            response.Languages = languages.Select(l => _mapper.ToLanguageDto(l)).ToList();

            return response;
        }

        public async Task AddMovieAsync(MovieFormDto model)
        {
            var movie = _mapper.ToEntity(model);

            if (model.PosterFile != null)
            {
                movie.PosterImage = await SaveImageAsync(model.PosterFile);
            }

            await ProcessGenresAsync(movie, model);
            await ProcessActorsAsync(movie, model);
            await ProcessDirectorsAsync(movie, model);
            await ProcessLanguagesAsync(movie, model);

            await _unitOfWork.Movie.AddAsync(movie);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateMovieAsync(MovieFormDto model)
        {
            var movieFromDb = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(model.Id);
            if (movieFromDb == null)
            {
                return;
            }

            if (model.PosterFile != null)
            {
                if (!string.IsNullOrEmpty(movieFromDb.PosterImage))
                {
                    DeleteImage(movieFromDb.PosterImage);
                }
                movieFromDb.PosterImage = await SaveImageAsync(model.PosterFile);
            }

            _mapper.UpdateEntityFromDto(movieFromDb, model);

            movieFromDb.MovieGenres.Clear();
            await ProcessGenresAsync(movieFromDb, model);

            movieFromDb.MovieCasts.Clear();
            await ProcessActorsAsync(movieFromDb, model);

            movieFromDb.MovieDirectors.Clear();
            await ProcessDirectorsAsync(movieFromDb, model);

            movieFromDb.MovieLanguages.Clear();
            await ProcessLanguagesAsync(movieFromDb, model);

            _unitOfWork.Movie.Update(movieFromDb);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(id);
            if (movie != null)
            {
                movie.Status = MovieStatus.Archived;
                _unitOfWork.Movie.Update(movie);
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task ProcessGenresAsync(Movie movie, MovieFormDto model)
        {
            var allIds = new HashSet<int>(model.GenreIds);

            if (!string.IsNullOrWhiteSpace(model.GenresInput))
            {
                var names = SplitInput(model.GenresInput);
                foreach (var name in names)
                {
                    var existing = (await _unitOfWork.Genre.GetAllAsync(x => x.GenreName.ToLower() == name.ToLower())).FirstOrDefault();
                    if (existing != null)
                    {
                        allIds.Add(existing.GenreId);
                    }
                    else
                    {
                        var newEntity = new Genre { GenreName = name };
                        await _unitOfWork.Genre.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allIds.Add(newEntity.GenreId);
                    }
                }
            }

            foreach (var id in allIds)
            {
                movie.MovieGenres.Add(new MovieGenre { GenreId = id });
            }
        }

        private async Task ProcessActorsAsync(Movie movie, MovieFormDto model)
        {
            var allIds = new HashSet<int>(model.CastIds);

            if (!string.IsNullOrWhiteSpace(model.ActorsInput))
            {
                var names = SplitInput(model.ActorsInput);
                foreach (var fullName in names)
                {
                    var (first, last) = ParseName(fullName);
                    var existing = (await _unitOfWork.CastMember.GetAllAsync(x => x.CastFirstName.ToLower() == first.ToLower() && x.CastLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null)
                    {
                        allIds.Add(existing.CastId);
                    }
                    else
                    {
                        var newEntity = new CastMember { CastFirstName = first, CastLastName = last };
                        await _unitOfWork.CastMember.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allIds.Add(newEntity.CastId);
                    }
                }
            }

            foreach (var id in allIds)
            {
                movie.MovieCasts.Add(new MovieCast { CastId = id });
            }
        }

        private async Task ProcessDirectorsAsync(Movie movie, MovieFormDto model)
        {
            var allIds = new HashSet<int>(model.DirectorIds);

            if (!string.IsNullOrWhiteSpace(model.DirectorsInput))
            {
                var names = SplitInput(model.DirectorsInput);
                foreach (var fullName in names)
                {
                    var (first, last) = ParseName(fullName);
                    var existing = (await _unitOfWork.Director.GetAllAsync(x => x.DirectorFirstName.ToLower() == first.ToLower() && x.DirectorLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null)
                    {
                        allIds.Add(existing.DirectorId);
                    }
                    else
                    {
                        var newEntity = new Director { DirectorFirstName = first, DirectorLastName = last };
                        await _unitOfWork.Director.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allIds.Add(newEntity.DirectorId);
                    }
                }
            }

            foreach (var id in allIds)
            {
                movie.MovieDirectors.Add(new DirectorMovie { DirectorId = id });
            }
        }

        private async Task ProcessLanguagesAsync(Movie movie, MovieFormDto model)
        {
            var allIds = new HashSet<int>(model.LanguageIds);

            if (!string.IsNullOrWhiteSpace(model.LanguagesInput))
            {
                var names = SplitInput(model.LanguagesInput);
                foreach (var name in names)
                {
                    var existing = (await _unitOfWork.Language.GetAllAsync(x => x.LanguageName.ToLower() == name.ToLower())).FirstOrDefault();
                    if (existing != null)
                    {
                        allIds.Add(existing.LanguageId);
                    }
                    else
                    {
                        var newEntity = new Language { LanguageName = name };
                        await _unitOfWork.Language.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allIds.Add(newEntity.LanguageId);
                    }
                }
            }

            foreach (var id in allIds)
            {
                movie.MovieLanguages.Add(new LanguageMovie { LanguageId = id });
            }
        }

        private string[] SplitInput(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        private (string First, string Last) ParseName(string fullName)
        {
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return ("", "");
            }

            var first = parts[0];
            var last = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";
            return (first, last);
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(wwwRootPath, @"images\movies");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return @"\images\movies\" + fileName;
        }

        private void DeleteImage(string imageUrl)
        {
            if (imageUrl.Contains("no-poster.png"))
            {
                return;
            }

            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\', '/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}