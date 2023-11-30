using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Security.Authentication
{
    public interface ITokenBuilder
    {
        string BuildToken(string name, string[] roles, DateTime expirationDate);
    }
}
