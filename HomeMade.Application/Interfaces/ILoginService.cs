using HomeMade.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Application.Interfaces
{
    public interface ILoginService
    {
        Task<UserModel> Authenticate(string username, string password);
        Task<string> Register(string userName, string password);
    }
}
