using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class OrderStatus : IAuditProperties
    {
        public OrderStatus()
        {
            Orders = new HashSet<Orders>();
            SubOrder = new HashSet<SubOrder>();
        }

        public int OrderStatusId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<SubOrder> SubOrder { get; set; }
    }
}
