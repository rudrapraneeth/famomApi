using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class ApplicationUser : IAuditProperties
    {
        public ApplicationUser()
        {
            UsageData = new HashSet<UsageData>();
            UserApartment = new HashSet<UserApartment>();
        }

        public int ApplicationUserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string ExpoPushToken { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual Chef Chef { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<UsageData> UsageData { get; set; }
        public virtual ICollection<UserApartment> UserApartment { get; set; }
    }
}
