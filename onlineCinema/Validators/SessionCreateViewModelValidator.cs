using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class SessionCreateViewModelValidator : AbstractValidator<SessionCreateViewModel>
    {
        public SessionCreateViewModelValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage("Фільм обов'язковий");

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage("Зал обов'язковий");

            RuleFor(x => x.ShowingDateTime)
                .NotEmpty()
                .GreaterThan(DateTime.Now).WithMessage("Дата сеансу має бути у майбутньому");

            RuleFor(x => x.BasePrice)
                .NotEmpty()
                .GreaterThan(0).WithMessage("Ціна повинна бути більшою за 0");
        }
    }
}