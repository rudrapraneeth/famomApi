using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeMade.Core;
using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HomeMade.Core.ViewModels;
using Microsoft.Extensions.Options;
using HomeMade.Infrastructure.Data.Configurations;
using HomeMade.Infrastructure.Data.DbContext;
using Expo.Server.Models;
using Newtonsoft.Json;

namespace HomeMade.Infrastructure.Repositories
{
    public class OrderRespository : IOrderRepository
    {
        private FamomAuditContext _context = null;

        public OrderRespository(FamomAuditContext context)
        {
            _context = context;
        }

        public async Task<Orders> CreateOrder(Orders order, int postId, int quantity)
        {
            _context.Orders.Add(order);
            var post = _context.Post.Find(postId);
            if (post.AvailabilityTypeId == (int)Core.Enums.AvailabilityType.NOW && post.Quantity > 0)
            {
                post.Quantity -= quantity;
            }
            
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<SubOrder>> GetOrders(int userId)
        {
            //TODO: start with application user
            //TODO: Select only required fields. In short move the controller code to here.
            return await _context.SubOrder
                                 .Include(x=>x.Rating)
                                 .Include(x => x.Order)
                                 .ThenInclude(y=>y.Customer)
                                 .Include(x => x.Post)
                                 .ThenInclude(x=>x.Chef)
                                 .ThenInclude(z=>z.ApplicationUser)
                                 .ThenInclude(ap=>ap.UserApartment)
                                 .Include(x=>x.Post.Menu)
                                 .Include(x=>x.Post.QuantityType)
                                 .Where(x => x.Order.Customer.ApplicationUserId == userId)
                                 .ToListAsync();
        }

        public async Task<List<SubOrder>> GetSales(int userId)
        {
            //TODO: start with application user
            //TODO: Select only required fields. In short move the controller code to here.
            return await _context.SubOrder
                                 .Include(x => x.Order)
                                 .ThenInclude(y => y.Customer)
                                 .ThenInclude(z => z.ApplicationUser)
                                 .ThenInclude(ap => ap.UserApartment)
                                 .Include(x => x.Post)
                                 .ThenInclude(x => x.Chef)
                                 .Include(x => x.Post.Menu)
                                 .Include(x => x.Post.QuantityType)
                                 .Where(x => x.Post.Chef.ApplicationUserId == userId)
                                 .ToListAsync();
        }

        public async Task UpdateStatus(int subOrderId, int statusId)
        {
            var order = await _context.SubOrder.Include(x => x.Order)
                                               .Include(x=>x.Post)
                                               .FirstOrDefaultAsync(x => x.SubOrderId == subOrderId);
            order.StatusId = statusId;
            order.Order.StatusId = statusId;
            if (statusId == (int)Core.Enums.OrderStatus.CANCELED)
            {
                order.Post.Quantity += order.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Orders>> MyReceivedOrders(int customerId)
        {
            return await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task<int> GetCustomerId(int userId)
        {
            return await _context.Customer.Where(x => x.ApplicationUserId == userId)
                                          .Select(x=>x.CustomerId)
                                          .FirstOrDefaultAsync();
        }
    }
}
