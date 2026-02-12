using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Mapping;
using Microsoft.AspNetCore.Mvc.Rendering;
using onlineCinema.Areas.Admin.Models;
namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;
        private readonly SessionViewModelMapper _sessionMapper;

        public SessionController(
            ISessionService sessionService, 
            IMovieService movieService,
            IHallService hallService,
            SessionViewModelMapper sessionMapper)
        {
            _sessionService = sessionService;
            _movieService = movieService;
            _hallService = hallService;
            _sessionMapper = sessionMapper;
        }

        public async Task<IActionResult> Index()
        {
            var sessionDtos = await _sessionService.GetAllSessionsAsync();
            var viewModels = _sessionMapper
                .MapToListViewModelList(sessionDtos);

            return View("AllSessions", viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new SessionViewModel();
            await PopulateViewModelDropdowns(viewModel);
            return View("Session", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateViewModelDropdowns(vm);
                return View("Session", vm);
            }

            try
            {
                await _sessionService.CreateSessionAsync(
                    _sessionMapper.MapToCreateDto(vm));

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(
                    "ShowingDateTime",
                    ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);
            }

            await PopulateViewModelDropdowns(vm);

            return View("Session", vm);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            var editViewModel = _sessionMapper.MapToEditViewModel(session);

            await PopulateViewModelDropdowns(editViewModel);

            return View("Session", editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SessionViewModel model)
        {
            if (model.Id == null || model.Id == 0)
            {
                model.Id = id;
            }

            if (!ModelState.IsValid)
            {
                await PopulateViewModelDropdowns(model);
                return View("Session", model);
            }

            var conflict = await _sessionService.HallHasSessionAtTime(
                model.HallId ?? 0,
                model.ShowingDateTime ?? DateTime.Now,
                model.MovieId ?? 0,
                model.Id ?? 0);

            if (conflict)
            {
                ModelState.AddModelError(
                     "ShowingDateTime",
                        "У цьому залі вже є інший сеанс у цей час");


                await PopulateViewModelDropdowns(model);
                return View("Session", model);
            }

            var updateDto = _sessionMapper.MapToUpdateDto(model);

            await _sessionService.UpdateSessionAsync(updateDto);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sessionService.DeleteSessionAsync(id);

                TempData["SuccessMessage"] =
                    "Сеанс успішно видалено.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }


        private async Task PopulateViewModelDropdowns(SessionViewModel vm)
        {
            vm.MoviesList = await _movieService.GetAllMoviesAsync();
            vm.HallsList = await _hallService.GetAllHallsAsync();
        }
    }
}
