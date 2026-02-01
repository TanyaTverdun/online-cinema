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
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var dto = _mapper.ToCreateUpdateDto(model);
            await _castMemberService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _castMemberService.GetByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.ToViewModel(dto);
            return View(viewModel);
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
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            var dto = _mapper.ToCreateUpdateDto(model);
            await _castMemberService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _castMemberService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}