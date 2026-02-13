using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class SnackValidator : AbstractValidator<SnackViewModel>
    {
        public SnackValidator()
        {
            RuleFor(x => x.SnackName)
                .NotEmpty().WithMessage("Назва снеку обов'язкова")
                .MaximumLength(100).WithMessage("Назва занадто довга");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Вкажіть ціну")
                .GreaterThan(0).WithMessage("Ціна має бути більше 0")
                .PrecisionScale(10, 2, false)
                .WithMessage(
                "Ціна має некоректний формат (макс. 2 знаки після коми)");
        }
    }
}