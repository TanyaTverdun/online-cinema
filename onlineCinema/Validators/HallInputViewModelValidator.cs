using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class HallInputViewModelValidator : AbstractValidator<HallInputViewModel>
    {
        public HallInputViewModelValidator()
        {
            RuleFor(x => x.HallNumber)
                .GreaterThan(0).WithMessage("Номер залу повинен бути більшим за 0.");

            RuleFor(x => x.RowCount)
                .InclusiveBetween(1, 255).WithMessage("Кількість рядів має бути від 1 до 255.");

            RuleFor(x => x.SeatInRowCount)
                .InclusiveBetween(1, 255).WithMessage("Кількість місць у ряду має бути від 1 до 255.");
        }
    }
}
