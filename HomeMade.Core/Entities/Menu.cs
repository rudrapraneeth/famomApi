using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Menu : IAuditProperties
    {
        public Menu()
        {
            Post = new HashSet<Post>();
        }

        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public int? ChefId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
