using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Application.DTOs.Booking;
using onlineCinema.Application.DTOs.Common;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Constants;
using onlineCinema.Domain.Entities;
using onlineCinema.Mapping;
using onlineCinema.ViewModels;
using System.Security.Claims;

namespace onlineCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly UserMapper _userMapping;
        private readonly IBookingService _bookingService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            UserMapper userMapping,
            IBookingService bookingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _userMapping = userMapping;
            _bookingService = bookingService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = null)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            RegisterViewModel model,
            string? returnUrl = null)
        {
            model.ReturnUrl = returnUrl ?? model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Перевірка, чи існує email в базі
            var existingUser = await _userManager
                .FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "Email",
                    "Ця електронна пошта вже зареєстрована.");
                return View(model);
            }

            const string userRoleName = Roles.User;
            if (!await _roleManager.RoleExistsAsync(userRoleName))
            {
                await _roleManager
                    .CreateAsync(new IdentityRole(userRoleName));
            }

            var user = _userMapping.ToApplicationUser(model);

            user.MiddleName = string.IsNullOrWhiteSpace(user.MiddleName)
                ? null
                : user.MiddleName.Trim();

            var result = await _userManager
                .CreateAsync(user, model.Password ?? string.Empty);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRoleName);

                _logger.LogInformation(
                    "Користувач {UserId} успішно зареєстрований " +
                    "з роллю {Role}",
                    user.Id, userRoleName);

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (await _userManager.IsInRoleAsync(user, Roles.Admin))
                {
                    return RedirectToAction(
                        "Index", "Dashboard", new { area = "Admin" });
                }

                if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                    Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            model.ReturnUrl = returnUrl ?? model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email ?? string.Empty,
                model.Password ?? string.Empty,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var loggedInUser = await _userManager
                    .FindByEmailAsync(model.Email ?? string.Empty);
                _logger.LogInformation(
                    "Користувач {UserId} успішно увійшов",
                    loggedInUser?.Id);

                if (loggedInUser != null &&
                    await _userManager.IsInRoleAsync(
                        loggedInUser, Roles.Admin))
                {
                    return RedirectToAction(
                        "Index", "Dashboard", new { area = "Admin" });
                }

                if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                    Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning(
                    "Обліковий запис користувача заблоковано при спробі входу");
                ModelState.AddModelError(string.Empty,
                    "Обліковий запис заблоковано. Спробуйте пізніше.");
                return View(model);
            }

            ModelState.AddModelError(
                string.Empty, "Невірний email або пароль.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Користувач вийшов з системи");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile(
            int? lastId = null,
            int? firstId = null,
            string? returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);

            bool isAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);

            var pagedResultDto = isAdmin
                ? new PagedResultDto<BookingHistoryDto>()
                : await _bookingService.GetBookingHistorySeekAsync(
                    user.Id,
                    lastId,
                    firstId);

            var model = _userMapping.ToProfileViewModel(
                user,
                pagedResultDto,
                returnUrl);

            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userForError = await _userManager.GetUserAsync(User);
                if (userForError != null)
                {
                    await LoadHistoryAsync(userForError, model);
                }
                return View(model);
            }


            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            _userMapping.UpdateApplicationUser(model, user);

            // Логіка зміни Email
            if (user.Email != model.Email)
            {
                var setEmailResult = await _userManager
                    .SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            error.Description);
                    }
                    await LoadHistoryAsync(user, model);
                    return View(model);
                }

                var setUserNameResult = await _userManager.SetUserNameAsync(
                    user,
                    model.Email);
                if (!setUserNameResult.Succeeded)
                {
                    foreach (var error in setUserNameResult.Errors)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            error.Description);
                    }
                    await LoadHistoryAsync(user, model);
                    return View(model);
                }
            }

            // Логіка зміни пароля
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                bool isSameAsOld = await _userManager.CheckPasswordAsync(
                    user,
                    model.NewPassword);

                if (isSameAsOld)
                {
                    ModelState.AddModelError(nameof(model.NewPassword),
                        "Новий пароль не може бути таким самим, як старий.");
                    await LoadHistoryAsync(user, model);
                    return View(model);
                }

                var token = await _userManager
                    .GeneratePasswordResetTokenAsync(user);
                var changePasswordResult = await _userManager
                    .ResetPasswordAsync(user, token, model.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(
                            string.Empty, error.Description);
                    }
                    await LoadHistoryAsync(user, model);
                    return View(model);
                }
                _logger.LogInformation(
                    "Користувач {UserId} змінив пароль через профіль",
                    user.Id);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation(
                    "Профіль користувача {UserId} оновлено",
                    user.Id);
                TempData["SuccessMessage"] = "Профіль успішно оновлено";
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await LoadHistoryAsync(user, model);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                await _bookingService
                    .CancelBookingAsync(id, userId);

                TempData["SuccessMessage"] =
                    "Квитки успішно повернуто, кошти " +
                    "зараховано на баланс.";

                return RedirectToAction("Profile");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Profile");
        }


        private async Task LoadHistoryAsync(
            ApplicationUser user,
            ProfileViewModel model,
            int? lastId = null,
            int? firstId = null)
        {
            bool isAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);

            var pagedResultDto = isAdmin
                ? new PagedResultDto<BookingHistoryDto>()
                : await _bookingService.GetBookingHistorySeekAsync(
                    user.Id, lastId, firstId);

            model.BookingHistory =
                new PagedResultDto<BookingHistoryItemViewModel>
                {
                    Items = pagedResultDto.Items
                    .Select(dto =>
                        _userMapping
                            .ToBookingHistoryItemViewModel(dto))
                            .ToList(),
                    TotalCount = pagedResultDto.TotalCount,
                    PageSize = pagedResultDto.PageSize,
                    HasNextPage = pagedResultDto.HasNextPage,
                    LastId = pagedResultDto.LastId,
                    FirstId = pagedResultDto.FirstId,
                    HasPreviousPage = pagedResultDto.HasPreviousPage
                };
        }
    }
}
