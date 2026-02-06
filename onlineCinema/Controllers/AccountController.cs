using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCinema.Domain.Entities;
using onlineCinema.ViewModels;
using onlineCinema.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly UserMapping _userMapping;
        private readonly IBookingService _bookingService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            UserMapping userMapping,
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
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            model.ReturnUrl = returnUrl ?? model.ReturnUrl;
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            const string userRoleName = "User";
            if (!await _roleManager.RoleExistsAsync(userRoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(userRoleName));
            }

            var user = _userMapping.ToApplicationUser(model);

            var result = await _userManager.CreateAsync(user, model.Password ?? string.Empty);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRoleName);

                _logger.LogInformation("Користувач {Email} успішно зареєстрований з роллю {Role}", model.Email, userRoleName);
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
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
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
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
                _logger.LogInformation("Користувач {Email} успішно увійшов", model.Email);
                
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Обліковий запис користувача {Email} заблоковано", model.Email);
                ModelState.AddModelError(string.Empty, "Обліковий запис заблоковано. Спробуйте пізніше.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Невірний email або пароль.");
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
        public async Task<IActionResult> Profile(string? returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var bookings = isAdmin
                ? Enumerable.Empty<BookingHistoryDto>()
                : await _bookingService.GetBookingHistoryAsync(user.Id);

            var model = _userMapping.ToProfileViewModel(user, bookings, returnUrl);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound();
            }

            _userMapping.UpdateApplicationUser(model, user);

            if (user.Email != model.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setUserNameResult.Succeeded)
                {
                    foreach (var error in setUserNameResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("Профіль користувача {UserId} оновлено", user.Id);
                TempData["SuccessMessage"] = "Профіль успішно оновлено";
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}

