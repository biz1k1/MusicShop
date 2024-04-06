using Microsoft.AspNetCore.Authorization;
using MusicShop.Domain.Enums;

namespace MusicShop.Application.Services.Authorization.PermissionService
{
    public class HasPermitionAttribute:AuthorizeAttribute
    {
        public HasPermitionAttribute(Permissions[] permissions):base(policy: permissions.ToString())
        {

        }
    }
}
