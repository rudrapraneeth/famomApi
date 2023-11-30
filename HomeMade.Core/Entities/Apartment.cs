using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Apartment : IAuditProperties
    {
        public Apartment()
        {
            ApartmentPromotion = new HashSet<ApartmentPromotion>();
            UserApartment = new HashSet<UserApartment>();
        }

        public int ApartmentId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<ApartmentPromotion> ApartmentPromotion { get; set; }
        public virtual ICollection<UserApartment> UserApartment { get; set; }
    }
}
