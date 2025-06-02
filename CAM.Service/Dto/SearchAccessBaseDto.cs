using Domain.DataModels;
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
        public DataCenter TargetDataCenter { get; set; } = DataCenter.Empty;
        public string SourceDCName { get; set; } = string.Empty;
        public string DestinationDCName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination {  get; set; } = string.Empty;
        public List<string> SourceDbEs {  get; set; } = new List<string>();
        public List<string> DestinationDbEs { get; set; } = new List<string>();

        public int Port { get; set; } = 0;

        public DatabaseDirection Direction { get; set; } = DatabaseDirection.none;


        private static readonly SearchAccessBaseDto _empty = new SearchAccessBaseDto();

        public static SearchAccessBaseDto Empty { get { return _empty; } } 

        public bool HasSourceDCName() => SourceDCName != string.Empty;
        public bool HasDestinationDCName() => DestinationDCName != string.Empty;
        public bool HasSource() => Source != string.Empty;
        public bool HasDestination() => Destination != string.Empty;
        public bool HasPort() => Port != 0;
        public bool HasDirection() => Direction != DatabaseDirection.none;
    }
}
