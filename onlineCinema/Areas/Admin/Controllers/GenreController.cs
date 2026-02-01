using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;
using onlineCinema.Application.DTOs.Genre;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly AdminGenreMapper _mapper;
        private readonly IValidator<GenreFormViewModel> _validator;

        public GenreController(IGenreService genreService, AdminGenreMapper mapper, IValidator<GenreFormViewModel> validator)
        {
            _genreService = genreService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Отримуємо GenreDto (читання)
            var genresDto = await _genreService.GetAllAsync();

            // 2. АДАПТАЦІЯ: Мапер очікує GenreFormDto, тому конвертуємо вручну
            var formDtos = genresDto.Select(g => new GenreFormDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName
            });

            // 3. Тепер мапер щасливий
            var viewModel = _mapper.ToViewModelList(formDtos);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GenreFormViewModel();
            return View("GenreValueInput", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // 1. Отримуємо GenreDto
            var dto = await _genreService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            // 2. АДАПТАЦІЯ: Конвертуємо в GenreFormDto для мапера
            var formDto = new GenreFormDto
            {
                GenreId = dto.GenreId,
                GenreName = dto.GenreName
            };

            var viewModel = _mapper.ToViewModel(formDto);

            return View("GenreValueInput", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreFormViewModel viewModel)
            => await SaveGenre(viewModel);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenreFormViewModel viewModel)
            => await SaveGenre(viewModel);

        private async Task<IActionResult> SaveGenre(GenreFormViewModel viewModel)
        {
            var result = await _validator.ValidateAsync(viewModel);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                if (viewModel.GenreId == 0)
                {
                    // ВИПРАВЛЕНО: AddAsync -> CreateAsync
                    await _genreService.CreateAsync(_mapper.ToDto(viewModel));
                }
                else
                {
                    await _genreService.UpdateAsync(_mapper.ToDto(viewModel));
                }

                return RedirectToAction(nameof(Index));
            }
            return View("GenreValueInput", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}