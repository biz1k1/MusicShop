using MusicShop.Presentation.Common.DTOs.Authentication;
using FluentResults;
namespace MusicShop.Application.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Register(RegisterRequest request);
        Task<AuthenticationResult> Login(LoginRequest loginRequest);
    }
}
