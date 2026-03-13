using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Extensions;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IMovieService _movieService;
        private readonly AdminMovieMapper _mapper;
        private readonly IValidator<MovieFormViewModel> _validator;

        public MovieController(
            IPhotoService photoService,
            IMovieService movieService,
            AdminMovieMapper mapper,
            IValidator<MovieFormViewModel> validator)
        {
            _photoService = photoService;
            _movieService = movieService;
            _mapper = mapper;
            _validator = validator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moviesDto = await _movieService.GetMoviesForShowcaseAsync();
            var moviesViewModel = moviesDto
                .Select(m => _mapper
                .ToViewModel(m));
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
        public async Task<IActionResult> Create(
    MovieFormViewModel viewModel)
        {
            var validationResult =
                await _validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                ModelState.AddFluentErrors(validationResult);
            }

            if (!ModelState.IsValid)
            {
                await ConfigureViewModel(viewModel);
                return View(viewModel);
            }

            try
            {
                if (viewModel.PosterFile != null)
                {
                    var uploadResult = await _photoService.AddPhotoAsync(viewModel.PosterFile);

                    if (uploadResult.Error != null)
                    {
                        ModelState.AddModelError(string.Empty, "Помилка Cloudinary: " + uploadResult.Error.Message);
                        await ConfigureViewModel(viewModel);
                        return View(viewModel);
                    }

                    viewModel.PosterUrl = uploadResult.SecureUrl.AbsoluteUri;
                }

                var dto = _mapper.ToDto(viewModel);

                await _movieService.AddMovieAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);
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
            var validationResult =
                await _validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                ModelState.AddFluentErrors(validationResult);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.PosterFile != null)
                    {
                        if (!string.IsNullOrEmpty(viewModel.PosterUrl))
                        {
                            await _photoService.DeletePhotoAsync(viewModel.PosterUrl);
                        }
                        var uploadResult = await _photoService.AddPhotoAsync(viewModel.PosterFile);

                        if (uploadResult.Error != null)
                        {
                            ModelState.AddModelError(string.Empty, "Cloudinary Error: " + uploadResult.Error.Message);
                            await ConfigureViewModel(viewModel);
                            return View(viewModel);
                        }

                        // 2. (Опціонально) Тут можна було б видалити стару картинку, 
                        // але для цього треба знати її PublicId. Поки що просто оновимо URL:
                        viewModel.PosterUrl = uploadResult.SecureUrl.AbsoluteUri;
                    }
                    var dto = _mapper.ToDto(viewModel);
                    await _movieService.UpdateMovieAsync(dto);

                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException ex)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        ex.Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        ex.Message);
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
                var movieDto = await _movieService.GetMovieForEditAsync(id);

                if (movieDto != null && !string.IsNullOrEmpty(movieDto.PosterUrl))
                {
                    await _photoService.DeletePhotoAsync(movieDto.PosterUrl);
                }
                
                await _movieService.DeleteMovieAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Delete successful"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        private async Task ConfigureViewModel(MovieFormViewModel vm)
        {
            var dropdowns = await _movieService
                .GetMovieDropdownsValuesAsync();
            _mapper.Fill(dropdowns, vm);
        }
    }
}