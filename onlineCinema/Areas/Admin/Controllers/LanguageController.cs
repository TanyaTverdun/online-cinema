using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IValidator<LanguageViewModel> _validator;
        private readonly AdminLanguageMapper _mapper;

        public LanguageController(ILanguageService languageService, IValidator<LanguageViewModel> validator, AdminLanguageMapper mapper)
        {
            _languageService = languageService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _languageService.GetAllAsync();
            return View(_mapper.ToViewModelList(dtos));
        }

        [HttpGet]
        public IActionResult Create() => View(new LanguageViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageViewModel model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(model);
            }

            await _languageService.CreateAsync(_mapper.ToDto(model));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _languageService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(_mapper.ToViewModel(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LanguageViewModel model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(model);
            }

            await _languageService.UpdateAsync(_mapper.ToDto(model));
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