using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traveltoursProject
{
    public static class CreateConnection
    {
        public static string ConnectionString
        {
            get
            {
                string db = Path.Combine(Path.GetFullPath(@"..\..\"), "traveltoursDb.mdf");
                return $@"Data Source=(localdb)\mssqllocaldb;AttachDbFilename={db};Initial Catalog=traveltoursDb;Trusted_Connection=True";
            }
        }
    }
}
