using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;



namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly AdminMovieMapper _mapper;

        public MovieController(IMovieService movieService, AdminMovieMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moviesDto = await _movieService.GetMoviesForShowcaseAsync();
            var moviesViewModel = moviesDto.Select(m => _mapper.ToViewModel(m));
            return Json(new { data = moviesViewModel });
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
            var validator = new Validators.MovieFormValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.ToDto(viewModel);
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

            var viewModel = _mapper.ToViewModel(dto);
            await ConfigureViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieFormViewModel viewModel)
        {
            var validator = new Validators.MovieFormValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _mapper.ToDto(viewModel);
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Json(new { success = true, message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task ConfigureViewModel(MovieFormViewModel vm)
        {
            var dropdowns = await _movieService.GetMovieDropdownsValuesAsync();
            _mapper.Fill(dropdowns, vm);
        }
    }
}