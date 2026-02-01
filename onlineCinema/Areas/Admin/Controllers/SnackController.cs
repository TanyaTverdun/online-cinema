using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SnackController : Controller
    {
        private readonly ISnackService _snackService;
        private readonly IValidator<SnackViewModel> _validator;
        private readonly AdminSnackMapper _mapper;

        public SnackController(
            ISnackService snackService,
            IValidator<SnackViewModel> validator,
            AdminSnackMapper mapper)
        {
            _snackService = snackService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _snackService.GetAllAsync();
            return View(_mapper.ToViewModelList(dtos));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SnackViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SnackViewModel model)
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

            await _snackService.CreateAsync(_mapper.ToDto(model));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _snackService.GetByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            return View(_mapper.ToViewModel(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SnackViewModel model)
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

            await _snackService.UpdateAsync(_mapper.ToDto(model));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _snackService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}