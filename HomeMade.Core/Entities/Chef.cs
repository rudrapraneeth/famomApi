using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Chef : IAuditProperties
    {
        public Chef()
        {
            Post = new HashSet<Post>();
            Rating = new HashSet<Rating>();
        }

        public int ChefId { get; set; }
        public int ApplicationUserId { get; set; }
        public int? ProfileImageId { get; set; }
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Image ProfileImage { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
