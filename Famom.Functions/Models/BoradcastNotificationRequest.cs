using System;
using System.Collections.Generic;
using System.Text;

namespace Famom.Functions.Models
{
    public class BoradcastNotificationRequest
    {
        public string NotificationMessage { get; set; }
        public string Title { get; set; }
        public int ApartmentId { get; set; }
        public string UserTypeCode { get; set; }
    }
}
