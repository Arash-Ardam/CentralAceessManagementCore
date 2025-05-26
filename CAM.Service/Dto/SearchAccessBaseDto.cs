using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public record SearchAccessBaseDto
    {
        public string SourceDCName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination {  get; set; } = string.Empty;
        public int Port { get; set; } = 0;

        public DatabaseDirection Direction { get; set; } = DatabaseDirection.none;


        public bool HasSourceDCName() => SourceDCName != string.Empty;
        public bool HasSource() => Source != string.Empty;
        public bool HasDestination() => Destination != string.Empty;
        public bool HasPort() => Port != 0;
        public bool HasDirection() => Direction != DatabaseDirection.none;
    }
}
