using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos
{
    public class SqlServer
    {
        private static readonly string connectionString =
          "Server=DESKTOP-42RL6N1;Database=Gestao_de_Alunos;User Id=sa;Password=135113rr;";


        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}