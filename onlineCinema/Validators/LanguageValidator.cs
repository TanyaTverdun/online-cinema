using FluentValidation;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class LanguageValidator : AbstractValidator<LanguageViewModel>
    {
        public LanguageValidator()
        {
            RuleFor(x => x.LanguageName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "назва мови"))
                .MaximumLength(50)
                    .WithMessage(string.Format(FieldTooLong, "назва мови", 50))
                .Matches(@"^[a-zA-Zа-яА-ЯіІїЇєЄ\s()]+$")
                    .WithMessage(
                        "Назва мови може містити тільки літери, пробіли та дужки");
        }
    }
}
