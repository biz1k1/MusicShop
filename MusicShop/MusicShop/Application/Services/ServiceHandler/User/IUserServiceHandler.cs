using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.ServiceHandler.User
{
    public interface IUserServiceHandler
    {
        Task<List<UserEntity>> DbInitializer();
    }
}
