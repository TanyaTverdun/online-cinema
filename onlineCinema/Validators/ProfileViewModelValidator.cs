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
                .Matches(@"^\+?\d{10,13}$").WithMessage("Невірний формат телефону");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов'язковою")
                .Must(d => !d.HasValue || d <= DateTime.Now)
                .WithMessage("Дата народження не може бути в майбутньому");
        }
    }
}
