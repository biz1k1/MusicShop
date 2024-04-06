using MusicShop.Application.Services.Authentication;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Services.ServiceHandler
{
    public interface IAuthenticationServiceHandler
    {
        Task<AuthenticationResult> Register(RegisterRequest request);
        Task<AuthenticationResult> Login(LoginRequest request);
    }
}
