using FluentValidation;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class DirectorFormValidator : AbstractValidator<DirectorFormViewModel>
    {
        public DirectorFormValidator()
        {
            RuleFor(x => x.DirectorFirstName)
                .NotEmpty().WithMessage("Вкажіть ім'я");
            RuleFor(x => x.DirectorLastName)
                .NotEmpty().WithMessage("Вкажіть прізвище");
            RuleFor(x => x.DirectorMiddleName)
                .NotEmpty().WithMessage("Вкажіть по батькові");
        }
    }
}