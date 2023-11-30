using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class UserApartment : IAuditProperties
    {
        public int UserApartmentId { get; set; }
        public int ApartmentId { get; set; }
        public int ApplicationUserId { get; set; }
        public string Block { get; set; }
        public string ApartmentNumber { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
