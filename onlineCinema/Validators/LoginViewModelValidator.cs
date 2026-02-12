using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Електронна пошта є обов'язковою")
                 .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                 .WithMessage(
                "Невірний формат електронної пошти (you@gmail.com)"); ;

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль є обов'язковим");
        }
    }
}
