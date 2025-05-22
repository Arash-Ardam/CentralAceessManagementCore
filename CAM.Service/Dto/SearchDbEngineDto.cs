using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public class SearchDbEngineDto
    {
        public string dcName { get; set; } = string.Empty;
        public string dbEngineName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public bool withDatabases { get; set; } = false;
    }
}
