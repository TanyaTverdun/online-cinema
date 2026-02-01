using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SnackController : Controller
    {
        private readonly ISnackService _snackService;

        public SnackController(ISnackService snackService)
        {
            _snackService = snackService;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _snackService.GetAllAsync();

            var viewModels = dtos.Select(d => new SnackViewModel
            {
                SnackId = d.SnackId,
                SnackName = d.SnackName,
                Price = d.Price
            }).ToList();

            return View(viewModels);
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new SnackDto
            {
                SnackName = model.SnackName,
                Price = model.Price
            };

            await _snackService.CreateAsync(dto); // Використовуємо CreateAsync

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _snackService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var viewModel = new SnackViewModel
            {
                SnackId = dto.SnackId,
                SnackName = dto.SnackName,
                Price = dto.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SnackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new SnackDto
            {
                SnackId = model.SnackId,
                SnackName = model.SnackName,
                Price = model.Price
            };

            await _snackService.UpdateAsync(dto);

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