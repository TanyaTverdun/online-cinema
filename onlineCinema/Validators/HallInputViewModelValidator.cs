using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class HallInputViewModelValidator 
        : AbstractValidator<HallInputViewModel>
    {
        public HallInputViewModelValidator()
        {
            RuleFor(x => x.HallNumber)
                .InclusiveBetween(1, 255)
                .WithMessage(
                "Номер залу повинен бути більшим за 0.");

            RuleFor(x => x.RowCount)
                .InclusiveBetween(1, 255)
                .WithMessage(
                "Кількість рядів має бути від 1 до 255.");

            RuleFor(x => x.SeatInRowCount)
                .InclusiveBetween(1, 255)
                .WithMessage(
                "Кількість місць у ряду має бути від 1 до 255.");

            RuleFor(x => x.VipRowCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Кількість VIP рядів не може бути від'ємною.");

            RuleFor(x => x.VipRowCount)
                .Must((model, vipRows) => vipRows <= model.RowCount)
                .WithMessage(
                "Кількість VIP рядів не може перевищувати " +
                 "загальну кількість рядів.");

            RuleFor(x => x.VipCoefficient)
                .GreaterThan(1.0f)
                .WithMessage("Коефіцієнт VIP має бути більше 1.0")
                .When(x => x.VipRowCount > 0);
        }
    }
}
