using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace OrderManagement.Util
{
    internal class DButil
    {
        static SqlConnection connection = null;
        public static SqlConnection GetConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            //connection.ConnectionString = Propertyutil.getpropertystring();
            return connection;
        }
    }
}
