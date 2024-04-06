using MusicShop.Domain.Enums;

namespace MusicShop.Application.Services.Authorization.PermissionService
{
    public interface IPermissionService
    {
        HashSet<Permissions> GetPermissions(int userId);
    }
}
