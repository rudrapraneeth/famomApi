using HomeMade.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface IExpoRepository
    {
        Task UpdateExpoToken(int userId, string token);
        Task SendPlaceOrderNotification(OrderModel order, int userId);
        Task SendCancelOrderNotification(int subOrderId, int userTypeId);
        Task SendReadyNotification(OrderModel order);
        Task CancelScheduledNotification(int subOrderId);
        Task ScheduleRatingNotification(OrderModel orderModel, string userName);
        Task BroadcastNotification(string notification, string title, int apartmentId, string userTypeCode);
        Task<List<string>> GetPushTokensForApt(int apartmentId);
    }
}
