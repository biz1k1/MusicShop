using MusicShop.Presentation.Common.DTOs.Authentication;
using FluentResults;
namespace MusicShop.Application.Services.Authentication
{
    public interface IAuthService
    {
        AuthenticationResult Register(RegisterRequest request);
        AuthenticationResult Login(LoginRequest loginRequest);
    }
}
