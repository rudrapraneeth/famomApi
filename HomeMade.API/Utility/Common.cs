using Expo.Server.Client;
using Expo.Server.Models;
using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HomeMade.Api.Utility
{
    public static class Common
    {
        public static bool IsPostActive(string toDateTime)
        {
            var availableTo = DateTime.Parse(toDateTime);

            if (availableTo > DateTime.Now)
                return true;
            return false;
        }

        public static string GenerateToken(ApplicationUser user, string secretKey, string issuer, string audience)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signInCred = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signInCred);

            // Add Claims 
            var claims = new[] {
                new Claim("userName", user.UserName),
                new Claim("mobileNumber", user.MobileNumber),
                new Claim("applicationUserId", user.ApplicationUserId.ToString()),
                new Claim("apartmentId", user.UserApartment.FirstOrDefault()?.ApartmentId.ToString()),
                new Claim("chefId", user.Chef?.ChefId.ToString() ?? "0"),
                new Claim("expoPushToken", user.ExpoPushToken ?? "")
            };

            var payload = new JwtPayload(
                issuer: issuer,
                audience: audience,
                claims: claims,
                DateTime.Now,
                DateTime.Now.AddYears(10) // Todo: This will be set to lesser time once refresh token is implemented.
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateToken(UserModel user, string secretKey, string issuer, string audience)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signInCred = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signInCred);

            // Add Claims 
            var claims = new[] {
                new Claim("userName", user.UserName),
                new Claim("mobileNumber", user.MobileNumber),
                new Claim("applicationUserId", user.ApplicationUserId.ToString()),
                new Claim("apartmentId", user.ApartmentId.ToString()),
                new Claim("chefId", user.ChefId.ToString() ?? "0"),
                new Claim("expoPushToken", user.ExpoPushToken ?? "")
            };

            var payload = new JwtPayload(
                issuer: issuer,
                audience: audience,
                claims: claims,
                DateTime.Now,
                DateTime.Now.AddYears(10) // Todo: This will be set to lesser time once refresh token is implemented.
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
