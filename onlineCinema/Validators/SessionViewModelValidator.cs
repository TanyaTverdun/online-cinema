using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Web.Validation
{
    public class SessionViewModelValidator
        : AbstractValidator<SessionViewModel>
    {
        public SessionViewModelValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty()
                    .WithMessage("Виберіть фільм для сеансу")
                .WithName("Фільм");

            RuleFor(x => x.HallId)
                .NotEmpty()
                    .WithMessage("Виберіть зал для проведення сеансу")
                .WithName("Зал");

            RuleFor(x => x.ShowingDateTime)
                .NotEmpty()
                    .WithMessage("Вкажіть дату та час початку сеансу")
                .GreaterThan(x => DateTime.Now)
                    .WithMessage("Час початку сеансу не може бути в минулому")
                .WithName("Дата та час");

            RuleFor(x => x.BasePrice)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "базова ціна"))
                .GreaterThan(0)
                    .WithMessage("ціна має бути більше нуля")
                .WithName("Ціна");
        }
    }
}
