using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public record AccessBaseDto
    {
        public string FromDCName { get; set; } = string.Empty;
        public string ToDCName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public int Port { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}
