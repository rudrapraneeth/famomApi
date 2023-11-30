using HomeMade.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Security
{
   public interface ISecurityContext
    {
       User User { get; set; }
    }
}
