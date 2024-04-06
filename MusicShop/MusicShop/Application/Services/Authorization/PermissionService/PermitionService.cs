using MusicShop.Domain.Enums;
using MusicShop.Infrastructure.Repository;

namespace MusicShop.Application.Services.Authorization.PermissionService
{
    public class PermitionsService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermitionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HashSet<Permissions> GetPermissions(int userId)
        {
            return _unitOfWork.User.GetUserPermissions(userId);
        }
    }
}
