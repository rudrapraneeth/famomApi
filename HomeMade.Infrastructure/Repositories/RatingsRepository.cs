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
    public class RatingsRepository : IRatingsRepository
    {
        private readonly FamomAuditContext _context;
        public RatingsRepository(FamomAuditContext context)
        {
            _context = context;
        }

        public async Task Submit(RatingsModel ratings)
        {
            var chefId = _context.SubOrder.Where(x => x.SubOrderId == ratings.SubOrderId).Select(y => y.Post.ChefId).FirstOrDefault();
            _context.Rating.Add( new Rating { Rating1 = ratings.Rating, SubOrderId = ratings.SubOrderId, ChefId = chefId, Review = ratings.Review });
            await _context.SaveChangesAsync();
        }

        public async Task<ReviewsModel> GetReviews(int applicationUserId)
        {
            var result = await _context.Chef
                                       .Include(x => x.Rating)
                                       .Where(y => y.ApplicationUserId == applicationUserId)
                                       .Select(x => new ReviewsModel
                                       {
                                           Rating = x.Rating.Any() ? x.Rating.Average(y => y.Rating1) : 0,
                                           NumberOfReviews = x.Rating.Count(),
                                           Reviews = x.Rating.Where(r=> !string.IsNullOrEmpty(r.Review)).Select(z=> new ReviewModel { RatingId = z.RatingId, CustomerUsername = z.SubOrder.Order.Customer.ApplicationUser.UserName, Rating = Convert.ToDouble(z.Rating1), Review = z.Review}).ToList()
                                       }).FirstOrDefaultAsync();
            return result;
        }
    }
}
