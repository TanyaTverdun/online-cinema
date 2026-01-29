using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.ViewModels;
using onlineCinema.Mapping;
namespace onlineCinema.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminSessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly SessionViewModelMapper _sessionMapper;

        public AdminSessionsController(ISessionService sessionService, SessionViewModelMapper sessionMapper)
        {
            _sessionService = sessionService;
            _sessionMapper = sessionMapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var dto = _sessionMapper.MapToCreateDto(vm);
                await _sessionService.CreateSessionAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
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

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SessionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var conflict = await _sessionService.HallHasSessionAtTime(
                model.HallId,
                model.ShowingDateTime,
                model.Id);

            if (conflict)
            {
                ModelState.AddModelError(
                    "",
                    "У цьому залі вже є інший сеанс у цей час");

                return View(model);
            }

            var updateDto = _sessionMapper.MapToUpdateDto(model);
            await _sessionService.UpdateSessionAsync(updateDto);

            return RedirectToAction(nameof(Index));
        }
    }
}
