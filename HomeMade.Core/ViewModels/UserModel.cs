using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class UserModel
    {
        public int ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int ApartmentId { get; set; }
        public string Block { get; set; }
        public string ApartmentNumber { get; set; }
        [JsonIgnore]
        public string CountryCode { get; set; } = "+91"; //Change to enum
        public bool IsVerified { get; set; }
        public string MobileNumberCc
        {
            get
            {
                return $"{CountryCode}{MobileNumber}";
            }
        }

        public string PushToken { get; set; }
        public int ChefId { get; set; }
        public string ExpoPushToken { get; set; }
    }
}
