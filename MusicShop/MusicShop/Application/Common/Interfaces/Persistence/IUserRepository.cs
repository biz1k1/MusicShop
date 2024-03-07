using MusicShop.Domain.Model;

namespace MusicShop.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void Add(User user);
    }
}
