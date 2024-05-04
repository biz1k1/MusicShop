using FluentValidation.TestHelper;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.DTOs.Product;

namespace MusicShop.UnitTests.Application.Common.Behavior
{
    public class ProductRequestValidatorTests
    {
        private ProductRequestValidator _validator;
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_Name_is_null()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            //act
            var model = new ProductRequest { Name = null };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_Name_Lenght_is_exceed()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            string maximum_result = string.Concat(Enumerable.Repeat("a", 16));
            //act
            var model = new ProductRequest { Name = maximum_result };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_Description_is_null()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            //act
            var model = new ProductRequest { Description=null };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_InStock_is_negativ()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            const int negativ_result = -1;
            //act
            var model = new ProductRequest { InStock=negativ_result};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.InStock);
        }
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_Price_is_negative()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            const int zero_result= -1;
            //act
            var model = new ProductRequest { Price=zero_result };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.Price);
        }
        [Fact]
        public void Should_have_error_when_ProductRequestValidator_CategoryId_is_less_than_zero()
        {
            //arrange 
            _validator = new ProductRequestValidator();
            const int zero_result = -1;
            //act
            var model = new ProductRequest { CategoryId=zero_result };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(product => product.CategoryId);
        }

    }
}
