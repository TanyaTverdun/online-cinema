using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class GenreFormValidator : AbstractValidator<GenreFormViewModel>
    {
        public GenreFormValidator()
        {
            RuleFor(x => x.GenreName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "назва жанру"))
                .MaximumLength(50)
                    .WithMessage(string.Format(FieldTooLong, "назва жанру", 50));
        }
    }
}
