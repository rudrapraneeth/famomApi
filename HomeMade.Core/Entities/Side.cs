using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Side : IAuditProperties
    {
        public int SideId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual PostSide PostSide { get; set; }
    }
}
