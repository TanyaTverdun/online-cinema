using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces; // Тут лежить IFeatureService
using onlineCinema.Areas.Admin.Models; // Тут лежить FeatureViewModel

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")] // <--- ЦЕЙ АТРИБУТ ОБОВ'ЯЗКОВИЙ
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        // GET: Admin/Feature/Index
        public async Task<IActionResult> Index()
        {
            // 1. Отримуємо дані (DTO) з сервісу
            var dtos = await _featureService.GetAllAsync();

            // 2. Перетворюємо DTO у ViewModel для відображення
            var viewModels = dtos.Select(d => new FeatureViewModel
            {
                FeatureId = d.FeatureId,
                FeatureName = d.FeatureName,
                FeatureDescription = d.FeatureDescription
            }).ToList();

            return View(viewModels);
        }

        // GET: Admin/Feature/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Відкриваємо порожню форму
            return View(new FeatureViewModel());
        }

        // POST: Admin/Feature/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Мапимо ViewModel -> DTO для відправки в сервіс
            var dto = new FeatureDto
            {
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };

            await _featureService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Feature/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _featureService.GetByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            // Мапимо DTO -> ViewModel для форми редагування
            var viewModel = new FeatureViewModel
            {
                FeatureId = dto.FeatureId,
                FeatureName = dto.FeatureName,
                FeatureDescription = dto.FeatureDescription
            };

            return View(viewModel);
        }

        // POST: Admin/Feature/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FeatureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Мапимо ViewModel -> DTO
            var dto = new FeatureDto
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };

            await _featureService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Feature/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}