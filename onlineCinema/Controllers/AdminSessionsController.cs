using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.ViewModels;
using onlineCinema.Mapping;
using onlineCinema.ViewModels;

namespace onlineCinema.Controllers
{
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
                return RedirectToAction("Index", "Sessions");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}
