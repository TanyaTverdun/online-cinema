using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new LanguageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new LanguageDto
            {
                LanguageName = model.LanguageName
            };

            await _languageService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _languageService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var viewModel = new LanguageViewModel
            {
                LanguageId = dto.LanguageId,
                LanguageName = dto.LanguageName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LanguageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new LanguageDto
            {
                LanguageId = model.LanguageId,
                LanguageName = model.LanguageName
            };

            await _languageService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _languageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}