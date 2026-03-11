using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class SnackValidator : AbstractValidator<SnackViewModel>
    {
        public SnackValidator()
        {
            RuleFor(x => x.SnackName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "назва снеку"))
                .MaximumLength(100)
                    .WithMessage(string.Format(FieldTooLong, "назва снеку", 100));

            RuleFor(x => x.Price)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "ціна"))
                .GreaterThan(0)
                    .WithMessage("ціна має бути більше нуля")
                .PrecisionScale(10, 2, false)
                    .WithMessage(
                        "Ціна має некоректний формат (макс. 2 знаки після коми)");
        }
    }
}
