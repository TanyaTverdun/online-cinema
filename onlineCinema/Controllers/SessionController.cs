using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using onlineCinema.Models;
using onlineCinema.ViewModels;

namespace onlineCinema.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly MovieScheduleViewModelMapper _sessionMapper;

        public SessionController(ISessionService sessionService, MovieScheduleViewModelMapper sessionMapper)
        {
            _sessionService = sessionService;
            _sessionMapper = sessionMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Schedule(int id)
        {
            try
            {
                var dto = await _sessionService.GetMovieScheduleAsync(id);

                var viewModel = _sessionMapper.MapMovieScheduleDtoToViewModel(dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
