using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public class AccessQueryDto
    {
        public string dcName { get; set; } = string.Empty;

        public string source { get; set; } = string.Empty;
        public string sourceName { get; set; } = string.Empty;
        public string sourceAddress {  get; set; } = string.Empty;

        public string destination { get; set; } = string.Empty;
        public string destinationName { get; set; } = string.Empty;
        public string destinationAddress { get; set; } = string.Empty;

        public int port { get; set; }
        public DatabaseDirection direction { get; set; }


    }
}
