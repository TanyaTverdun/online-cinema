using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly IValidator<FeatureViewModel> _validator;
        private readonly AdminFeatureMapper _mapper;

        public FeatureController(
            IFeatureService featureService,
            IValidator<FeatureViewModel> validator,
            AdminFeatureMapper mapper)
        {
            _featureService = featureService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _featureService.GetAllAsync();
            return View(_mapper.ToViewModelList(dtos));
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

            await _featureService.CreateAsync(_mapper.ToDto(model));
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

            return View(_mapper.ToViewModel(dto));
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

            await _featureService.UpdateAsync(_mapper.ToDto(model));
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