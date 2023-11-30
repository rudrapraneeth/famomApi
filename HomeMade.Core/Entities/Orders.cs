using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Orders : IAuditProperties
    {
        public Orders()
        {
            SubOrder = new HashSet<SubOrder>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal TotalCost { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<SubOrder> SubOrder { get; set; }
    }
}
