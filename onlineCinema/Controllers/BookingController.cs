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

        [HttpGet("Booking/SelectSeats/{sessionId:int}")]
        public async Task<IActionResult> SelectSeats(int sessionId)
        {
            try
            {
                var dto = await _bookingService
                    .GetSessionSeatMapAsync(sessionId);

                var viewModel = _viewMapper
                    .MapSessionSeatMapToViewModel(dto);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> BookSeats(
            BookingInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault()?.ErrorMessage;

                return RedirectToAction(nameof(SelectSeats),
                    new { sessionId = model.SessionId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            if (!user.DateOfBirth.HasValue)
            {
                return RedirectToAction("Profile", "Account");
            }

            try
            {
                var dto = _viewMapper
                    .MapBookingInputViewModelToDto(model, user);

                int bookingId = await _bookingService
                    .CreateBookingAsync(dto);

                return RedirectToAction(nameof(AddSnacks),
                    new { bookingId });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(SelectSeats),
                new { sessionId = model.SessionId });
        }


        [HttpGet]
        public async Task<IActionResult> AddSnacks(int bookingId)
        {
            try
            {
                var snackDtos = await _snackService.GetAllAsync();

                var seatsTotalPrice = await _bookingService
                    .GetTicketsPriceTotalAsync(bookingId);

                var lockUntil = await _bookingService
                    .GetBookingLockUntilAsync(bookingId);

                var initialSeconds = (int)(lockUntil - DateTime.Now).TotalSeconds;

                var viewModel = _snackMapper
                    .MapToSelectionViewModel(
                        snackDtos,
                        bookingId,
                        seatsTotalPrice,
                        lockUntil,
                        initialSeconds);

                return View(viewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Profile", "Account");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Profile", "Account");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSnacks(
            SnackSelectionViewModel model)
        {
            var lockUntil = await _bookingService
                .GetBookingLockUntilAsync(model.BookingId);

            if (lockUntil < DateTime.Now)
            {
                TempData["Error"] = 
                    "Час бронювання вийшов. Місця були звільнені.";
                return RedirectToAction("Index", "Home");
            }

            var chosenItems = model.AvailableSnacks?
                .Where(s => s.Quantity > 0)
                .ToList() ?? new List<SnackItemViewModel>();

            try
            {
                if (chosenItems.Any())
                {
                    var selectedDtos = _snackMapper
                        .MapSnackItemViewModelToSelectedDtoList(chosenItems);

                    await _bookingService
                        .AddSnacksToBookingAsync(
                        model.BookingId, selectedDtos);
                }

                await _bookingService
                    .CompletePaymentAsync(model.BookingId);

                return RedirectToAction(
                    nameof(BookingSuccess),
                    new { id = model.BookingId });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Сталася помилка під час завершення бронювання.";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public IActionResult BookingSuccess(int id)
        {
            return View(id);
        }
    }
}
