using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

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
                    var dto = viewModel.ToDto();
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
            if (dto == null)
            {
                return NotFound();
            }

            var viewModel = dto.ToViewModel();
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
                    var dto = viewModel.ToDto();
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
    }
}