using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class RegisterViewModelValidator 
        : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            const string nameRegex = @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ\s\-']+$";

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ім'я є обов'язковим")
                .Matches(nameRegex)
                .WithMessage("Ім'я може містити тільки літери")
                .MaximumLength(50)
                .WithMessage("Ім'я не може перевищувати 50 символів");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Прізвище є обов'язковим")
                .Matches(nameRegex)
                .WithMessage("Прізвище може містити тільки літери")
                .MaximumLength(50)
                .WithMessage("Прізвище не може перевищувати 50 символів");

            RuleFor(x => x.MiddleName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50)
                .WithMessage("По батькові не може перевищувати 50 символів")
                .Matches(nameRegex)
                .WithMessage("По батькові може містити тільки літери")
                .When(x => !string.IsNullOrWhiteSpace(x.MiddleName));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна пошта є обов'язковою")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage(
                "Невірний формат електронної пошти (you@gmail.com)");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов'язковим")
                .Matches(@"^\+380\d{9}$")
                .WithMessage("Формат телефону має бути +380XXXXXXXXX");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль є обов'язковим")
                .MinimumLength(6)
                .WithMessage("Пароль має бути не менше 6 символів")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("Пароль повинен містити мінімум 6 символів, " 
                        + "хоча б одну велику літеру (A-Z),"
                        + "одну малу літеру (a-z) та одну цифру (0-9)");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов'язковою")
                .Must(d => !d.HasValue || d <= DateTime.Now)
                .WithMessage("Дата народження не може бути в майбутньому")
                .Must(d => !d.HasValue || d >= new DateTime(1900, 1, 1))
                .WithMessage(
                "Дата народження не може бути раніше 01.01.1900");
        }
    }
}
