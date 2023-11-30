using HomeMade.DataAccess.Context;
using HomeMade.Domain.Models;
using HomeMade.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.DataAccess.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HomeMadeDbContext _context;
        public LoginRepository(HomeMadeDbContext context)
        {
            _context = context;
        }

        public async Task<User> FetchUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync( x => x.UserName == username);
        }

        public async Task<string> Register(User user)
        {       
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserName;
        }        
    }
}
