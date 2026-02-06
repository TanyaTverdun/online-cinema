using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace onlineCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly AdminPaymentMapping _viewMapper;

        public PaymentController(IPaymentService paymentService, AdminPaymentMapping viewMapper)
        {
            _paymentService = paymentService;
            _viewMapper = viewMapper;
        }

        public async Task<IActionResult> Index(int? lastId, string? email, string? movie, DateTime? date)
        {
            var pagedResult = await _paymentService.GetPaymentsForAdminAsync(lastId, email, movie, date);

            string? msg = TempData["SuccessMessage"]?.ToString();

            var viewModel = _viewMapper.MapWithDetails(pagedResult, lastId, email, movie, date, msg);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Refund(int id)
        {
            await _paymentService.RefundPaymentAsync(id);
            TempData["SuccessMessage"] = "Оплату успішно повернено";

            return RedirectToAction(nameof(Index));
        }
    }
}
