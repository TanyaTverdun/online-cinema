using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using onlineCinema.Infrastructure.Data;
using onlineCinema.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace onlineCinema.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<MaintenanceController> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("reset-database")]
        public async Task<IActionResult> ResetDatabase(
            [FromHeader(Name = "X-Maintenance-Token")]
             string token)
        {
            var expectedToken = _configuration["MaintenanceToken"];
            if (string.IsNullOrEmpty(expectedToken)
                    || string.IsNullOrEmpty(token)
                    || !CryptographicOperations.FixedTimeEquals(
                        Encoding.UTF8.GetBytes(token),
                        Encoding.UTF8.GetBytes(expectedToken)))
            {
                _logger
                    .LogWarning(
                        "Unauthorized attempt to reset the database. Invalid token provided.");
                return Unauthorized(
                    new
                    {
                        message = "Invalid access token."
                    });
            }

            try
            {
                _logger
                    .LogInformation("Starting database initialization via API...");

                await DbInitializer
                    .Initialize(_dbContext, _userManager, _roleManager);

                _logger
                    .LogInformation("Database initialization completed successfully.");
                return Ok(new
                {
                    message = "Database has been successfully reset and updated."
                });
            }
            catch (Exception ex)
            {
                _logger
                    .LogError(
                        ex,
                        "An error occurred during database initialization via API.");
                return StatusCode(500, new
                {
                    message = "Internal server error."
                });
            }
        }
    }
}