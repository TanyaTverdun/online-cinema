using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class FeatureValidator : AbstractValidator<FeatureViewModel>
    {
        public FeatureValidator()
        {
            RuleFor(x => x.FeatureName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "назва"))
                .MaximumLength(100)
                    .WithMessage(string.Format(FieldTooLong, "назва", 100));

            RuleFor(x => x.FeatureDescription)
                .MaximumLength(500)
                    .WithMessage(string.Format(FieldTooLong, "опис", 500));
        }
    }
}