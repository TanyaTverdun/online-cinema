using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using onlineCinema.ViewModels;

namespace onlineCinema.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BookingViewModelMapper _viewMapper;
        private readonly ISnackService _snackService;
        private readonly SnackViewModelMapper _snackMapper;

        public BookingController(
            IBookingService bookingService,
            UserManager<ApplicationUser> userManager,
            BookingViewModelMapper viewMapper,
            ISnackService snackService,
            SnackViewModelMapper snackMapper)
        {
            _bookingService = bookingService;
            _userManager = userManager;
            _viewMapper = viewMapper;
            _snackService = snackService;
            _snackMapper = snackMapper;
        }

        // сторінка бронювання місць
        [HttpGet("Booking/SelectSeats/{sessionId:int}")]
        public async Task<IActionResult> SelectSeats(int sessionId)
        {
            if (sessionId <= 0)
            {
                return BadRequest("Некоректний ID сеансу.");
            }

            try
            {
                var dto = await _bookingService.GetSessionSeatMapAsync(sessionId);

                var viewModel = _viewMapper.MapSessionSeatMapToViewModel(dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // збереження вибраних місць
        // POST: /Booking/BookSeats
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> BookSeats(BookingInputViewModel model)
        {
            if (model.SelectedSeatIds == null || !model.SelectedSeatIds.Any())
            {
                TempData["Error"] = "Ви не обрали місця!";
                return RedirectToAction(nameof(SelectSeats), new { sessionId = model.SessionId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            try
            {
                var dto = _viewMapper.MapBookingInputViewModelToDto(model, user);

                int bookingId = await _bookingService.CreateBookingAsync(dto);

                return RedirectToAction(nameof(AddSnacks), new { bookingId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(SelectSeats), new { sessionId = model.SessionId });
            }
        }

        // сторінка вибору снеків
        [HttpGet]
        public async Task<IActionResult> AddSnacks(int bookingId)
        {
            var snackDtos = await _snackService.GetAllSnacksAsync();
            var seatsTotalPrice = await _bookingService.GetTicketsPriceTotalAsync(bookingId);
            var viewModel = _snackMapper.MapToSelectionViewModel(snackDtos, bookingId, seatsTotalPrice);

            return View(viewModel);
        }

        // Збереження обраних снеків
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSnacks(SnackSelectionViewModel model)
        {
            var chosenItems = model.AvailableSnacks.Where(s => s.Quantity > 0).ToList();

            if (chosenItems.Any())
            {
                var selectedDtos = _snackMapper.MapSnackItemViewModelToSelectedDtoList(chosenItems);

                await _bookingService.AddSnacksToBookingAsync(model.BookingId, selectedDtos);
            }

            return RedirectToAction(nameof(BookingSuccess), new { id = model.BookingId });
        }

        // Фінальна сторінка (дякуємо за замовлення)
        [HttpGet]
        public IActionResult BookingSuccess(int id)
        {
            return View(id);
        }
    }
}
