using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Dto
{
    public class SearchDCDto
    {
        public string DCName { get; set; } = string.Empty;
        public string DCAccessSourceName { get; set; } = string.Empty ; 
        public string DCAccessSourceAddress { get; set; } = string.Empty;
        public string DCAccessDestinationName { get; set; } = string.Empty;
        public string DCAccessDestinationAddress { get; set; } = string.Empty;
        public string DBEngineName { get; set; } = string.Empty;
        public string DBEngineAddress { get; set; } = string.Empty;


        private SearchDCDto(Create create)
        {
            DCName = create.DCName;
            DCAccessSourceName = create.DCAccessSourceName;
            DCAccessSourceAddress = create.DCAccessSourceAddress;
            DCAccessDestinationName = create.DCAccessDestinationName;
            DCAccessDestinationAddress = create.DCAccessDestinationAddress;
            DBEngineName = create.DBEngineName;
            DBEngineAddress = create.DBEngineAddress;
        }

        public bool HasDCName() => DCName != string.Empty;
        public bool HasAccessSourceName() => DCAccessSourceName != string.Empty;
        public bool HasAccessSourceAddress() => DCAccessSourceAddress != string.Empty;
        public bool HasAccessDestinationName() => DCAccessDestinationName != string.Empty;
        public bool HasAccessDestinationAddress() => DCAccessDestinationAddress != string.Empty;
        public bool HasDbEngineName() => DBEngineName != string.Empty;
        public bool HasDbEngineAddess() => DBEngineAddress != string.Empty;


        public class Create()
        {
            public string DCName { get; set; } = string.Empty;
            public string DCAccessSourceName { get; set; } = string.Empty;
            public string DCAccessSourceAddress { get; set; } = string.Empty;
            public string DCAccessDestinationName { get; set; } = string.Empty;
            public string DCAccessDestinationAddress { get; set; } = string.Empty;
            public string DBEngineName { get; set; } = string.Empty;
            public string DBEngineAddress { get; set; } = string.Empty;


            public Create AddDcName(string dcName)
            {
                DCName = dcName;
                return this;
            }

            public Create AddAccessSourceName(string accessSourceName)
            {
                DCAccessSourceName = accessSourceName;
                return this;
            }

            public Create AddAccessSourceAddress(string accessSourceAddress)
            {
                DCAccessSourceAddress = accessSourceAddress;
                return this;
            }

            public Create AddAccessDestinationName(string accessDestinationName)
            {
                DCAccessDestinationName = accessDestinationName;
                return this;
            }

            public Create AddAccessDestinationAddress(string accessDestinationAddress)
            {
                DCAccessDestinationAddress = accessDestinationAddress;
                return this;
            }

            public Create AddDbEngineName(string dbEngineName)
            {
                DBEngineName = dbEngineName;
                return this;
            }

            public Create AddDbEngineAddress(string dbEngineAddress)
            {
                DBEngineAddress = dbEngineAddress;
                return this;
            }

            public SearchDCDto Build()
            {
                return new SearchDCDto(this);
            }

        }

    }
}
