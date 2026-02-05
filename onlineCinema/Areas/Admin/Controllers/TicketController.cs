using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly AdminTicketViewMapping _viewMapper;

        public TicketController(ITicketService ticketService, AdminTicketViewMapping viewMapper)
        {
            _ticketService = ticketService;
            _viewMapper = viewMapper;
        }

        public async Task<IActionResult> Index(int? lastId, string? email, string? movie, DateTime? date)
        {
            var pagedResult = await _ticketService.GetTicketsForAdminAsync(lastId, email, movie, date);

            var viewModel = _viewMapper.MapWithDetails(pagedResult, lastId, email, movie, date);

            return View(viewModel);
        }
    }
}
