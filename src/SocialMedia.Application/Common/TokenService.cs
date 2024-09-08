using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Role.RoleDto;
using SocialMedia.Application.Users.UserDtos;
using SocialMedia.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace SocialMedia.Application.Common
{
    public interface ITokenService
    {
        public string CreateToken(UserDto user, Roles roles);
    }
    public class TokenService : ITokenService


    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(UserDto user, Roles roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),

                new Claim("id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, roles.Name),
                new Claim("Role", roles.Name),


            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


    }
}
