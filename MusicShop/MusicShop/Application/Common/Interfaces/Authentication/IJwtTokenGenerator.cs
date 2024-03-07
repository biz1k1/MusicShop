namespace MusicShop.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int id, string firstname, string lastname);
    }
}
