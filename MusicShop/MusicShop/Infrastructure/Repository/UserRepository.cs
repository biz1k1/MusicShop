using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<User>> GetAllCategoryAsync()
        {
            return await GetAll().ToListAsync();

        }
        public async Task<User> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
