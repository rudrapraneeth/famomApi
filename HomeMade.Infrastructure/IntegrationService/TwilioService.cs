using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace HomeMade.Infrastructure.IntegrationService
{
    public class TwilioService : ITwilioService
    {
        private readonly TwilioConfig _config;

        public TwilioService(IOptions<TwilioConfig> configuration)
        {
            _config = configuration.Value;
            TwilioClient.Init(_config.AccountSid, _config.AuthToken);
        }

        public async Task<TwilioResult> StartVerificationAsync(string phoneNumber, string channel)
        {
            try
            {
                var verificationResource = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: channel,
                    pathServiceSid: _config.VerificationSid
                );
                return new TwilioResult(verificationResource.Sid);
            }
            catch (TwilioException e)
            {
                return new TwilioResult(new List<string> { e.Message });
            }
        }

        public async Task<TwilioResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            try
            {
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: _config.VerificationSid
                );
                return verificationCheckResource.Status.Equals("approved") ?
                    new TwilioResult(verificationCheckResource.Sid) :
                    new TwilioResult(new List<string> { "Wrong code. Try again." });
            }
            catch (TwilioException e)
            {
                return new TwilioResult(new List<string> { e.Message });
            }
        }
    }
}
