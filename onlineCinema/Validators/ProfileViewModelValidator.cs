using FluentValidation;
using onlineCinema.ViewModels;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class ProfileViewModelValidator
        : AbstractValidator<ProfileViewModel>
    {
        public ProfileViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "ім'я"))
                .Matches(NameRegex)
                    .WithMessage(string.Format(OnlyLetters, "ім'я"))
                .MaximumLength(50)
                    .WithMessage(string.Format(FieldTooLong, "ім'я", 50));

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "прізвище"))
                .Matches(NameRegex)
                    .WithMessage(string.Format(OnlyLetters, "прізвище"))
                .MaximumLength(50)
                    .WithMessage(string.Format(FieldTooLong, "прізвище", 50));

            RuleFor(x => x.MiddleName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(50)
                    .WithMessage(string.Format(FieldTooLong, "по батькові", 50))
                .Matches(NameRegex)
                    .WithMessage(string.Format(OnlyLetters, "по батькові"))
                .When(x => !string.IsNullOrWhiteSpace(x.MiddleName));

            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "електронна пошта"))
                .EmailAddress()
                    .WithMessage(EmailInvalidFormat);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "телефон"))
                .Matches(@"^\+380\d{9}$")
                    .WithMessage(PhoneFormat);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "дата народження"))
                .Must(d => !d.HasValue || d <= DateTime.Now)
                    .WithMessage(DateOfBirthFuture)
                .Must(d => !d.HasValue || d >= new DateTime(1900, 1, 1))
                    .WithMessage(DateOfBirthTooOld);

            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                    .WithMessage(PasswordMinLength)
                .Matches("[A-Z]")
                    .WithMessage(
                        string.Format(PasswordFormat, "велику літеру"))
                .Matches("[a-z]")
                    .WithMessage(
                        string.Format(PasswordFormat, "малу літеру"))
                .Matches("[0-9]")
                    .WithMessage(
                        string.Format(PasswordFormat, "цифру"))
                .When(x => !string.IsNullOrEmpty(x.NewPassword));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(
                        string.Format(FieldRequired, "підтвердження пароля"))
                .Equal(x => x.NewPassword)
                    .WithMessage(PasswordsMismatch)
                .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }
    }
}
