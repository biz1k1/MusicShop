using MusicShop.Application.Services.Authentication;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Services.ServiceHandler
{
    public class AuthenticationServiceHandler : IAuthenticationServiceHandler
    {
        private readonly IAuthService _authService;
        public AuthenticationServiceHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<AuthenticationResult> Login(LoginRequest request)
        {
            var result= _authService.Login(request);
            return result;
        }

        public Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var result = _authService.Register(request);
            return result;
        }
    }
}
