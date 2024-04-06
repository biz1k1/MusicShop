using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.Authorization.PermissionService;
using MusicShop.Domain.Model.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MusicShop.Application.Services.JwtTokenGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) {
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateToken(UserEntity user)
        {


            var Credentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(CustomClaims.UserId,user.Id.ToString()),

            };
            var securityToken = new JwtSecurityToken(
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtSettings.ExpireHour),
                signingCredentials: Credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
