    using Microsoft.AspNetCore.Mvc;
    using onlineCinema.Application.Services.Interfaces;
    using onlineCinema.ViewModels;
    using onlineCinema.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;
    namespace onlineCinema.Controllers
    {
        //[Authorize(Roles = "Admin")]
        public class AdminSessionsController : Controller
        {
            private readonly ISessionService _sessionService;
            private readonly IMovieService _movieService;
            private readonly IHallService _hallService;
            private readonly SessionViewModelMapper _sessionMapper;

            public AdminSessionsController(ISessionService sessionService, IMovieService movieService, IHallService hallService, SessionViewModelMapper sessionMapper)
            {
                _sessionService = sessionService;
                _movieService = movieService;
                _hallService = hallService;
                _sessionMapper = sessionMapper;
            }

            public async Task<IActionResult> Index()
            {
                var sessionDtos = await _sessionService.GetAllSessionsAsync();
                var viewModels = _sessionMapper.MapToListViewModelList(sessionDtos);

                return View("AllSessions", viewModels);
            }

            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var viewModel = new SessionCreateViewModel();
                await PopulateViewModelDropdowns(viewModel);
                return View("CreateSession", viewModel);
            }
        [HttpPost]
        public async Task<IActionResult> Create(SessionCreateViewModel vm)
        {
            if (vm.ShowingDateTime == null)
            {
                ModelState.AddModelError(nameof(vm.ShowingDateTime),
                    "Оберіть дату та час сеансу");
            }

            if (!ModelState.IsValid)
            {
                await PopulateViewModelDropdowns(vm);
                return View("CreateSession", vm);
            }

            try
            {
                await _sessionService.CreateSessionAsync(
                    _sessionMapper.MapToCreateDto(vm));

                TempData["Success"] = "Сеанси успішно створені";
                return RedirectToAction(nameof(Create));
            }
            catch (InvalidOperationException ex)
            {
                TempData["Warning"] = ex.Message;
                return RedirectToAction(nameof(Create));
            }
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

                return View("EditSession", editViewModel);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, SessionEditViewModel model)
            {
                if (model.Id == null || model.Id == 0)
                {
                    model.Id = id;
                }

                if (!ModelState.IsValid)
                {
                    await PopulateViewModelDropdowns(model);
                    return View("EditSession", model);
                }

                var conflict = await _sessionService.HallHasSessionAtTime(
                    model.HallId ?? 0,
                    model.ShowingDateTime ?? DateTime.Now,
                    model.MovieId ?? 0,
                    model.Id ?? 0);

                if (conflict)
                {
                    ModelState.AddModelError("ShowingDateTime", "У цьому залі вже є інший сеанс у цей час");

                    await PopulateViewModelDropdowns(model);
                    return View("EditSession", model);
                }

                var updateDto = _sessionMapper.MapToUpdateDto(model);

                await _sessionService.UpdateSessionAsync(updateDto);

                return RedirectToAction(nameof(Index));
            }

            private async Task PopulateViewModelDropdowns(SessionCreateViewModel vm)
            {
                var movies = await _movieService.GetAllMoviesAsync();
                var halls = await _hallService.GetAllHallsAsync();

                vm.AvailableMovies = new SelectList(movies, "Id", "Title", vm.MovieId);
                vm.AvailableHalls = new SelectList(halls, "Id", "HallNumber", vm.HallId);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    await _sessionService.DeleteSessionAsync(id);
                    TempData["SuccessMessage"] = "Сеанс успішно видалено.";
                }
                catch (InvalidOperationException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Сталася помилка при видаленні сеансу.";
                }

                return RedirectToAction(nameof(Index));
            }

            private async Task PopulateViewModelDropdowns(SessionEditViewModel vm)
            {
                var movies = await _movieService.GetAllMoviesAsync();
                var halls = await _hallService.GetAllHallsAsync();

                vm.AvailableMovies = new SelectList(movies, "Id", "Title", vm.MovieId);
                vm.AvailableHalls = new SelectList(halls, "Id", "HallNumber", vm.HallId);
            }
        }
    }
