using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;

namespace HomeMade.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<SubOrder>> GetOrders(int userId);
        Task<List<SubOrder>> GetSales(int userId);
        Task<Orders> CreateOrder(Orders order, int postId, int quantity);
        Task UpdateStatus(int subOrderId, int statusId);
        Task<int> GetCustomerId(int userId);
    }
}
