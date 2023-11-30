using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FamomAuditContext _context;
        public AddressRepository(FamomAuditContext context)
        {
            _context = context;
        }

        public async Task<List<Apartment>> GetApartments()
        {
            return await _context.Apartment
                                 .Include(y => y.Address)
                                 .Where(x=> x.IsActive)
                                 .ToListAsync();
        }
    }
}
