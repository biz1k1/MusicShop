using Microsoft.AspNetCore.Authorization;
using MusicShop.Domain.Enums;
using System.Globalization;

namespace MusicShop.Application.Services.Authorization.PermissionService
{
    public class PermissionRequierment : IAuthorizationRequirement
    {
        public PermissionRequierment(Permissions[] permissions)
        {
            Permissions = permissions;
        }
        public Permissions[] Permissions { get; set; } = [];
    }
}
