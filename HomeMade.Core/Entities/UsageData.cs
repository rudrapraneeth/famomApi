using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class UsageData : IAuditProperties
    {
        public int UsageDataId { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime LastActionDate { get; set; }
        public string ActionName { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
