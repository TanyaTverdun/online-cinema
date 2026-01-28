using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return View(sessions);
        }

        public async Task<IActionResult> ByMovie(int movieId)
        {
            var sessions = await _sessionRepository.GetByMovieIdAsync(movieId);
            return View("Index", sessions);
        }
    }
}
