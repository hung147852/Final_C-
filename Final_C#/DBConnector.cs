using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Final_C
{
        public class DBConnector
        {

            public string Database { get; set; }
            public string Server { get; set; }
            public string User { get; set; }
            public string Password { get; set; }

            public DBConnector()
            {
                Server = "10.36.0.36";
                Database = "toannv_final";
                User = "toannv";
                Password = "@Automation1";
            }

            public DBConnector(string database, string server, string user, string password)
            {
                Database = database;
                Server = server;
                User = user;
                Password = password;
            }

            public SqlConnection GetConnection()
            {
                string connStr = BuildConnectionString();
                return new SqlConnection(connStr);
            }

            private string BuildConnectionString()
            {
                return String.Format("Data Source={0},1433;Initial Catalog={1};User Id={2};Password={3}",
                    Server, Database, User, Password);
            }
        }
}

