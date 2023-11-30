using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using HomeMade.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly FamomAuditContext _context;
        public LookupRepository(FamomAuditContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            return await _context.Category.Select(x => new CategoryModel { Name = x.Name, Key = x.CategoryId }).ToListAsync();
        }
    }
}
