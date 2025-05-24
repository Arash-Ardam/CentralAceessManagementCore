using Domain.DataModels;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataModels
{
    public class Access
    {   
        public int Id { get; set; }    
        public string Source { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;

        public int Port { get; set; }

        public DatabaseDirection Direction { get; set; }

        private Access() { }
        private Access(Create newAccess)
        {
            Source = newAccess.SourceName;
            Destination = newAccess.DestinationName;
            Port = newAccess.Port;
            Direction = newAccess.Direction;
        }

        private static Access baseAccess = new Access();
        public static Access Empty { get { return baseAccess; } }

        public class Create
        {
            public string SourceName { get; set; } = string.Empty;
            public string DestinationName { get; set; } = string.Empty;
            public int Port { get; private set; }
            public DatabaseDirection Direction { get; private set; }

            public Create AddSource(string sourceName)
            {
                SourceName = sourceName;
                return this;
            }

            public Create AddPort(int port)
            {
                Port = port;
                return this;
            }

            public Create AddDestination(string destinationName)
            {
                DestinationName = destinationName;
                return this;
            }

            public Create SetDirection(DatabaseDirection direction)
            {
                Direction = direction;
                return this;
            }

            public Access Build()
            {
                return new Access(this);
            }
        }
       

    }
}
