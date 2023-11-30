using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class SubOrder : IAuditProperties
    {
        public SubOrder()
        {
            Rating = new HashSet<Rating>();
            SubOrderPromotion = new HashSet<SubOrderPromotion>();
        }

        public int SubOrderId { get; set; }
        public int OrderId { get; set; }
        public int PostId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalDiscountPrice { get; set; }
        public int StatusId { get; set; }
        public int DeliveryTypeId { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Instructions { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Post Post { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
        public virtual ICollection<SubOrderPromotion> SubOrderPromotion { get; set; }
    }
}
