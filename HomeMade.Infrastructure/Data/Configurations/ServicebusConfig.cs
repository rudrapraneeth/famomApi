using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Data.Configurations
{
    public class ServicebusConfig
    {
        public string ConnectionString { get; set; }
        public string NotificationQueue { get; set; }
        public string PostMenuQueue { get; set; }
    }
}
