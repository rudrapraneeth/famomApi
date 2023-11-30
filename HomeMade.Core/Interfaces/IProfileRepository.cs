using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface IProfileRepository
    {
        Task Save(RatingsModel ratings);
    }
}
