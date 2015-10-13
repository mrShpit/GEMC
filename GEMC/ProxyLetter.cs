using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace GEMC
{
    public class ProxyLetter
    {

        public ProxyLetter(string id, string subject)
        {
            Id = id;
            Subject = subject;
        }


        private Letter letter = new Letter();

        public string Id
        {
            get 
            { 
                return letter.Id; 
            }
            set
            {
                letter.Id = value;
            }
        }

        public string Subject
        {
            get
            {
                return letter.Subject;
            }
            set
            {
                letter.Subject = value;
            }
        }

        private string interlocutor;
        public string Interlocutor
        {
            get
            {
                return interlocutor;
            }
            set
            {
                interlocutor = value;
            }
        }

        private DateTime sendtime;
        public DateTime SendTime
        {
            get
            {
                return sendtime;
            }
            set
            {
                sendtime = value;
            }
        }

        public Letter GetLetter()
        {
            Letter letter = new Letter();

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where Id='" + this.Id + "'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    letter = new Letter(dr);
                }
            }

            _connection.Close();

            return letter;
        }

        public static List<ProxyLetter> GetRecieved(Profile user)
        {
            List<ProxyLetter> proxies = new List<ProxyLetter>();

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");

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
                    proxies.Add(proxy);
                }
            }

            _connection.Close();


            return proxies;
        }

        public static List<ProxyLetter> GetSended(Profile user)
        {
            List<ProxyLetter> proxies = new List<ProxyLetter>();

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");

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
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString());
                    proxy.Interlocutor = dr[5].ToString();
                    proxies.Add(proxy);
                }
            }

            _connection.Close();
            return proxies;
        }


    }
}
