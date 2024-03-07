using MusicShop.Presentation.Common.DTOs.Authentication;
namespace MusicShop.Application.Services.Authentication
{
    public interface IAuthService
    {
        AuthenticationResult Register(string firstName, string lastName, string email, string password);
        AuthenticationResult Login(string email,string password);
    }
}
