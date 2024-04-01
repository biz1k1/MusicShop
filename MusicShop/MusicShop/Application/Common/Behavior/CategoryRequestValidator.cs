using FluentValidation;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Category;

namespace MusicShop.Application.Common.Behavior
{
    public class CategoryRequestValidator: AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.SubCategoryId)
                .GreaterThan(-1);
        }
    }
}
