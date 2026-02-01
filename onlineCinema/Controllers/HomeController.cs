using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Interfaces;
using System.Diagnostics;
using onlineCinema.Models;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
        
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMoviesForShowcaseAsync();
            return View(movies);
        }
       
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}