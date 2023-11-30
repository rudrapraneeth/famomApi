using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly FamomAuditContext _context;
        public ProfileRepository(FamomAuditContext context)
        {
            _context = context;
        }

        public async Task Save(RatingsModel ratings)
        {
            var chefId = _context.SubOrder.Where(x => x.SubOrderId == ratings.SubOrderId).Select(y => y.Post.ChefId).FirstOrDefault();
            _context.Rating.Add( new Rating { Rating1 = ratings.Rating, SubOrderId = ratings.SubOrderId, ChefId = chefId, Review = ratings.Review });
            await _context.SaveChangesAsync();
        }
    }
}
