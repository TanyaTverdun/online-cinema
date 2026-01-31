using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Models.ViewModels;

namespace onlineCinema.Controllers;

public class FeatureController : Controller
{
    private readonly IFeatureService _featureService;

    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = await _featureService.GetAllAsync();
        var viewModels = dtos.Select(d => new FeatureViewModel
        {
            FeatureId = d.FeatureId,
            FeatureName = d.FeatureName,
            FeatureDescription = d.FeatureDescription
        });
        return View(viewModels);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FeatureViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new FeatureDto
            {
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };
            await _featureService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _featureService.GetByIdAsync(id);
        if (dto == null) return NotFound();

        var model = new FeatureViewModel
        {
            FeatureId = dto.FeatureId,
            FeatureName = dto.FeatureName,
            FeatureDescription = dto.FeatureDescription
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(FeatureViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new FeatureDto
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
                FeatureDescription = model.FeatureDescription
            };
            await _featureService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}