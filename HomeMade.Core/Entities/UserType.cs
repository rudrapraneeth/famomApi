using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class UserType : IAuditProperties
    {
        public UserType()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public int UserTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
