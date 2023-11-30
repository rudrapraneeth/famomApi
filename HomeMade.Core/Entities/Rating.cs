using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Rating : IAuditProperties
    {
        public int RatingId { get; set; }
        public int SubOrderId { get; set; }
        public short Rating1 { get; set; }
        public string Review { get; set; }
        public int ChefId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Chef Chef { get; set; }
        public virtual SubOrder SubOrder { get; set; }
    }
}
