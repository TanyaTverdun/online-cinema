using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Models.ViewModels;

namespace onlineCinema.Controllers;

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
        });

        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CastMemberViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new CastMemberCreateUpdateDto
            {
                CastFirstName = model.CastFirstName,
                CastLastName = model.CastLastName,
                CastMiddleName = model.CastMiddleName
            };

            await _castMemberService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _castMemberService.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var model = new CastMemberViewModel
        {
            CastId = dto.CastId,
            CastFirstName = dto.CastFirstName,
            CastLastName = dto.CastLastName,
            CastMiddleName = dto.CastMiddleName
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CastMemberViewModel model)
    {
        if (ModelState.IsValid)
        {
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
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _castMemberService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}