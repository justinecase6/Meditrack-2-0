using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditrack_2._0
{
    class DBConnection
    {
        public string MyConnection()
        {
            string con = @"Data Source=MIRAKOL;Initial Catalog=POS_DB;Integrated Security=True";
            return con;
        }
    }
}
