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
                .MaximumLength(50).WithMessage("Назва занадто довга (макс 50 символів)");

            RuleFor(x => x.FeatureDescription)
                .MaximumLength(500).WithMessage("Опис не може перевищувати 500 символів");
        }
    }
}