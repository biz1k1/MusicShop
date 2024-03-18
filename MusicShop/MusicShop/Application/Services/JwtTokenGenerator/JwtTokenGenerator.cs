using Microsoft.IdentityModel.Tokens;
using MusicShop.Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace MusicShop.Application.Services.JwtTokenGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string firstname, string lastname)
        {
            
            var key = Convert.ToBase64String(new HMACSHA256().Key);


            var Credentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,firstname+" "+lastname),
                new Claim(JwtRegisteredClaimNames.GivenName,firstname),
                new Claim(JwtRegisteredClaimNames.FamilyName,lastname),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };
            var securityToken = new JwtSecurityToken(
                issuer: "MusicShop",
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
