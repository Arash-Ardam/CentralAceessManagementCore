using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSqlDataAccess
{
    public class ReadDbContextConfigEntry
    {
        public bool isEnabled { get; set; }
        public dapper dapper { get; set; } = new dapper();
    }
    public class dapper
    {
        public bool isEnabled { get; set; }
        public short retryCount { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
    }

}
