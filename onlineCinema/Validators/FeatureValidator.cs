using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class FeatureValidator : AbstractValidator<FeatureViewModel>
    {
        public FeatureValidator()
        {
            RuleFor(x => x.FeatureName)
                .NotEmpty().WithMessage("Назва характеристики обов'язкова")
                .MaximumLength(100).WithMessage("Назва занадто довга");

            RuleFor(x => x.FeatureDescription)
                .MaximumLength(500).WithMessage("Опис занадто довгий");
        }
    }
}