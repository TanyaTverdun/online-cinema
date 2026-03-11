using FluentValidation;
using onlineCinema.ViewModels;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                    .WithMessage(string.Format(
                        FieldRequired, "електронна пошта"))
                 .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                    .WithMessage(EmailInvalidFormat);

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "пароль"));
        }
    }
}
