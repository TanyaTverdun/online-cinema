using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly IValidator<FeatureViewModel> _validator;

        public FeatureController(IFeatureService featureService, IValidator<FeatureViewModel> validator)
        {
            _featureService = featureService;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _featureService.GetAllAsync();

            var viewModels = dtos.Select(d => new FeatureViewModel
            {
                FeatureId = d.FeatureId,
                FeatureName = d.FeatureName,
                FeatureDescription = d.FeatureDescription
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FeatureViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureViewModel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var dto = new FeatureDto
            {
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };

            await _featureService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _featureService.GetByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            var viewModel = new FeatureViewModel
            {
                FeatureId = dto.FeatureId,
                FeatureName = dto.FeatureName,
                FeatureDescription = dto.FeatureDescription
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FeatureViewModel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var dto = new FeatureDto
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };

            await _featureService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}