using Expo.Server.Models;
using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using HomeMade.Infrastructure.Data.DbContext;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Repositories
{
    public class ExpoRepository : IExpoRepository
    {
        private readonly FamomAuditContext _context;
        private readonly FamomContext _famomContext;
        private readonly ServicebusConfig _servicebusConfig;
        private ILogger<ExpoRepository> _logger;
        public ExpoRepository(FamomAuditContext context, IOptions<ServicebusConfig> servicebusConfig, FamomContext famomContext, ILogger<ExpoRepository> logger)
        {
            _context = context;
            _servicebusConfig = servicebusConfig.Value;
            _logger = logger;
            _famomContext = famomContext;
        }

        public async Task UpdateExpoToken(int userId, string token)
        {
            var result = await _context.ApplicationUser.FindAsync(userId);
            result.ExpoPushToken = token;
            await _context.SaveChangesAsync();
        }

        public async Task SendCancelOrderNotification(int subOrderId, int userTypeId)
        {
            var pushToken = string.Empty;
            int notificationUserType;
            if (userTypeId == (int)Core.Enums.UserType.CUSTOMER)
            {
                pushToken = await _context.SubOrder
                                        .Where(x => x.SubOrderId == subOrderId)
                                        .Select(x => x.Post.Chef.ApplicationUser.ExpoPushToken)
                                        .FirstOrDefaultAsync();
                notificationUserType = (int)Core.Enums.UserType.CHEF;
            }
            else
            {
                pushToken = await _context.SubOrder
                                        .Where(x => x.SubOrderId == subOrderId)
                                        .Select(x => x.Order.Customer.ApplicationUser.ExpoPushToken)
                                        .FirstOrDefaultAsync();
                notificationUserType = (int)Core.Enums.UserType.CUSTOMER;
            }

            if (!string.IsNullOrEmpty(pushToken))
            {
                //TODO: Get from cache instead of calling from db everytime.
                var notificationTypes = await _context.NotificationType.ToListAsync();
                await SendNotification(notificationTypes, pushToken, "ORDCAN", notificationUserType, subOrderId.ToString());

                //Cancel notifications which are scheduled.
                await CancelScheduledNotification(subOrderId);
            }
        }

        public async Task SendReadyNotification(OrderModel order)
        {
            var customerPushToken = await _context.SubOrder
                                        .Where(x => x.SubOrderId == order.SubOrderId)
                                        .Select(x => x.Order.Customer.ApplicationUser.ExpoPushToken)
                                        .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(customerPushToken))
            {
                //TODO: Get from cache instead of calling from db everytime.
                var notificationTypes = await _context.NotificationType.ToListAsync();
                var notificationType = order.DeliveryType == (int)Core.Enums.DeliveryType.DELIVERY ? "ORDDEL" : "ORDPIK";
                await SendNotification(notificationTypes, customerPushToken, notificationType, (int)Core.Enums.UserType.CUSTOMER, order.SubOrderId.ToString());
            }
        }

        public async Task CancelScheduledNotification(int subOrderId)
        {
            var sequenceNumbers = await _context.PushNotification.Where(x => x.ReferenceId == subOrderId && x.ReferenceValue == "ORDER")
                                                        .Select(x => x.SequenceNumber)
                                                        .ToListAsync();
            sequenceNumbers.ForEach(async x => {
                try
                {
                    await Data.ServiceBus.Queue.CancelMessageAsync(_servicebusConfig, x);
                }
                catch (Exception)
                {
                    return;
                }
            });
        }

        public async Task ScheduleRatingNotification(OrderModel orderModel, string userName)
        {
            var customerPushToken = await _context.SubOrder
                                                  .Where(x => x.SubOrderId == orderModel.SubOrderId)
                                                  .Select(x => x.Order.Customer.ApplicationUser.ExpoPushToken)
                                                  .FirstOrDefaultAsync();

            //TODO: Get from cache instead of calling from db everytime.
            var notificationType = await _context.NotificationType.FirstOrDefaultAsync(x=>x.NotificationTypeCode == "RATREQ");

            var ticketReq = GetPushTicketRequest(customerPushToken, string.Format(notificationType.Title, orderModel.Title), string.Format(notificationType.Body, userName, orderModel.Title), new { notificationTypeCode = "RATREQ", subOrderId = orderModel.SubOrderId, title = orderModel.Title, userName } );
            Data.ServiceBus.Queue.SendMessageAsync(_servicebusConfig, ticketReq, DateTime.Now.AddMinutes(90)); //change to config - 120
        }

        public async Task SendPlaceOrderNotification(OrderModel order, int userId)
        {
            var chef = await _context.Post
                                 .Where(x => x.PostId == order.PostId)
                                 .Select(x => x.Chef.ApplicationUser)
                                 .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(chef.ExpoPushToken))
            {
                //TODO: Get from cache instead of calling from db everytime.
                var notificationTypes = await _context.NotificationType.ToListAsync();

                //First send notification to chef.
                await SendNotification(notificationTypes, chef.ExpoPushToken, "ORDPLA", (int)Core.Enums.UserType.CHEF, order.Title);

                //schedule notifications if delivery time is more than two hours from now
                if ((order.DeliveryTime - DateTime.Now).TotalMinutes > 120) //change to config
                {
                    var customerPushToken = _context.ApplicationUser.FirstOrDefault(x => x.ApplicationUserId == userId).ExpoPushToken;
                    if (order.DeliveryType == (int)Core.Enums.DeliveryType.PICKUP)
                    {
                        //set pickup reminder for chef
                        await ScheduleNotification(notificationTypes, chef.ExpoPushToken, "PICORD", (int)Core.Enums.UserType.CHEF, order.DeliveryTime, chef.ApplicationUserId, order.SubOrderId, "ORDER");

                        //set pickup reminder for customer
                        await ScheduleNotification(notificationTypes, customerPushToken, "PICORD", (int)Core.Enums.UserType.CUSTOMER, order.DeliveryTime, userId, order.SubOrderId, "ORDER");
                    }

                    if (order.DeliveryType == (int)Core.Enums.DeliveryType.DELIVERY)
                    {
                        //set delivery reminder for chef
                        await ScheduleNotification(notificationTypes, chef.ExpoPushToken, "DELORD", (int)Core.Enums.UserType.CHEF, order.DeliveryTime, chef.ApplicationUserId, order.SubOrderId, "ORDER");

                        //set pickup reminder for customer
                        await ScheduleNotification(notificationTypes, customerPushToken, "DELORD", (int)Core.Enums.UserType.CUSTOMER, order.DeliveryTime, userId, order.SubOrderId, "ORDER");
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task BroadcastNotification(string notification, string title, int apartmentId, string userTypeCode)
        {
            var chefPushTokens = _context.Chef.Select(x => x.ApplicationUser.ExpoPushToken).ToList();
            if (userTypeCode == "CUST")
            {
                var userPushTokens = _context.ApplicationUser.Select(x=>x.ExpoPushToken).ToList();
                var customers = userPushTokens.Except(chefPushTokens);
                customers.ToList().ForEach(x =>
                {
                    var pushTicketReq = GetPushTicketRequest(x, title, notification, null);
                    Data.ServiceBus.Queue.SendMessageAsync(_servicebusConfig, pushTicketReq);
                });
            }

            if (userTypeCode == "CHEF")
            {
                chefPushTokens.ToList().ForEach(x =>
                {
                    var pushTicketReq = GetPushTicketRequest(x, title, notification, null);
                    Data.ServiceBus.Queue.SendMessageAsync(_servicebusConfig, pushTicketReq);
                });
            }
        }

        public async Task<List<string>> GetPushTokensForApt(int apartmentId)
        {
            return await _famomContext.UserApartment.Where(x => x.ApartmentId == apartmentId && x.ApplicationUser.ExpoPushToken != null).Select(y => y.ApplicationUser.ExpoPushToken).ToListAsync();
        }

        private async Task ScheduleNotification(List<NotificationType> notificationTypes, string pushToken, string notificationTypeCode, int userTypeId, DateTime deliveryTime, int appUserId, int referenceId, string referenceValue)
        {
            var notificationType = notificationTypes.FirstOrDefault(x => x.NotificationTypeCode == notificationTypeCode && x.UserTypeId == userTypeId);
            var ticketReq = GetPushTicketRequest(pushToken, notificationType.Title, notificationType.Body, new { userTypeId });
            var sequenceNumber = await Data.ServiceBus.Queue.SendMessageAsync(_servicebusConfig, ticketReq, deliveryTime.AddMinutes(-30));
            var pushNotification = new PushNotification()
            {
                ApplicationUserId = appUserId,
                NotificationTypeId = notificationType.NotificationTypeId,
                CreateDateTime = DateTime.Now,
                ReferenceId = referenceId,
                ReferenceValue = referenceValue,
                ScheduleDateTime = deliveryTime.AddMinutes(-30),
                SequenceNumber = (int)sequenceNumber
            };

            _context.PushNotification.Add(pushNotification);
        }

        private async Task SendNotification(List<NotificationType> notificationTypes, string pushToken, string notificationTypeCode, int userTypeId, string placeHolder)
        {
            var notificationType = notificationTypes.FirstOrDefault(x => x.NotificationTypeCode == notificationTypeCode && x.UserTypeId == userTypeId);
            var pushTicketReq = GetPushTicketRequest(pushToken, notificationType.Title, string.Format(notificationType.Body, placeHolder), new { userTypeId });
            await Data.ServiceBus.Queue.SendMessageAsync(_servicebusConfig, pushTicketReq);
        }

        private PushTicketRequest GetPushTicketRequest(string pushToken, string title, string body, object data)
        {
            return new PushTicketRequest()
            {
                PushTo = new List<string>() { pushToken },
                PushTitle = title,
                PushBody = body,
                PushData = data ?? new object(),
                PushSound = "default"
            };
        }
    }
}
