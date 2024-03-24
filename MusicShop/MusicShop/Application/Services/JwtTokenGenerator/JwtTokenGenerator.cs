using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Application.Services.Authentication.Identity;
using MusicShop.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace MusicShop.Application.Services.JwtTokenGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) {
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateToken(User user)
        {


            var Credentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name,user.FirstName+" "+user.LastName),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString()),
                //new Claim(ClaimsIdentity.Def)

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
