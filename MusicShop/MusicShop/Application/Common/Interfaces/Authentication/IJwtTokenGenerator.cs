using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserEntity user);
    }
}
