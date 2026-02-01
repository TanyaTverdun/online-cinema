using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Genre;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Controllers;

public class GenreController : Controller
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = await _genreService.GetAllAsync();
        var viewModels = dtos.Select(d => new GenreViewModel
        {
            GenreId = d.GenreId,
            GenreName = d.GenreName
        });
        return View(viewModels);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new GenreFormDto { GenreName = model.GenreName };
            await _genreService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _genreService.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var model = new GenreViewModel { GenreId = dto.GenreId, GenreName = dto.GenreName };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GenreViewModel model)
    {
        if (ModelState.IsValid)
        {

            var dto = new GenreFormDto { GenreName = model.GenreName };

            await _genreService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}