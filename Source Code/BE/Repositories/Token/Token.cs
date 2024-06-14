using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Token
{
    public class Token : IToken
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public Token(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }


        public async Task<string> CreateToken(Users user)
        {


            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsersId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(CustomeClaimType.Role, user.Role.Name),
            };
            

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken
            ( _config["JWT:Issuer"],
            _config["JWT:Audience"],
            authClaims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

    public static class CustomeClaimType
    {
        public const string Role = "Role";
    }
}
