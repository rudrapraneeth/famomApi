using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HomeMade.Security.Authentication
{
    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(string name, string[] roles, DateTime expirationDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();

            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(name, "Bearer"),
                claims
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOptions.Issuer,
                Audience = TokenAuthOptions.Audience,
                SigningCredentials = TokenAuthOptions.SigningCredentials,
                Subject = identity,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }
    }
}
