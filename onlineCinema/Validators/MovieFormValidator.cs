using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class MovieFormValidator : AbstractValidator<MovieFormViewModel>
    {
        public MovieFormValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Введіть назву фільму")
                .MaximumLength(200).WithMessage("Назва занадто довга (макс 200)");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Опис є обов'язковим");

            RuleFor(x => x.Runtime)
                .InclusiveBetween(1, 1000).WithMessage("Тривалість має бути коректною (1-1000 хв)");

            RuleFor(x => x.ReleaseDate)
                .NotEmpty().WithMessage("Вкажіть дату виходу");
        }
    }
}