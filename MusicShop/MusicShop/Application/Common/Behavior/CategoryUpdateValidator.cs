using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Application.Common.Behavior
{
    public class CategoryUpdateValidator: AbstractValidator<CategoryResponseUpdate>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty();
            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(-1);
            RuleFor(x => x.ParentCategoryId)
                .NotEmpty();
        }
    }
}
