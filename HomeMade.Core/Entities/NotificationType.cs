using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class NotificationType : IAuditProperties
    {
        public NotificationType()
        {
            PushNotification = new HashSet<PushNotification>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; }
        public string NotificationTypeCode { get; set; }
        public int? UserTypeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<PushNotification> PushNotification { get; set; }
    }
}
