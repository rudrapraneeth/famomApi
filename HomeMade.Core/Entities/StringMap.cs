using System;
using System.Collections.Generic;

namespace HomeMade.Core.Entities
{
    public partial class StringMap
    {
        public int StringMapId { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string Description { get; set; }
    }
}
