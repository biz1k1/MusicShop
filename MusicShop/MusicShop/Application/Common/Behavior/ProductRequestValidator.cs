using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;

namespace MusicShop.Application.Common.Behavior
{
    public class ProductRequestValidator: AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(0, 15);
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.InStock)
                .NotEmpty()
                .GreaterThan(-1);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .GreaterThan(-1);
        }
    }
}
