using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Models.ViewModels;

namespace onlineCinema.Controllers;

public class LanguageController : Controller
{
    private readonly ILanguageService _languageService;

    public LanguageController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = await _languageService.GetAllAsync();
        var viewModels = dtos.Select(d => new LanguageViewModel
        {
            LanguageId = d.LanguageId,
            LanguageName = d.LanguageName
        });
        return View(viewModels);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LanguageViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new LanguageDto { LanguageName = model.LanguageName };
            await _languageService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _languageService.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var model = new LanguageViewModel { LanguageId = dto.LanguageId, LanguageName = dto.LanguageName };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LanguageViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new LanguageDto { LanguageId = model.LanguageId, LanguageName = model.LanguageName };
            await _languageService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}