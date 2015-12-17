namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FillSended : BuildListStrategy
    {
        public override void Fill(List<ProxyLetter> list, Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and AdressFrom='" + user.Adress + "'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[5].ToString();
                    list.Add(proxy);
                }
            }

            _connection.Close();
        }

        public override void Sort(List<ProxyLetter> list)
        {
            int y = 1;
            while (y == 0)
            {
                y = 1;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i].SendTime < list[i + 1].SendTime)
                    {
                        DateTime tmp = list[i].SendTime;
                        list[i].SendTime = list[i + 1].SendTime;
                        list[i + 1].SendTime = tmp;
                        y = 0;
                    }
                }
            }
        }
    }
}