using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class CastMemberValidator : AbstractValidator<CastMemberViewModel>
    {
        public CastMemberValidator()
        {
            RuleFor(x => x.CastFirstName)
                .NotEmpty().WithMessage(string.Format(FieldRequired, "ім'я"))
                .MaximumLength(100)
                .WithMessage(string.Format(FieldTooLong, "ім'я", 100));

            RuleFor(x => x.CastLastName)
                .NotEmpty().WithMessage(string.Format(FieldRequired, "прізвище"))
                .MaximumLength(100)
                .WithMessage(string.Format(FieldTooLong, "прізвище", 100));

            RuleFor(x => x.CastMiddleName)
                .MaximumLength(100)
                .WithMessage(string.Format(FieldTooLong, "по батькові", 100))
                .When(x => !string.IsNullOrWhiteSpace(x.CastMiddleName));
        }
    }
}
