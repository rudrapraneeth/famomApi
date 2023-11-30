using Azure.Messaging.ServiceBus;
using Expo.Server.Models;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Data.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Data.ServiceBus
{
    public static class Queue
    {
        public static async Task<long> SendMessageAsync(ServicebusConfig config, PushTicketRequest request, DateTime? scheduleTime = null)
        {
            var payLoad = JsonConvert.SerializeObject(request);
            //create a Service Bus client
            await using ServiceBusClient client = new ServiceBusClient(config.ConnectionString);
            // create a sender for the queue 
            ServiceBusSender sender = client.CreateSender(config.NotificationQueue);

            // create a message that we can send
            ServiceBusMessage message = new ServiceBusMessage(payLoad);

            // send the message
            return await sender.ScheduleMessageAsync(message, scheduleTime ?? DateTime.Now).ConfigureAwait(false); ;
        }

        public static async Task CancelMessageAsync(ServicebusConfig config, long sequenceNumber)
        {
            //create a Service Bus client
            await using ServiceBusClient client = new ServiceBusClient(config.ConnectionString);
            // create a sender for the queue 
            ServiceBusSender sender = client.CreateSender(config.NotificationQueue);

            // cancel the message
            await sender.CancelScheduledMessageAsync(sequenceNumber).ConfigureAwait(false); ;
        }

        public static async Task SendPostMenuBroadcastMessageAsync(ServicebusConfig config, PostMenuQueueMessage request)
        {
            var payLoad = JsonConvert.SerializeObject(request);
            //create a Service Bus client
            await using ServiceBusClient client = new ServiceBusClient(config.ConnectionString);
            // create a sender for the queue 
            ServiceBusSender sender = client.CreateSender(config.PostMenuQueue);

            // create a message that we can send
            ServiceBusMessage message = new ServiceBusMessage(payLoad);

            // send the message
            await sender.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
