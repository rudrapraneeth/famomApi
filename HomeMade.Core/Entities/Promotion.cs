using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Promotion : IAuditProperties
    {
        public Promotion()
        {
            ApartmentPromotion = new HashSet<ApartmentPromotion>();
            SubOrderPromotion = new HashSet<SubOrderPromotion>();
        }

        public int PromotionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PromoCode { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int DiscountPercent { get; set; }
        public string PromoType { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<ApartmentPromotion> ApartmentPromotion { get; set; }
        public virtual ICollection<SubOrderPromotion> SubOrderPromotion { get; set; }
    }
}
