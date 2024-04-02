using MusicShop.Application.Services.Authentication;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Services.ServiceHandler
{
    public interface IAuthenticationServiceHandler
    {
        AuthenticationResult Register(RegisterRequest request);
        AuthenticationResult Login(LoginRequest request);
    }
}
