using System.ComponentModel.DataAnnotations;

namespace onlineCinema.ViewModels
{
    public class LoginViewModel
    {
        public string? Email { get; set; } = string.Empty;

        public string? Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}

