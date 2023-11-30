using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using HomeMade.Core.Interfaces;
using HomeMade.Core.Entities;
using Famom.Functions.Models;

namespace Famom.Functions
{
    public class BroadcastNotification
    {
        private readonly IExpoRepository _service;

        public BroadcastNotification(IExpoRepository service)
        {
            _service = service;
        }

        [FunctionName("BroadcastNotification")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] BoradcastNotificationRequest request)
        {
            try
            {
                await _service.BroadcastNotification(request.NotificationMessage, request.Title, request.ApartmentId, request.UserTypeCode);
                return new OkObjectResult(request);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }

            
        }
    }
}
