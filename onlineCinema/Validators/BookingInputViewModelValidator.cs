using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class BookingInputViewModelValidator : AbstractValidator<BookingInputViewModel>
    {
        public BookingInputViewModelValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0).WithMessage("Некоректний ідентифікатор сеансу.");

            RuleFor(x => x.SelectedSeatIds)
                .NotEmpty().WithMessage("Ви не обрали жодного місця!") // Замінює вашу ручну перевірку в контролері
                .Must(list => list != null && list.Count <= 10)
                .WithMessage("За один раз можна забронювати не більше 10 місць.");
        }
    }
}
