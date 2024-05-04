using FluentValidation.TestHelper;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.UnitTests.Application.Common.Behavior
{
    public class RegisterRequestValidatorTests
    {
        private RegisterRequestValidator _validator;
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Login_Too_High()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            string maximum_result = string.Concat(Enumerable.Repeat("a", 16));
            //act
            var model = new RegisterRequest { Login = maximum_result };
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Login);
        }
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Login_is_null()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            //act
            var model = new RegisterRequest { Login = null};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Login);
        }
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Email_is_null()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            //act
            var model = new RegisterRequest { Login = null};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Email);
        }
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Email_is_not_EmailAndress()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            const string email_adress = "adress@mail.ru";
            //act
            var model = new RegisterRequest { Login = email_adress};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Email);
        }
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Password_is_null()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            //act
            var model = new RegisterRequest { Password=null};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Password);
        }
        [Fact]
        public void Should_have_error_when_RegisterRequestValidator_Password_is_Too_High()
        {
            //arrange 
            _validator = new RegisterRequestValidator();
            string maximum_result = string.Concat(Enumerable.Repeat("a", 21));
            //act
            var model = new RegisterRequest { Password=maximum_result};
            var result = _validator.TestValidate(model);
            //assert
            result.ShouldHaveValidationErrorFor(register => register.Password);
        }

        

    }
}
