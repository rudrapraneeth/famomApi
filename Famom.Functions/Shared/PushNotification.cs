using Expo.Server.Client;
using Expo.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Famom.Functions.Shared
{
    public static class PushNotification
    {
        public static void Send(PushTicketRequest pushTicketRequest)
        {
            var expoSDKClient = new PushApiClient();
            var result = expoSDKClient.PushSendAsync(pushTicketRequest).GetAwaiter().GetResult();

            if (result?.PushTicketErrors?.Count > 0)
            {
                foreach (var error in result.PushTicketErrors)
                {
                    Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                }
            }

            ///If no errors, then wait for a the notifications to be delivered
            foreach (var ticketStatus in result.PushTicketStatuses)
            {
                Console.WriteLine($"TicketId & Status: {ticketStatus.TicketId} = {ticketStatus.TicketStatus}, {ticketStatus.TicketMessage}");
                var pushReceiptReq = new PushReceiptRequest()
                {
                    PushTicketIds = new List<string>() { "..." }
                };
            }
        }
    }
}
