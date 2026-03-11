using FluentValidation;
using onlineCinema.Areas.Admin.Models;
using static onlineCinema.Validators.ValidationMessages;

namespace onlineCinema.Validators
{
    public class DirectorFormValidator
        : AbstractValidator<DirectorFormViewModel>
    {
        public DirectorFormValidator()
        {
            RuleFor(x => x.DirectorFirstName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "ім'я"));
            RuleFor(x => x.DirectorLastName)
                .NotEmpty()
                    .WithMessage(string.Format(FieldRequired, "прізвище"));
        }
    }
}
