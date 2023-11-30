using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class ApartmentPromotion : IAuditProperties
    {
        public int ApartmentPromotionId { get; set; }
        public int PromotionId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
