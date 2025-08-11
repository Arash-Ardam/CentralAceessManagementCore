using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppDbContext.DataModels
{
    public class ReadDataCenter
    {
        public string Name { get; set; } = string.Empty;


        private static ReadDataCenter _empty = new ReadDataCenter();

        public static ReadDataCenter Empty { get { return _empty; } }

    }
}
