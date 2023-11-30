using HomeMade.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Domain.Repositories
{
    public interface ILoginRepository
    {
        Task<User> FetchUser(string username);
        Task<string> Register(User user);
    }
}
