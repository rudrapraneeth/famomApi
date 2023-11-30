using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.Interfaces
{
    public interface IAuditProperties
    {
        DateTime? CreateDateTime { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdateDateTime { get; set; }
        string UpdateBy { get; set; }
    }
}
