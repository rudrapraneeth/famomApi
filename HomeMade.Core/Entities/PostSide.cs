using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class PostSide : IAuditProperties
    {
        public int SideId { get; set; }
        public int PostId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Post Post { get; set; }
        public virtual Side Side { get; set; }
    }
}
