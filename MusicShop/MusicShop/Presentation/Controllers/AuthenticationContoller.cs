using Microsoft.AspNetCore.Mvc;
using MusicShop.Presentation.Common.DTOs.Authentication;
using FluentValidation;
using FluentValidation.Results;
using MusicShop.Application.Common.Behavior;
using Microsoft.AspNetCore.Authentication;
using MusicShop.Application.Services.ServiceHandler;
namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly IAuthenticationServiceHandler _authenticationHandler;
        private IValidator<RegisterRequest> _validator;
        public AuthenticationContoller(IAuthenticationServiceHandler authenticationHandler,IValidator<RegisterRequest> validator)
        {
            _validator = validator;
            _authenticationHandler = authenticationHandler;
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
            _authenticationHandler.Register(request);
            return Ok("Registration was successful");
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var context = HttpContext;
            var loginResult = _authenticationHandler.Login(login);
            context.Response.Cookies.Append("Not-a-very-tasty-cookie", loginResult.Token);
            return Ok("The login was successful");
        }
    }
}
