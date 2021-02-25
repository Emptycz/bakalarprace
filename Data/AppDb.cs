using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarPrace.Data
{
    public class AppDb : IDisposable
    {
        public readonly MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("host=mysql;port=3306;user id=root;password=mysql;database=bachalor;");
            //Connection = new MySqlConnection("host=127.0.0.1;port=3306;user id=root;password=mysql;database=bachalor;");
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
