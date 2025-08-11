using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppDbContext.DataModels
{
    public class ReadAccess
    {
        public int Id { get; set; }

        public string Source { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public string SourceAddress { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public string DestinationAddress { get; set; } = string.Empty;

        public int Port { get; set; } = 0;
        public DatabaseDirection Direction { get; set; } = DatabaseDirection.none;

        public string DataCenterName { get; set; } = string.Empty;

    }
}
