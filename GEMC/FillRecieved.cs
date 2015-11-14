namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class FillRecieved : BuildListStrategy
    {
        public override void Fill(List<ProxyLetter> list, Profile user)
        {
            list.Clear();
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and AdressTo='" + user.Adress + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString());
                    proxy.Interlocutor = dr[4].ToString();
                    proxy.SendTime = Convert.ToDateTime(dr[7]);
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
