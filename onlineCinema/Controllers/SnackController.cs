using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Models.ViewModels;

namespace onlineCinema.Controllers;

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
        });
        return View(viewModels);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SnackViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new SnackDto
            {
                SnackName = model.SnackName,
                Price = model.Price
            };
            await _snackService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _snackService.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var model = new SnackViewModel
        {
            SnackId = dto.SnackId,
            SnackName = dto.SnackName,
            Price = dto.Price
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SnackViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new SnackDto
            {
                SnackId = model.SnackId,
                SnackName = model.SnackName,
                Price = model.Price
            };
            await _snackService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}