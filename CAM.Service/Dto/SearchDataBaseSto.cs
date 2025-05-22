using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public record SearchDataBaseSto
    {
        public string DataCenterName { get; set; } = string.Empty;
        public string DataBaseEngineName { get; set;} = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;

    }
}
