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
     
        public static string GenerateVerificationCode()
        {
            Random random = new Random();

            return new string (Enumerable.Repeat(Constants.CODE_CHARACTERS, Constants.CODE_LENGTH)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
