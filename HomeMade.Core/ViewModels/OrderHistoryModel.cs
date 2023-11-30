using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class OrderHistoryModel
    {
        public int SubOrderId { get; set; }
        public string MenuName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string QuantityType { get; set; }
        public string Chef { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int DeliveryType { get; set; }
        public string Instructions { get; set; }
        public int Status { get; set; }
        public string Apartment { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public bool IsRated { get; set; }
    }
}
