using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int? Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int RatingsCount { get; set; }
        public int DeliveryType { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public int QuantityType { get; set; }
        public string BlockNumber { get; set; }
        public string FlatNumber { get; set; }
        public string Categories { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int? NoticeHours { get; set; }
        public int? MinimumOrder { get; set; }
        public string ApartmentNumber
        {
            get
            {
                return String.IsNullOrEmpty(BlockNumber) ? FlatNumber : $"{BlockNumber} - {FlatNumber}";
            }
        }

        public Discount Discount { get; set; }

    }

    public class Discount
    {
        public int PromotionId { get; set; }
        public int DiscountPercent { get; set; }
    }
    
}
