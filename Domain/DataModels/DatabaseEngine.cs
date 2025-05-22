using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataModels
{
    public class DatabaseEngine
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<Database> Databases { get; set; } = new List<Database>();

        private DatabaseEngine()
        {

        }

        //Null Pattern
        private static readonly DatabaseEngine _empty = new DatabaseEngine();
        public static DatabaseEngine Empty { get { return _empty; } }

        public static DatabaseEngine CreateByNameAndAddress(string name, string Address)
        {
            if (null != name && null != Address)
            {
                return new DatabaseEngine() { Name = name, Address = Address };

            }
            else
            {
                throw new ArgumentNullException();
            }
        }


        public void AddDatabase(Database database)
        {
            if (null != database)
            {
                Databases.Add(database);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

    }
}
