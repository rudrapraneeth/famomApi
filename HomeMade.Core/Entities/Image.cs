using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class Image : IAuditProperties
    {
        public Image()
        {
            Chef = new HashSet<Chef>();
            PostImage = new HashSet<PostImage>();
        }

        public int ImageId { get; set; }
        public string Url { get; set; }
        public string Metadata { get; set; }
        public string FileName { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Chef> Chef { get; set; }
        public virtual ICollection<PostImage> PostImage { get; set; }
    }
}
