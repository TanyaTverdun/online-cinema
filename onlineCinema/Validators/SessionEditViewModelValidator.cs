using FluentValidation;
using onlineCinema.ViewModels;
using onlineCinema.ViewModels;

namespace onlineCinema.Web.Validation
{
    public class SessionEditViewModelValidator
        : AbstractValidator<SessionEditViewModel>
    {
        public SessionEditViewModelValidator()
        {
            RuleFor(x => x.MovieId)
                .GreaterThan(0)
                .WithMessage("Оберіть фільм");

            RuleFor(x => x.HallId)
                .GreaterThan(0)
                .WithMessage("Оберіть зал");

            RuleFor(x => x.ShowingDateTime)
                .GreaterThan(DateTime.Now)
                .WithMessage("Дата показу має бути в майбутньому");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .WithMessage("Ціна має бути більшою за 0");
        }
    }
}
