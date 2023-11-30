using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class SubOrderPromotion : IAuditProperties
    {
        public int SubOrderPromotionId { get; set; }
        public int SubOrderId { get; set; }
        public int PromotionId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual SubOrder SubOrder { get; set; }
    }
}
