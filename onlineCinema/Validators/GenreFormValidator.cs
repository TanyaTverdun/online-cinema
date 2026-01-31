using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class GenreFormValidator : AbstractValidator<GenreFormViewModel>
    {
        public GenreFormValidator()
        {
            RuleFor(x => x.GenreName)
                .NotEmpty().WithMessage("Вкажіть назву жанру")
                .MaximumLength(50).WithMessage("Назва не може перевищувати 50 символів");
        }
    }
}
