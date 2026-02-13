using FluentValidation;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Validators
{
    public class CastMemberValidator : AbstractValidator<CastMemberViewModel>
    {
        public CastMemberValidator()
        {
            RuleFor(x => x.CastFirstName)
                .NotEmpty().WithMessage("Ім'я обов'язкове")
                .MaximumLength(100)
                .WithMessage(
                "Ім'я не може перевищувати 100 символів");

            RuleFor(x => x.CastLastName)
                .NotEmpty().WithMessage("Прізвище обов'язкове")
                .MaximumLength(100)
                .WithMessage(
                "Прізвище не може перевищувати 100 символів");

            RuleFor(x => x.CastMiddleName)
                .NotEmpty().WithMessage("По батькові обов'язкове")
                .MaximumLength(100).
                WithMessage(
                "По батькові не може перевищувати 100 символів");
        }
    }
}