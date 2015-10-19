namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class ProxyLetter
    {
        public ProxyLetter(string id, string subject)
        {
            this.Id = id;
            this.Subject = subject;
        }

        private Letter letter = new Letter();

        public string Id
        {
            get 
            {
                return this.letter.Id; 
            }

            set
            {
                this.letter.Id = value;
            }
        }

        public string Subject
        {
            get
            {
                return this.letter.Subject;
            }

            set
            {
                this.letter.Subject = value;
            }
        }

        private string interlocutor;

        public string Interlocutor
        {
            get
            {
                return this.interlocutor;
            }

            set
            {
                this.interlocutor = value;
            }
        }

        private DateTime sendtime;

        public DateTime SendTime
        {
            get
            {
                return this.sendtime;
            }

            set
            {
                this.sendtime = value;
            }
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
    }
}