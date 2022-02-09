using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Infrastructure.Utils
{
    public class StaticFunctions
    {
        public static string GenerateJwtToken(Guid userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.JWT_SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(Constants.JWT_EXPIRATION_HOURS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateVerificationCode()
        {
            Random random = new Random();

            return new string (Enumerable.Repeat(Constants.CODE_CHARACTERS, Constants.CODE_LENGTH)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
