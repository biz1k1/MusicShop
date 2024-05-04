using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Application.Common.Behavior
{
    public class CategoryRequestUpdateValidator : AbstractValidator<CategoryRequestUpdate>
    {
        public CategoryRequestUpdateValidator()
        {
            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(-1);
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
