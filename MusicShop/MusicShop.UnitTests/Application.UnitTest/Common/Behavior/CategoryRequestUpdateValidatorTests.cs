using FluentValidation.TestHelper;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.UnitTests.Application.Common.Behavior
{
    public class CategoryRequestUpdateValidatorTests
    {
        private CategoryRequestUpdateValidator _validator;
        [Fact]
        public void Should_have_error_when_CategoryRequestUpdate_Name_is_null()
        {
            //arrange 
            _validator = new CategoryRequestUpdateValidator();
            //act
            var model = new CategoryRequestUpdate { Name=null};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }
        [Fact]
        public void Should_have_error_when_CategoryRequestUpdate_ParentCategoryId_is_negativ()
        {
            //arrange 
            _validator = new CategoryRequestUpdateValidator();
            const int negativ_Result = -1;
            //act
            var model = new CategoryRequestUpdate { ParentCategoryId=negativ_Result};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }
    }
}
