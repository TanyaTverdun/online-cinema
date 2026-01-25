using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Controllers
{
    public class HallController : Controller
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var halls = await _hallService.GetAllHallsAsync();
            return View(halls);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var hall = await _hallService.GetHallByIdAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            return View(hall);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Features = await _hallService.GetAllFeaturesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HallDto hallDto, List<int> selectedFeatureIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _hallService.CreateHallAsync(hallDto, selectedFeatureIds);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.Features = await _hallService.GetAllFeaturesAsync();
                    return View(hallDto);
                }
            }
            ViewBag.Features = await _hallService.GetAllFeaturesAsync();
            return View(hallDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var hall = await _hallService.GetHallByIdAsync(id);
                ViewBag.Features = await _hallService.GetAllFeaturesAsync();
                return View(hall);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HallDto hallDto, List<int> selectedFeatureIds)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Features = await _hallService.GetAllFeaturesAsync();
                return View(hallDto);
            }

            try
            {
                await _hallService.EditHallAsync(hallDto, selectedFeatureIds);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Features = await _hallService.GetAllFeaturesAsync();
                return View(hallDto);
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
