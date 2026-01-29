using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.ViewModels;
using onlineCinema.Mapping;
using Microsoft.AspNetCore.Authorization;
namespace onlineCinema.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminSessionsController : Controller
    {
        private readonly ISessionService _sessionService;

        public AdminSessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ================= CREATE =================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _sessionService.CreateSessionAsync(vm.ToDto());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        // ================= EDIT =================

        // ✅ ВІДКРИТИ СТОРІНКУ
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);

            if (session == null)
                return NotFound();

            var vm = session.ToEditViewModel();

            return View(vm);
        }

        // ✅ ЗБЕРЕГТИ ЗМІНИ
        [HttpPost]
        public async Task<IActionResult> Edit(SessionEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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

            await _sessionService.UpdateSessionAsync(model.ToDto());

            return RedirectToAction(nameof(Index));
        }
    }
}
