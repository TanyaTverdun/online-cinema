using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;

        public AdminSessionsController(
            ISessionService sessionService,
            IMovieService movieService,
            IHallService hallService)
        {
            _sessionService = sessionService;
            _movieService = movieService;
            _hallService = hallService;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return View(sessions);
        }

        // ✅ GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateSessionViewModel
            {
                Movies = await GetMoviesSelectListAsync(),
                Halls = await GetHallsSelectListAsync(),

                ShowingDateTime = DateTime.Now
                    .AddHours(1)
                    .AddSeconds(-DateTime.Now.Second)
                    .AddMilliseconds(-DateTime.Now.Millisecond),

                BasePrice = 0
            };

            return View(model);
        }

        // ✅ POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSessionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = new SessionCreateDto
                    {
                        MovieId = model.MovieId,
                        HallId = model.HallId,
                        ShowingDateTime = model.ShowingDateTime,
                        BasePrice = model.BasePrice,
                        GenerateForWeek = model.GenerateForWeek
                    };

                    await _sessionService.CreateSessionAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (InvalidOperationException ex)
            {
                // бізнес-помилка (feature mismatch, hall busy, etc.)
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            // 👇 ВСЕГДА відновлюємо dropdown-и
            await RestoreDropdownsAsync(model);

            return View(model);
        }


        // =========================
        // 🔽 HELPERS
        // =========================

        private async Task RestoreDropdownsAsync(CreateSessionViewModel model)
        {
            model.Movies = await GetMoviesSelectListAsync();
            model.Halls = await GetHallsSelectListAsync();
        }

        private async Task<List<SelectListItem>> GetMoviesSelectListAsync()
        {
            var movies = await _movieService.GetAllMoviesAsync();

            return movies.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Title
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetHallsSelectListAsync()
        {
            var halls = await _hallService.GetAllHallsAsync();

            return halls.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = $"Hall №{h.HallNumber}"
            }).ToList();
        }
    }
}
