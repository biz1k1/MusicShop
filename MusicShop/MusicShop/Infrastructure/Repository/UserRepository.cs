using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;

namespace MusicShop.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await GetAll().ToListAsync();

        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
