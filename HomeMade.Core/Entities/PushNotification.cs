using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class PushNotification : IAuditProperties
    {
        public int PushNotificationId { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public int NotificationTypeId { get; set; }
        public int SequenceNumber { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceValue { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
