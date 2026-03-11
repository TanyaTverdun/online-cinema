using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class MovieFormValidator : AbstractValidator<MovieFormViewModel>
    {
        public MovieFormValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "назва"))
                .MaximumLength(200)
                    .WithMessage(string.Format(FieldTooLong, "назва", 200));

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "опис"));

            RuleFor(x => x.Runtime)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "тривалість"))
                .Must(t => t.HasValue && t.Value.TotalMinutes > 0)
                    .WithMessage("Тривалість має бути більшою за 0")
                .Must(t => t.HasValue && t.Value.TotalMinutes <= 300)
                    .WithMessage("Фільм не може тривати довше 5 годин");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 10)
                    .WithMessage("Рейтинг має бути від 0 до 10")
                .Must(rating => Math.Round(rating, 1) == rating)
                    .WithMessage(
                        "Рейтинг повинен мати не більше 1 цифри після коми ");


            RuleFor(x => x.ReleaseDate)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "дата релізу"));

            RuleFor(x => x.GenreIds)
                .NotEmpty()
                    .WithMessage(
                        string.Format(FieldRequiredOption, "жанр"))
                .When(x => string.IsNullOrWhiteSpace(x.GenresInput));

            RuleFor(x => x.CastIds)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequiredOption, "актора"))
                .When(x => string.IsNullOrWhiteSpace(x.ActorsInput));

            RuleFor(x => x.DirectorIds)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequiredOption, "режисера"))
                .When(x => string.IsNullOrWhiteSpace(x.DirectorsInput));

            RuleFor(x => x.LanguageIds)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequiredOption, "мову"))
                .When(x => string.IsNullOrWhiteSpace(x.LanguagesInput));
        }
    }
}