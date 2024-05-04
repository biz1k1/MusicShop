using FluentValidation.TestHelper;
using MusicShop.Application.Common.Behavior;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.Category;
namespace MusicShop.UnitTests.Application.Common.Behavior
{
    public class CategoryRequestValidatorTests
    {
        private  CategoryRequestValidator _validator;
        [Fact]
        public void Should_have_error_when_CategoryRequest_Name_is_null()
        {
            //arrange 
            _validator = new CategoryRequestValidator();
            //act
            var model = new CategoryRequest { Name = null };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }
        [Fact]
        public void Should_have_error_when_CategoryRequest_SubCategoryId_is_negative()
        {
            //arrange
            _validator = new CategoryRequestValidator();
            const int negativ_Result= -1;
            //act
            var model = new CategoryRequest { SubCategoryId=negativ_Result};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(category => category.SubCategoryId);
        }
    }
}
