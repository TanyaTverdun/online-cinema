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

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("Виберіть жанр зі списку або додайте новий")
                .When(x => string.IsNullOrWhiteSpace(x.GenresInput));

            RuleFor(x => x.CastIds)
                .NotEmpty().WithMessage("Виберіть акторів або додайте нових")
                .When(x => string.IsNullOrWhiteSpace(x.ActorsInput));

            RuleFor(x => x.DirectorIds)
                .NotEmpty().WithMessage("Виберіть режисера або додайте нового")
                .When(x => string.IsNullOrWhiteSpace(x.DirectorsInput));

            RuleFor(x => x.LanguageIds)
                .NotEmpty().WithMessage("Виберіть мову або додайте нову")
                .When(x => string.IsNullOrWhiteSpace(x.LanguagesInput));
        }
    }
}