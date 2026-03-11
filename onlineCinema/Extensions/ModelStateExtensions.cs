using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace onlineCinema.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddFluentErrors(
            this ModelStateDictionary modelState,
            ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }
        }
    }
}
