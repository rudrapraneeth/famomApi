using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class AvailabilityType : IAuditProperties
    {
        public AvailabilityType()
        {
            Post = new HashSet<Post>();
        }

        public int AvailabilityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
