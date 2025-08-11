using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppDbContext.Configs
{
    public class ReadDbOptions
    {
        public bool isEnabled { get; set; }
        public sqlServer sqlServer { get; set; } = new sqlServer();
    }

    public class sqlServer
    {
        public bool isEnabled { get; set; }
        public short retryCount { get; set; }
        public string AdminConnectionString { get; set; } = string.Empty;
        public string TenantConnectionString { get; set; } = string.Empty;
    }
}
