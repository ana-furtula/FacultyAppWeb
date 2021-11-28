using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.MySQL
{
    public class DbBroker
    {
        private static MySqlConnection Connection { get; set; }

        public static MySqlConnection GetConnection()
        {
            if (Connection == null)
            {
                string server = "localhost";
                string database = "facultyapp";
                string uid = "root";
                string password = "root";
                string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                Connection = new MySqlConnection(connectionString);
            }
            else{
                try
                {
                    Connection.Close();
                } catch(Exception) { }
            }

            return Connection;
        }
    }
}
