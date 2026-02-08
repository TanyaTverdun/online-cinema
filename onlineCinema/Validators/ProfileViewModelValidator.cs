using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class ProfileViewModelValidator : AbstractValidator<ProfileViewModel>
    {
        public ProfileViewModelValidator()
        {
            const string nameRegex = @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ\s\-']+$";

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ім'я є обов'язковим")
                .Matches(nameRegex).WithMessage("Ім'я може містити тільки літери")
                .MaximumLength(50).WithMessage("Ім'я не може перевищувати 50 символів");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Прізвище є обов'язковим")
                .Matches(nameRegex).WithMessage("Прізвище може містити тільки літери")
                .MaximumLength(50).WithMessage("Прізвище не може перевищувати 50 символів");

            RuleFor(x => x.MiddleName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("По батькові є обов'язковим")
                .Matches(nameRegex).WithMessage("По батькові може містити тільки літери")
                .MaximumLength(50).WithMessage("По батькові не може перевищувати 50 символів");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна пошта є обов'язковою")
                .EmailAddress().WithMessage("Невірний формат електронної пошти");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов'язковим")
                .Matches(@"^\+380\d{9}$").WithMessage("Формат телефону має бути +380XXXXXXXXX");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов'язковою")
                .Must(d => !d.HasValue || d <= DateTime.Now).WithMessage("Дата народження не може бути в майбутньому")
                .Must(d => !d.HasValue || d >= new DateTime(1900, 1, 1)).WithMessage("Дата народження не може бути раніше 01.01.1900");

            // Ця логіка спрацьовує тільки якщо поле NewPassword не порожнє
            RuleFor(x => x.NewPassword)
                .MinimumLength(6).WithMessage("Пароль повинен містити мінімум 6 символів")
                .Matches("[A-Z]").WithMessage("Пароль повинен містити хоча б одну велику літеру")
                .Matches("[a-z]").WithMessage("Пароль повинен містити хоча б одну малу літеру")
                .Matches("[0-9]").WithMessage("Пароль повинен містити хоча б одну цифру")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Підтвердження пароля є обов'язковим")
                .Equal(x => x.NewPassword).WithMessage("Паролі не співпадають")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }
    }
}
