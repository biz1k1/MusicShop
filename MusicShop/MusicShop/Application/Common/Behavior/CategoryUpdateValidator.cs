using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Application.Common.Behavior
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryRequestUpdate>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(-1);
        }
    }
}
