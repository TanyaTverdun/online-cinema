using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

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
        var genresDto = await _genreService.GetAllAsync();
        var viewModel = _mapper.ToViewModelList(genresDto);
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
        var dto = await _genreService.GetByIdAsync(id);
        if (dto == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.ToViewModel(dto);

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
                await _genreService.AddAsync(_mapper.ToDto(viewModel));
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