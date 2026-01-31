using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ім'я є обов'язковим");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Прізвище є обов'язковим");
            RuleFor(x => x.MiddleName).NotEmpty().WithMessage("Прізвище є обов'язковим");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна пошта є обов'язковою")
                .EmailAddress().WithMessage("Невірний формат електронної пошти");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов'язковим");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль є обов'язковим")
                .MinimumLength(6).WithMessage("Пароль має бути не менше 6 символів");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Підтвердьте пароль")
                .Equal(x => x.Password).WithMessage("Паролі не співпадають");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов'язковою")
                .Must(d => !d.HasValue || d <= DateTime.Now).WithMessage("Дата народження не може бути в майбутньому");
        }
    }
}
