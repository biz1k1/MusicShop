using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface IUserRepository:IRepository<UserEntity>
    {
        Task<IEnumerable<UserEntity>> GetAllUsersIncludeRoleAsync();
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity> GetUserByLoginAsync(string email);
        HashSet<Permissions> GetUserPermissions(int userId);
        Task<UserEntity> GetUserIncludeRoleAsync(int id);
    }
}
