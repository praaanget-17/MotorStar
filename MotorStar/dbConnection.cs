using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorStar
{

    public class dbConnection
    {
        static string conString = "Server=localhost;Database=motorstar;Uid=root;Pwd=;";
        MySqlConnection con = new MySqlConnection(conString);

        public MySqlConnection GetConnection()
        {
            return con;
        }
    }
}
