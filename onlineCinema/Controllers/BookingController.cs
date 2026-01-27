using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using onlineCinema.Models;
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
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            IBookingService bookingService,
            UserManager<ApplicationUser> userManager,
            BookingViewModelMapper viewMapper,
            ISnackService snackService,
            SnackViewModelMapper snackMapper,
            ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _userManager = userManager;
            _viewMapper = viewMapper;
            _snackService = snackService;
            _snackMapper = snackMapper;
            _logger = logger;
        }

        // сторінка бронювання місць
        [HttpGet("Booking/SelectSeats/{sessionId:int}")]
        public async Task<IActionResult> SelectSeats(int sessionId)
        {
            _logger.LogInformation("Користувач відкрив сторінку вибору місць для сеансу {SessionId}", sessionId);
            if (sessionId <= 0)
            {
                _logger.LogWarning("Запит SelectSeats відхилено: некоректний ID {SessionId}", sessionId);
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
                _logger.LogWarning("Сторінку вибору місць не знайдено: сеанс {SessionId} відсутній", sessionId);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при завантаженні карти місць для сеансу {SessionId}", sessionId);
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorModel);
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
                _logger.LogWarning("Спроба бронювання без вибраних місць для сеансу {SessionId}", model.SessionId);
                TempData["Error"] = "Ви не обрали місця!";
                return RedirectToAction(nameof(SelectSeats), new { sessionId = model.SessionId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Авторизований користувач не знайдений у системі при бронюванні");
                return Challenge();
            }

            if (!user.DateOfBirth.HasValue)
            {
                TempData["Error"] = "Будь ласка, вкажіть дату народження у своєму профілі перед бронюванням.";
                return RedirectToAction("Profile", "Account");
            }

            try
            {
                _logger.LogInformation("Користувач {UserId} бронює місця [{Seats}] на сеанс {SessionId}", 
                    user.Id, string.Join(", ", model.SelectedSeatIds), model.SessionId);
                var dto = _viewMapper.MapBookingInputViewModelToDto(model, user);

                int bookingId = await _bookingService.CreateBookingAsync(dto);

                _logger.LogInformation("Успішне бронювання квитків. Створено BookingId: {BookingId}", bookingId);
                return RedirectToAction(nameof(AddSnacks), new { bookingId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при створенні бронювання для користувача {UserId}", user.Id);
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(SelectSeats), new { sessionId = model.SessionId });
            }
        }

        // сторінка вибору снеків
        [HttpGet]
        public async Task<IActionResult> AddSnacks(int bookingId)
        {
            _logger.LogInformation("Завантаження сторінки снеків для бронювання {BookingId}", bookingId);

            try
            {
                var snackDtos = await _snackService.GetAllSnacksAsync();
                var seatsTotalPrice = await _bookingService.GetTicketsPriceTotalAsync(bookingId);
                var lockUntil = await _bookingService.GetBookingLockUntilAsync(bookingId);
                var viewModel = _snackMapper.MapToSelectionViewModel(snackDtos, bookingId, seatsTotalPrice, lockUntil);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка завантаження сторінки снеків для бронювання {BookingId}", bookingId);
                return RedirectToAction("Index", "Home");
            }
        }

        // Збереження обраних снеків
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSnacks(SnackSelectionViewModel model)
        {
            var lockUntil = await _bookingService.GetBookingLockUntilAsync(model.BookingId);
            if (lockUntil < DateTime.Now)
            {
                TempData["Error"] = "Час бронювання вийшов. Місця були звільнені.";
                return RedirectToAction("Index", "Home");
            }

            var chosenItems = model.AvailableSnacks.Where(s => s.Quantity > 0).ToList();

            try
            {
                if (chosenItems.Any())
                {
                    _logger.LogInformation("Додавання {Count} снеків до бронювання {BookingId}", chosenItems.Count, model.BookingId);
                    var selectedDtos = _snackMapper.MapSnackItemViewModelToSelectedDtoList(chosenItems);
                    await _bookingService.AddSnacksToBookingAsync(model.BookingId, selectedDtos);
                }
                else
                {
                    _logger.LogInformation("Користувач не обрав жодного снека для бронювання {BookingId}", model.BookingId);
                }

                await _bookingService.CompletePaymentAsync(model.BookingId);

                return RedirectToAction(nameof(BookingSuccess), new { id = model.BookingId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка при збереженні снеків для бронювання {BookingId}", model.BookingId);
                return RedirectToAction(nameof(BookingSuccess), new { id = model.BookingId });
            }
        }

        // Фінальна сторінка (дякуємо за замовлення)
        [HttpGet]
        public IActionResult BookingSuccess(int id)
        {
            _logger.LogInformation("Відображення успішного завершення бронювання {BookingId}", id);
            return View(id);
        }
    }
}
