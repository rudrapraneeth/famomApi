using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HomeMade.Api.Models
{
    public class MobileVerificationModel
    {
        public string MobileNumber { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public string CountryCode { get; set; } = "+91"; //Change to enum
        public bool IsValid { get; set; }
        public string VerificationReason { get; set; }
        [JsonIgnore]
        public string MobileNumberCc
        {
            get
            {
                return $"{CountryCode}{MobileNumber}";
            }
        }
    }
}
