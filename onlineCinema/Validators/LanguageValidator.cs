using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class LanguageValidator : AbstractValidator<LanguageViewModel>
    {
        public LanguageValidator()
        {
            RuleFor(x => x.LanguageName)
                .NotEmpty().WithMessage("Назва мови обов'язкова")
                .MaximumLength(50).WithMessage("Назва мови занадто довга")
                .Matches(@"^[a-zA-Zа-яА-ЯіІїЇєЄ\s]+$").WithMessage("Назва мови має містити тільки літери");
        }
    }
}