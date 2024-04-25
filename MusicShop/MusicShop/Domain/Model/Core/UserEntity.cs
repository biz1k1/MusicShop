using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model.Aunth;

namespace MusicShop.Domain.Model.Core
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
