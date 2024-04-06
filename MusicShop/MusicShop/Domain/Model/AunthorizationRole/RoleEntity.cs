using MusicShop.Domain.Model.Core;

namespace MusicShop.Domain.Model.Aunth
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PermissionsEntity> Permissions { get; set; } = new List<PermissionsEntity>();

        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
