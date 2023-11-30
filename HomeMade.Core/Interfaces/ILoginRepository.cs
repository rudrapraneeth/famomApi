using HomeMade.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface ILoginRepository
    {
        Task<ApplicationUser> UpdateIsVerified(string mobileNumber);
        Task<ApplicationUser> UpdatePassword(string mobileNumber, string password);
        Task<ApplicationUser> GetUser(string mobileNumber, string password);
        Task<(bool, string)> AnyExistingUser(string userName, string mobileNumber);
        Task<bool> AnyExistingMobileNumber(string mobileNumber);
        Task RegisterUser(ApplicationUser user);
    }
}
