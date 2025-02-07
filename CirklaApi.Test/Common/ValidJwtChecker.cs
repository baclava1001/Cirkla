using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;

namespace Test.CirklaApi.Common
{
    // Compare credentials in token to user properties
    public static class ValidJwtChecker
    {
        public static bool IsValid(string token, User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token)) return false;

            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken.Claims.Any(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == user.UserName)
                   && jwtToken.Claims.Any(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == user.Email)
                   && jwtToken.Claims.Any(c => c.Type == CustomClaimTypes.UserId && c.Value == user.Id);
        }
    }
}
