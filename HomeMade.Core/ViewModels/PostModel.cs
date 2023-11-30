using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class PostModel
    {
        public int ApplicationUserId { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int QuantityTypeId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public decimal Price { get; set; }
        public string FromDateTime { get; set; }
        public string ToDateTime { get; set; }
        public int ChefId { get; set; }
        public int DeliveryTypeId { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public IFormFile Image { get; set; }
        public string Categories { get; set; }
        public int? MinimumOrder { get; set; }
        public int? NoticeHours { get; set; }

    }
}
