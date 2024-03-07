using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Common.Interfaces.Persistence;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Services.Authentication
{
    public class AuthenticationService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IUserRepository userRepository,IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public AuthenticationResult Login(string email, string password)
        {
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User doesnt exist");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtTokenGenerator.GenerateToken();
            return new AuthenticationResult(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    email,
                    token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //Проверна на дубликат
            if (_userRepository.GetUserByEmail(email) == null)
            {
                throw new Exception("Duplicate email");
            }
            //Создание юзера
            var User = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            //Создание jwt токена
            var token = _jwtTokenGenerator.GenerateToken();
            return Results;
        }
    }
}
