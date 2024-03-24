using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Application.Services.Authentication;
using FluentResults;
using MusicShop.Application.Common.Errors;
using MusicShop.Presentation.Common.FilterError;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;
using System;
using MusicShop.Application.Common.Behavior;
namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private IValidator<RegisterRequest> _validator;
        public AuthenticationContoller(IAuthService authentication,IValidator<RegisterRequest> validator)
        {
            _validator = validator;
            _authenticationService = authentication;
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            ValidationResult validationResult=await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var registerResult = _authenticationService.Register(request);
            return Ok("Registration was successful");
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var context = HttpContext;
            var loginResult = _authenticationService.Login(login);
            context.Response.Cookies.Append("Not-a-very-tasty-cookie", loginResult.Token);
            return Ok("The login was successful");
        }
    }
}
