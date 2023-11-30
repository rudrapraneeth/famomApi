using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Famom.Functions
{
    public static class PushEmail
    {
        [FunctionName("PushEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string name = data?.name;
                string email = data.email;
                string message = data.message;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("famomapp@gmail.com", "famomapp@123"),
                    EnableSsl = true,
                };

                smtpClient.Send("famomapp@gmail.com", "rudrapraneeth@famomapp.in", $"{name} : {email}", message);

                string responseMessage = "Thanks for your email. We'll get back to you as soon as we can.";

                return new OkObjectResult(responseMessage);
            }
            catch (System.Exception e)
            {
                return new OkObjectResult(e.Message);
            }
        }
    }
}
