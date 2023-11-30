using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class PostHistoryModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public String Description { get; set; }
        public int Orders { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? QuantityLeft { get; set; }
        public decimal Price { get; set; }
        public int QuantityType { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public int DeliveryTypeId { get; set; }
        public string Categories { get; set; }
        public int? MinimumOrder { get; set; }
        public int? NoticeHours { get; set; }
        public int AvailabilityTypeId { get; set; }
    }
}
