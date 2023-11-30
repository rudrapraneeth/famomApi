using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.Interfaces
{
    public interface ISecurityService
    {
        bool VerifyPassword(string passwordGuess, string actualSavedHashResults);
        string CreatePasswordHash(string password);
    }
}
