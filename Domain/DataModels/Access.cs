using Domain.DataModels;
using Domain.Enums;
using Newtonsoft.Json;
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
            Source = newAccess.Source;
            Destination = newAccess.Destination;
            Port = newAccess.Port;
            Direction = newAccess.Direction;
        }

        private static Access baseAccess = new Access();
        public static Access Empty { get { return baseAccess; } }

        public class Create
        {
            public string Source { get; set; } = string.Empty;
            public string Destination { get; set; } = string.Empty;
            public int Port { get; private set; }
            public DatabaseDirection Direction { get; private set; }

            public Create AddSource(DatabaseEngine source)
            {
                if(source != DatabaseEngine.Empty)
                    Source = JsonConvert.SerializeObject(source);

                return this;
            }

            public Create AddPort(int port)
            {
                Port = port;
                return this;
            }

            public Create AddDestination(DatabaseEngine destination)
            {
                if (destination != DatabaseEngine.Empty)
                    Destination = JsonConvert.SerializeObject(destination); 
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
