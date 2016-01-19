namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SqlClient;

    public class LocalSQLConnection
    {
        private static string connectionLine;

        public string DetectLocalDB()
        {
            //Detecting
            connectionLine = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\GEMC2\GEMC\EMCdataBase.mdf;Integrated Security=True;";
            return connectionLine;
        }

        public SqlCommand DeployConnectionAndCommand()
        {
            SqlConnection _connection = new SqlConnection(connectionLine);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            return cmd;
        }

        public void CloseConnection()
        {
            SqlConnection _connection = new SqlConnection(connectionLine);
            _connection.Close();
        }
    }
}
