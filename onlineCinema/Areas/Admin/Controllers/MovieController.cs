using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Application.Interfaces;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IMovieService movieService, IUnitOfWork unitOfWork)
        {
            _movieService = movieService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMoviesForShowcaseAsync();
            return View(movies);
        }

    
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropDownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormDto model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    await _movieService.AddMovieAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", $"System Error: {ex.Message}");

                   
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Details: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                
                ModelState.AddModelError("", "Validation Failed. Please check inputs.");
            }

          
            await PopulateDropDownsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movieForm = await _movieService.GetMovieForEditAsync(id);
            if (movieForm == null) return NotFound();

            await PopulateDropDownsAsync();
            return View(movieForm); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieFormDto model)
        {
            if (ModelState.IsValid)
            {
                await _movieService.UpdateMovieAsync(model);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropDownsAsync();
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task PopulateDropDownsAsync()
        {
            var genres = await _unitOfWork.Genre.GetAllAsync();
            var actors = await _unitOfWork.CastMember.GetAllAsync();
            var directors = await _unitOfWork.Director.GetAllAsync();
            var languages = await _unitOfWork.Language.GetAllAsync();

            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");

            
            var actorList = actors.Select(a => new
            {
                CastId = a.CastId,
                FullName = $"{a.CastFirstName} {a.CastLastName}" 
            });
            ViewBag.Actors = new SelectList(actorList, "CastId", "FullName");

            
            var directorList = directors.Select(d => new
            {
                DirectorId = d.DirectorId,
                FullName = $"{d.DirectorFirstName} {d.DirectorLastName}"
            });
            ViewBag.Directors = new SelectList(directorList, "DirectorId", "FullName");

            ViewBag.Languages = new SelectList(languages, "LanguageId", "LanguageName");
        }
    }
}