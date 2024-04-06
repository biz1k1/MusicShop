using MusicShop.Application.Services.DbInitializer;
using MusicShop.Domain.Model.Core;

namespace MusicShop.Application.Services.ServiceHandler.User
{
    public class UserServiceHandler : IUserServiceHandler
    {
        private readonly IDbInitializer _dbInitializer;
        public UserServiceHandler(IDbInitializer dbInitializer)
        {
            _dbInitializer = dbInitializer;
        }
        public Task<List<UserEntity>> DbInitializer()
        {
            return _dbInitializer.SeedUserAdmin();
        }
    }
}
