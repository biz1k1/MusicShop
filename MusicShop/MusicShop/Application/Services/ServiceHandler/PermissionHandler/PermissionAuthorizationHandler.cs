using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using MusicShop.Application.Services.Authorization.PermissionService;
using System.IdentityModel.Tokens.Jwt;

namespace MusicShop.Application.Services.ServiceHandler.PermissionHandler
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequierment>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequierment requirement)
        {

            var userId = context.User.Claims.FirstOrDefault(
                x => x.Type == CustomClaims.UserId);

            if (userId == null || !int.TryParse(userId.Value, out var id))
            {
                return Task.CompletedTask;
            }
            using var scope = _serviceScopeFactory.CreateScope();

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var permissions =  permissionService.GetPermissions(id);

            if (permissions.Intersect(requirement.Permissions).Any())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

    }
}
