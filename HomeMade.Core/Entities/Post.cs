using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Post : IAuditProperties
    {
        public Post()
        {
            PostImage = new HashSet<PostImage>();
            PostSide = new HashSet<PostSide>();
            SubOrder = new HashSet<SubOrder>();
        }

        public int PostId { get; set; }
        public int ChefId { get; set; }
        public int MenuId { get; set; }
        public int? Quantity { get; set; }
        public int QuantityTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public int? NoticeHours { get; set; }
        public int? MinimumOrder { get; set; }
        public DateTime? InactiveDateTime { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual AvailabilityType AvailabilityType { get; set; }
        public virtual Chef Chef { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual QuantityType QuantityType { get; set; }
        public virtual ICollection<PostImage> PostImage { get; set; }
        public virtual ICollection<PostSide> PostSide { get; set; }
        public virtual ICollection<SubOrder> SubOrder { get; set; }
    }
}
