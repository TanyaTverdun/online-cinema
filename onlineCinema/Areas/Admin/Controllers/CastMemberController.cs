using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CastMemberController : Controller
    {
        private readonly ICastMemberService _castMemberService;
        private readonly IValidator<CastMemberViewModel> _validator;
        private readonly AdminCastMemberMapper _mapper;

        public CastMemberController(
            ICastMemberService castMemberService,
            IValidator<CastMemberViewModel> validator,
            AdminCastMemberMapper mapper)
        {
            _castMemberService = castMemberService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _castMemberService.GetAllAsync();
            var viewModels = _mapper.ToViewModelList(dtos);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CastMemberViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CastMemberViewModel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        error.PropertyName,
                        error.ErrorMessage);
                }

                return View(model);
            }

            try
            {
                var dto = _mapper.ToCreateUpdateDto(model);
                await _castMemberService.CreateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var dto = await _castMemberService.GetByIdAsync(id);
                var viewModel = _mapper.ToViewModel(dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CastMemberViewModel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        error.PropertyName,
                        error.ErrorMessage);
                }

                return View(model);
            }

            try
            {
                var dto = _mapper.ToCreateUpdateDto(model);
                await _castMemberService.UpdateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _castMemberService.DeleteAsync(id);

                TempData["SuccessMessage"] =
                    "Актор успішно видалений.";
            }
            catch (KeyNotFoundException)
            {
                TempData["ErrorMessage"] =
                    "Актор не знайдений.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] =
                    ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
