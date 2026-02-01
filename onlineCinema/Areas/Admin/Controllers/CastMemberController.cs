using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CastMemberController : Controller
    {
        private readonly ICastMemberService _castMemberService;

        public CastMemberController(ICastMemberService castMemberService)
        {
            _castMemberService = castMemberService;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _castMemberService.GetAllAsync();

            var viewModels = dtos.Select(d => new CastMemberViewModel
            {
                CastId = d.CastId,
                CastFirstName = d.CastFirstName,
                CastLastName = d.CastLastName,
                CastMiddleName = d.CastMiddleName
            }).ToList();

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // ВИПРАВЛЕННЯ 1: Використовуємо CastMemberCreateUpdateDto
            var dto = new CastMemberCreateUpdateDto
            {
                // CastId при створенні зазвичай не потрібен, або 0
                CastFirstName = model.CastFirstName,
                CastLastName = model.CastLastName,
                CastMiddleName = model.CastMiddleName
            };

            await _castMemberService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _castMemberService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var viewModel = new CastMemberViewModel
            {
                CastId = dto.CastId,
                CastFirstName = dto.CastFirstName,
                CastLastName = dto.CastLastName,
                CastMiddleName = dto.CastMiddleName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CastMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // ВИПРАВЛЕННЯ 2: Використовуємо CastMemberCreateUpdateDto
            var dto = new CastMemberCreateUpdateDto
            {
                CastId = model.CastId,
                CastFirstName = model.CastFirstName,
                CastLastName = model.CastLastName,
                CastMiddleName = model.CastMiddleName
            };

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