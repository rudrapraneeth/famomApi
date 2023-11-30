using HomeMade.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Apartment>> GetApartments();
    }
}
