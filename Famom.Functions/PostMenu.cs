using System.Collections.Generic;
using System.Threading.Tasks;
using Expo.Server.Models;
using Famom.Functions.Shared;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Famom.Functions
{
    public class PostMenu
    {
        private readonly IExpoRepository _service;
        public PostMenu(IExpoRepository service)
        {
            this._service = service;
        }

        [FunctionName("PostMenu")]
        public async Task Run([ServiceBusTrigger("postmenu", Connection = "SbConnectionString")]string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<PostMenuQueueMessage>(myQueueItem);
            var pushTokens = await _service.GetPushTokensForApt(message.ApartmentId);
            string title = string.Empty;
            string body = string.Empty;

            if (message.AvailabilityTypeId == (int)HomeMade.Core.Enums.AvailabilityType.NOW)
            {
                title = $"{message.MenuTitle} is available now";
                if (message.Quantity > 0)
                {
                    body = $"Just {message.Quantity} {message.QuantityType} left. Order now before its too late";
                }

                else
                {
                    body = $"Order now before its too late";
                }
            }
            else if (message.AvailabilityTypeId == (int)HomeMade.Core.Enums.AvailabilityType.SCHPREORDER)
            {
                title = $"{message.MenuTitle} will be available soon";
                body = $"{message.ChefName} scheduled to make {message.MenuTitle} soon. Pre order now.";
            }
            else
            {
                title = $"{message.MenuTitle} is added to the menu";
                body = $"{message.ChefName} added {message.MenuTitle} to their menu, Try it out.";
            }

            pushTokens.ForEach(x =>
            {
                var pushRequest = new PushTicketRequest()
                {
                    PushTo = new List<string>() { x },
                    PushData = new { message.PostId },
                    PushSound = "default",
                    PushTitle=title,
                    PushBody = body
                };
                PushNotification.Send(pushRequest);
            });

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
