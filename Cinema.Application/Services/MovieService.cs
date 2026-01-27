using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
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

        public MovieService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                .Select(m => m.ToCardDto());
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(id);
            if (movie == null || movie.Status == MovieStatus.Archived) return null;

            return movie.ToDetailsDto();
        }

        public async Task<MovieFormDto?> GetMovieForEditAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(id);
            if (movie == null) return null;

            return movie.ToFormDto();
        }

        public async Task AddMovieAsync(MovieFormDto model)
        {
            var movie = model.ToEntity();

            if (model.PosterFile != null)
            {
                movie.PosterImage = await SaveImageAsync(model.PosterFile);
            }

           
            var allGenreIds = new HashSet<int>(model.GenreIds);

            if (!string.IsNullOrWhiteSpace(model.GenresInput))
            {
                var names = model.GenresInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var name in names)
                {
                  
                    var existing = (await _unitOfWork.Genre.GetAllAsync(x => x.GenreName.ToLower() == name.ToLower())).FirstOrDefault();

                    if (existing != null)
                    {
                        allGenreIds.Add(existing.GenreId); 
                    }
                    else
                    {
                        var newEntity = new Genre { GenreName = name };
                        await _unitOfWork.Genre.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync(); 
                        allGenreIds.Add(newEntity.GenreId);
                    }
                }
            }
         
            foreach (var id in allGenreIds) movie.MovieGenres.Add(new MovieGenre { GenreId = id });


            var allActorIds = new HashSet<int>(model.CastIds);

            if (!string.IsNullOrWhiteSpace(model.ActorsInput))
            {
                var names = model.ActorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var fullName in names)
                {
                    var parts = fullName.Split(' ');
                    var first = parts[0];
                    var last = parts.Length > 1 ? parts[1] : "";

                    var existing = (await _unitOfWork.CastMember.GetAllAsync(x => x.CastFirstName.ToLower() == first.ToLower() && x.CastLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null)
                    {
                        allActorIds.Add(existing.CastId);
                    }
                    else
                    {
                        var newEntity = new CastMember { CastFirstName = first, CastLastName = last };
                        await _unitOfWork.CastMember.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allActorIds.Add(newEntity.CastId);
                    }
                }
            }
            foreach (var id in allActorIds) movie.MovieCasts.Add(new MovieCast { CastId = id });


           
            var allDirectorIds = new HashSet<int>(model.DirectorIds);
            if (!string.IsNullOrWhiteSpace(model.DirectorsInput))
            {
                var names = model.DirectorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var fullName in names)
                {
                    var parts = fullName.Split(' ');
                    var first = parts[0];
                    var last = parts.Length > 1 ? parts[1] : "";

                    var existing = (await _unitOfWork.Director.GetAllAsync(x => x.DirectorFirstName.ToLower() == first.ToLower() && x.DirectorLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null) allDirectorIds.Add(existing.DirectorId);
                    else
                    {
                        var newEntity = new Director { DirectorFirstName = first, DirectorLastName = last };
                        await _unitOfWork.Director.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allDirectorIds.Add(newEntity.DirectorId);
                    }
                }
            }
            foreach (var id in allDirectorIds) movie.MovieDirectors.Add(new DirectorMovie { DirectorId = id });


         
            var allLangIds = new HashSet<int>(model.LanguageIds);
            if (!string.IsNullOrWhiteSpace(model.LanguagesInput))
            {
                var names = model.LanguagesInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var name in names)
                {
                    var existing = (await _unitOfWork.Language.GetAllAsync(x => x.LanguageName.ToLower() == name.ToLower())).FirstOrDefault();

                    if (existing != null) allLangIds.Add(existing.LanguageId);
                    else
                    {
                        var newEntity = new Language { LanguageName = name };
                        await _unitOfWork.Language.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allLangIds.Add(newEntity.LanguageId);
                    }
                }
            }
            foreach (var id in allLangIds) movie.MovieLanguages.Add(new LanguageMovie { LanguageId = id });

           
            await _unitOfWork.Movie.AddAsync(movie);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateMovieAsync(MovieFormDto model)
        {
           
            var movieFromDb = await _unitOfWork.Movie.GetByIdWithAllDetailsAsync(model.Id);
            if (movieFromDb == null) return;

           
            if (model.PosterFile != null)
            {
                if (!string.IsNullOrEmpty(movieFromDb.PosterImage)) DeleteImage(movieFromDb.PosterImage);
                movieFromDb.PosterImage = await SaveImageAsync(model.PosterFile);
            }

         
            movieFromDb.Title = model.Title;
            movieFromDb.Description = model.Description;
            movieFromDb.ReleaseDate = model.ReleaseDate;
            movieFromDb.Runtime = model.Runtime;
            movieFromDb.Status = model.Status;
            movieFromDb.AgeRating = (AgeRating)model.AgeRating; 
            movieFromDb.TrailerLink = model.TrailerLink;

            movieFromDb.MovieGenres.Clear(); 
            var allGenreIds = new HashSet<int>(model.GenreIds); 

            if (!string.IsNullOrWhiteSpace(model.GenresInput))
            {
                var names = model.GenresInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var name in names)
                {
                    var existing = (await _unitOfWork.Genre.GetAllAsync(x => x.GenreName.ToLower() == name.ToLower())).FirstOrDefault();
                    if (existing != null) allGenreIds.Add(existing.GenreId);
                    else
                    {
                        var newEntity = new Genre { GenreName = name };
                        await _unitOfWork.Genre.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allGenreIds.Add(newEntity.GenreId);
                    }
                }
            }
            foreach (var id in allGenreIds) movieFromDb.MovieGenres.Add(new MovieGenre { GenreId = id });

          
            movieFromDb.MovieCasts.Clear();
            var allActorIds = new HashSet<int>(model.CastIds);

            if (!string.IsNullOrWhiteSpace(model.ActorsInput))
            {
                var names = model.ActorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var fullName in names)
                {
                    var parts = fullName.Split(' ');
                    var first = parts[0];
                    var last = parts.Length > 1 ? parts[1] : "";
                    var existing = (await _unitOfWork.CastMember.GetAllAsync(x => x.CastFirstName.ToLower() == first.ToLower() && x.CastLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null) allActorIds.Add(existing.CastId);
                    else
                    {
                        var newEntity = new CastMember { CastFirstName = first, CastLastName = last };
                        await _unitOfWork.CastMember.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allActorIds.Add(newEntity.CastId);
                    }
                }
            }
            foreach (var id in allActorIds) movieFromDb.MovieCasts.Add(new MovieCast { CastId = id });

          
            movieFromDb.MovieDirectors.Clear();
            var allDirectorIds = new HashSet<int>(model.DirectorIds);
            if (!string.IsNullOrWhiteSpace(model.DirectorsInput))
            {
                var names = model.DirectorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var fullName in names)
                {
                    var parts = fullName.Split(' ');
                    var first = parts[0];
                    var last = parts.Length > 1 ? parts[1] : "";
                    var existing = (await _unitOfWork.Director.GetAllAsync(x => x.DirectorFirstName.ToLower() == first.ToLower() && x.DirectorLastName.ToLower() == last.ToLower())).FirstOrDefault();

                    if (existing != null) allDirectorIds.Add(existing.DirectorId);
                    else
                    {
                        var newEntity = new Director { DirectorFirstName = first, DirectorLastName = last };
                        await _unitOfWork.Director.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allDirectorIds.Add(newEntity.DirectorId);
                    }
                }
            }
            foreach (var id in allDirectorIds) movieFromDb.MovieDirectors.Add(new DirectorMovie { DirectorId = id });

            movieFromDb.MovieLanguages.Clear();
            var allLangIds = new HashSet<int>(model.LanguageIds);
            if (!string.IsNullOrWhiteSpace(model.LanguagesInput))
            {
                var names = model.LanguagesInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var name in names)
                {
                    var existing = (await _unitOfWork.Language.GetAllAsync(x => x.LanguageName.ToLower() == name.ToLower())).FirstOrDefault();
                    if (existing != null) allLangIds.Add(existing.LanguageId);
                    else
                    {
                        var newEntity = new Language { LanguageName = name };
                        await _unitOfWork.Language.AddAsync(newEntity);
                        await _unitOfWork.SaveAsync();
                        allLangIds.Add(newEntity.LanguageId);
                    }
                }
            }
            foreach (var id in allLangIds) movieFromDb.MovieLanguages.Add(new LanguageMovie { LanguageId = id });

           
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

      

        private void UpdateCollections(Movie movie, MovieFormDto model)
        {
         
            movie.MovieGenres.Clear();
            foreach (var id in model.GenreIds)
                movie.MovieGenres.Add(new MovieGenre { GenreId = id, MovieId = movie.Id });

            movie.MovieCasts.Clear();
            foreach (var id in model.CastIds)
                movie.MovieCasts.Add(new MovieCast { CastId = id, MovieId = movie.Id });

            
            movie.MovieDirectors.Clear();
            foreach (var id in model.DirectorIds)
                movie.MovieDirectors.Add(new DirectorMovie { DirectorId = id, MovieId = movie.Id });

            movie.MovieLanguages.Clear();
            foreach (var id in model.LanguageIds)
                movie.MovieLanguages.Add(new LanguageMovie { LanguageId = id, MovieId = movie.Id });
        }

        private async Task<string> SaveImageAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(wwwRootPath, @"images\movies");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return @"\images\movies\" + fileName;
        }

        private void DeleteImage(string imageUrl)
        {
           
            if (imageUrl.Contains("no-poster.png")) return;

            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\', '/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}