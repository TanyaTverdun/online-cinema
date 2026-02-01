using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;
        private readonly AdminDirectorMapper _mapper;
        private readonly IValidator<DirectorFormViewModel> _validator;

        public DirectorController(IDirectorService directorService, AdminDirectorMapper mapper, IValidator<DirectorFormViewModel> validator)
        {
            _directorService = directorService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var directorsDto = await _directorService.GetAllAsync();
            var viewModel = _mapper.ToViewModelList(directorsDto);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create() => View(new DirectorFormViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorFormViewModel viewModel)
        {
            var result = await _validator.ValidateAsync(viewModel);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                await _directorService.AddAsync(_mapper.ToDto(viewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _directorService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            return View(_mapper.ToViewModel(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DirectorFormViewModel viewModel)
        {
            var result = await _validator.ValidateAsync(viewModel);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                await _directorService.UpdateAsync(_mapper.ToDto(viewModel));
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _directorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}