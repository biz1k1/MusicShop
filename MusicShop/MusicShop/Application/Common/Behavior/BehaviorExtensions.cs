using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MusicShop.Application.Common.Behavior
{
    public static class BehaviorExtensions
    {
        public static ModelStateDictionary AddToModelState(ValidationResult result)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure failure in result.Errors)
            {
                modelStateDictionary.AddModelError(
                    failure.PropertyName,
                    failure.ErrorMessage);
            }
            return modelStateDictionary;
        }
    }
}
