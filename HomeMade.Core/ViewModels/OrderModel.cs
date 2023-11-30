using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class OrderModel
    {
        public int SubOrderId { get; set; }
        public int DeliveryType { get; set; }
        public string Instructions { get; set; }
        public int PostId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int StatusChangeUserTypeId { get; set; }
        public int PromotionId { get; set; }
        public decimal TotalDiscountPrice { get; set; }
    }
}
