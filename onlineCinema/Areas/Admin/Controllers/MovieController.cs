using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Application.Interfaces;
using onlineCinema.Areas.Admin.Models; 

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            
            var movies = await _movieService.GetMoviesForShowcaseAsync();
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            var viewModel = new MovieFormViewModel();

            
            await ConfigureViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var dto = MapToDto(viewModel);

                    await _movieService.AddMovieAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

           
            await ConfigureViewModel(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _movieService.GetMovieForEditAsync(id);
            if (dto == null) return NotFound();

           
            var viewModel = MapToViewModel(dto);

            await ConfigureViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = MapToDto(viewModel);
                    await _movieService.UpdateMovieAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            await ConfigureViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
        private async Task ConfigureViewModel(MovieFormViewModel vm)
        {
            var dropdowns = await _movieService.GetMovieDropdownsValuesAsync();

            vm.GenresList = dropdowns.Genres.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            vm.ActorsList = dropdowns.Actors.Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
            vm.DirectorsList = dropdowns.Directors.Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
            vm.LanguagesList = dropdowns.Languages.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

     
        private MovieFormDto MapToDto(MovieFormViewModel vm)
        {
            return new MovieFormDto
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                Status = vm.Status,
                AgeRating = vm.AgeRating,
                Runtime = vm.Runtime,
                ReleaseDate = vm.ReleaseDate,
                TrailerLink = vm.TrailerLink,
                PosterUrl = vm.PosterUrl,
                PosterFile = vm.PosterFile,
                GenreIds = vm.GenreIds,
                CastIds = vm.CastIds,
                DirectorIds = vm.DirectorIds,
                LanguageIds = vm.LanguageIds,
                GenresInput = vm.GenresInput,
                ActorsInput = vm.ActorsInput,
                DirectorsInput = vm.DirectorsInput,
                LanguagesInput = vm.LanguagesInput
            };
        }

       
        private MovieFormViewModel MapToViewModel(MovieFormDto dto)
        {
            return new MovieFormViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                AgeRating = dto.AgeRating,
                Runtime = dto.Runtime,
                ReleaseDate = dto.ReleaseDate,
                TrailerLink = dto.TrailerLink,
                PosterUrl = dto.PosterUrl,
              
                GenreIds = dto.GenreIds,
                CastIds = dto.CastIds,
                DirectorIds = dto.DirectorIds,
                LanguageIds = dto.LanguageIds
             
            };
        }
    }
}