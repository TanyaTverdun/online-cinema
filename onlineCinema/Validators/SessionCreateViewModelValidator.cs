using FluentValidation;
using onlineCinema.ViewModels;

namespace onlineCinema.Validators
{
    public class SessionCreateViewModelValidator : AbstractValidator<SessionCreateViewModel>
    {
        public SessionCreateViewModelValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage("Виберіть фільм для сеансу")
                .WithName("Фільм");

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage("Виберіть зал для проведення сеансу")
                .WithName("Зал");

            RuleFor(x => x.ShowingDateTime)
                .NotEmpty().WithMessage("Вкажіть дату та час початку сеансу")
                .GreaterThan(x => DateTime.Now).WithMessage("Час початку сеансу не може бути в минулому")
                .WithName("Дата та час");

            RuleFor(x => x.BasePrice)
                .NotEmpty().WithMessage("Вкажіть базову ціну квитка")
                .GreaterThan(0).WithMessage("Ціна квитка повинна бути більшою за 0 грн")
                .WithName("Ціна");

            RuleFor(x => x.BasePrice)
                .NotEmpty().WithMessage("Вкажіть базову ціну квитка")
                .GreaterThan(0).WithMessage("Ціна повинна бути більшою за 0 грн")
                .WithName("Ціна");
        }
    }
}