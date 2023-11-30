using HomeMade.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class PostMenuQueueMessage
    {
        public int ApartmentId { get; set; }
        public int AvailabilityTypeId { get; set; }
        public int Quantity { get; set; }
        public string QuantityType  { get; set; }
        public string MenuTitle { get; set; }
        public string ChefName { get; set; }
        public int PostId { get; set; }
    }
}
