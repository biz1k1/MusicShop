using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Domain.Model.Core;
using MusicShop.Infrastructure.Data;

namespace MusicShop.Infrastructure.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        protected readonly DataContext _dbContext;
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserEntity>> GetAllUsersIncludeRoleAsync()
        {
            return await _dbContext.Users.Include(x=>x.Roles).ToListAsync();

        }
        public async Task<UserEntity?> GetUserIncludeRoleAsync(int id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).Include(x => x.Roles).FirstOrDefaultAsync();
        }
        public async Task<UserEntity?> GetUserByLoginAsync(string login)
        {
            return await _dbContext.Users.Where(x=>x.Login==login).FirstOrDefaultAsync();
        }

        public  HashSet<Permissions> GetUserPermissions(int userId)
        {
            var roles = _dbContext.Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .ThenInclude(d => d.Permissions)
                .Where(x => x.Id == userId)
                .Select(x => x.Roles)
                .ToList();
            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(d=>(Permissions)d.Id)
                .ToHashSet();
        }

    }
}
