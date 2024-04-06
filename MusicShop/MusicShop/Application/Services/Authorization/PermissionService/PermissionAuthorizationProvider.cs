//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Options;
//using MusicShop.Domain.Enums;

//namespace MusicShop.Application.Services.Authorization.PermissionService
//{
//    public class PermissionAuthorizationProvider : DefaultAuthorizationPolicyProvider
//    {
//        public PermissionAuthorizationProvider(IOptions<AuthorizationOptions> options) : base(options)
//        {
//        }
//        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
//        {
//            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);
//            if(policy is not null)
//            {
//                return policy;
//            }
//            return new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequierment(policyName)).Build();
//        }
//    }
//}
