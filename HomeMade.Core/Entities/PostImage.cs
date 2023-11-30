using System;
using System.Collections.Generic;
using HomeMade.Core.Interfaces;

namespace HomeMade.Core.Entities
{
    public partial class PostImage : IAuditProperties
    {
        public int PostImageId { get; set; }
        public int ImageId { get; set; }
        public int PostId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }

        public virtual Image Image { get; set; }
        public virtual Post Post { get; set; }
    }
}
