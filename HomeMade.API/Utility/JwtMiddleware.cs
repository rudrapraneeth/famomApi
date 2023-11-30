using HomeMade.Api.Models;
using HomeMade.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Api.Utility
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtAuthenticationConfig _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtAuthenticationConfig> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var user = new UserModel();
                jwtToken.Claims.ToList().ForEach(x =>
                {
                    if (x.Type == "userName")
                        user.UserName = x.Value;
                    if (x.Type == "email")
                        user.Email = x.Value;
                    if (x.Type == "applicationUserId")
                        user.ApplicationUserId = int.Parse(x.Value);
                    if (x.Type == "apartmentId")
                        user.ApartmentId = int.Parse(x.Value);
                });

                // attach user to context on successful jwt validation
                context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
