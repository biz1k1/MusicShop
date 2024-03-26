using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
