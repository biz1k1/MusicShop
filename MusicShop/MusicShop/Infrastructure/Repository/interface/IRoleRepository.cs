using MusicShop.Domain.Model.Aunth;

namespace MusicShop.Infrastructure.Repository
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        Task<RoleEntity> GetRoleByIdAsync(int id);
        RoleEntity GetRoleById(int id);
    }
}