using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTApplication.Models;
using JWTApplication.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
namespace JWTApplication.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1","pass1" },
            {"user2","pass2" },
            {"user3","pass3" },
        };
        public JWTManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Tokens Authonticate(Users user)
        {
            if(!UserRecords.Any(x=> x.Key==user.Username && x.Value == user.Password))
            {
                return null;
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(configuration["JWT:key"]);
            var tokendescreptior = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenhandler.CreateToken(tokendescreptior);
            return new Tokens { Token = tokenhandler.WriteToken(token) };
        }
    }
}
