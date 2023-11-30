using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Infrastructure.Data;
using System.Collections.Generic;
using HomeMade.Infrastructure.Data.DbContext;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeMade.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly FamomAuditContext _context;
        private readonly ISecurityService _security;
        public LoginRepository(FamomAuditContext context, ISecurityService security)
        {
            _context = context;
            _security = security;
        }

        public async Task<ApplicationUser> GetUser(string mobileNumber, string password)
        {
            //TODO: Move to common method 
            var user = await _context.ApplicationUser
                                .Include(x => x.UserApartment)
                                .Include(x => x.Chef)
                                .Where(x => x.MobileNumber == mobileNumber)
                                .OrderByDescending(x => x.CreateDateTime)
                                .FirstOrDefaultAsync();


            if (user == null || !_security.VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        public async Task<(bool, string)> AnyExistingUser(string userName, string mobileNumber)
        {
            var user = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.UserName == userName || x.MobileNumber == mobileNumber);
            if (user != null)
            {
                string errorMessage = user.MobileNumber == mobileNumber ? "Mobile Number you entered is already registered" : "Username you requested already exists";
                return (true, errorMessage);
            }

            return (false, string.Empty);
        }

        public async Task<bool> AnyExistingMobileNumber(string mobileNumber)
        {
            var isExistingUser = await _context.ApplicationUser.AnyAsync(x => x.MobileNumber == mobileNumber);
            return isExistingUser;
        }

        public async Task<ApplicationUser> UpdateIsVerified(string mobileNumber)
        {
            //TODO: Move to common method 
            var user = await _context.ApplicationUser
                                .Include(x => x.UserApartment)
                                .Include(x => x.Chef)
                                .Where(x=>x.MobileNumber == mobileNumber)
                                .OrderByDescending(x=>x.CreateDateTime)
                                .FirstOrDefaultAsync();
            user.IsVerified = true;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> UpdatePassword(string mobileNumber, string password)
        {
            //TODO: Move to common method 
            var user = await _context.ApplicationUser
                                .Include(x => x.UserApartment)
                                .Include(x => x.Chef)
                                .Where(x => x.MobileNumber == mobileNumber)
                                .OrderByDescending(x => x.CreateDateTime)
                                .FirstOrDefaultAsync();
            user.PasswordHash = _security.CreatePasswordHash(password);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task RegisterUser(ApplicationUser user)
        {
            user.PasswordHash = _security.CreatePasswordHash(user.PasswordHash);
            _context.ApplicationUser.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
