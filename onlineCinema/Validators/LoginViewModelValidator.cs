using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна ашта є обов'язковою")
                .EmailAddress().WithMessage("Невірний формат електронної пошти");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль є обов'язковим");
        }
    }
}
