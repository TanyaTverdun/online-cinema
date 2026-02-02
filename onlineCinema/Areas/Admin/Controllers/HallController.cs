using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Mapping;
using onlineCinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HallController : Controller
    {
        private readonly IHallService _hallService;
        private readonly HallViewModelMapper _mapper;

        private readonly IValidator<HallInputViewModel> _validator;

        public HallController(IHallService hallService, HallViewModelMapper mapper, IValidator<HallInputViewModel> validator)
        {
            _hallService = hallService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet] //Hall
        public async Task<IActionResult> Index()
        {
            var dtos = await _hallService.GetAllHallsAsync();

            var viewModels = dtos.Select(dto => _mapper.MapToViewModel(dto)).ToList();

            return View(viewModels);
        }

        [HttpGet] //Hall/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var dto = await _hallService.GetHallDetailsAsync(id);

                var viewModel = _mapper.MapToViewModel(dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet] //Hall/Create
        public async Task<IActionResult> Create()
        {

            var features = await _hallService.GetAllFeaturesAsync();

            var viewModel = _mapper.PrepareInputViewModel(features);

            return View(viewModel);
        }

        [HttpPost] //Hall/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HallInputViewModel model)
        {
            //чиста валідація, бо не бачить складні методи валідації
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                var features = await _hallService.GetAllFeaturesAsync();

                model.AvailableFeatures = features
                    .Select(f => new FeatureCheckboxViewModel
                    {
                        Id = f.Id,
                        Name = f.Name,
                        IsSelected = model.SelectedFeatureIds != null && model.SelectedFeatureIds.Contains(f.Id)
                    })
                    .ToList();

                return View(model);
            }

            var dto = _mapper.MapToDto(model);

            await _hallService.CreateHallAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet] //Hall/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var dto = await _hallService.GetHallByIdAsync(id);
                var features = await _hallService.GetAllFeaturesAsync();

                var viewModel = _mapper.PrepareInputViewModel(features, dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost] //Hall/Edit/{id}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HallInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var features = await _hallService.GetAllFeaturesAsync();
                model.AvailableFeatures = _mapper.PrepareInputViewModel(features).AvailableFeatures;
                //Select(f => new FeatureCheckboxViewModel
                //{
                //    Id = f.Id,
                //    Name = f.Name,
                //    IsSelected = model.SelectedFeatureIds.Contains(f.Id)
                //}).ToList();
                return View(model);
            }

            try
            {
                var dto = _mapper.MapToDto(model);
                await _hallService.EditHallAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _hallService.DeleteHallAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
