using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace GEMC
{
    public class Profile : DefaultDBClass
    {

        public Profile()
        {

        }

        public Profile(string name, string adr, string pas, string server)
        {
            Name = name;
            Adress = adr;
            Password = pas;
            Server = server;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        private string _pass;
        public string Password
        {
            get { return _pass; }
            set { _pass  = value; }
        }

        private DateTime _lastTimeChecked;
        public DateTime LastTimeChecked
        {
            get { return _lastTimeChecked; }
            set { _lastTimeChecked = value; }
        }

        private string _server;
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private int _smtpport;
        public int SmtpPort
        {
            get { return _smtpport; }
            set { _smtpport = value; }
        }

        private int _popport;
        public int PopPort
        {
            get { return _popport; }
            set { _popport = value; }
        }

        #region -- DBmethods --

        public static void DB_Add(Profile user)
        {

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText="insert into Profiles (Id, Name, Adress,Password, LastTimeChecked, Server, SmtpPort, PopPort)"
            +" values (@id, @name, @adr, @pas, @ltc, @s, @smtp, @pop)";

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@adr", user.Adress);
            cmd.Parameters.AddWithValue("@pas", user.Password);
            cmd.Parameters.AddWithValue("@ltc", user.LastTimeChecked);
            cmd.Parameters.AddWithValue("@s", user.Server);
            cmd.Parameters.AddWithValue("@smtp", user.SmtpPort);
            cmd.Parameters.AddWithValue("@pop", user.PopPort);


            cmd.ExecuteNonQuery();
            _connection.Close();
        }


        public static List<Profile> DB_Load()
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            List<Profile> ProfileList = new List<Profile>();
            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Profiles";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    Profile user = new Profile();

                    user.Id = dr[0].ToString();
                    user.Name = dr[1].ToString();
                    user.Adress = dr[2].ToString();
                    user.Password = dr[3].ToString();
                    user.LastTimeChecked = Convert.ToDateTime(dr[4]);
                    user.Server = dr[5].ToString();
                    user.SmtpPort = Convert.ToInt32(dr[6]);
                    user.PopPort = Convert.ToInt32(dr[7]);

                    ProfileList.Add(user);
                }
            }

            _connection.Close();

            return ProfileList;
        }

        public static void DB_Update(Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = @"Update Profiles SET LastTimeChecked = (@UE) where Id='" + user.Id + "'";
            cmd.Parameters.AddWithValue("@UE", user.LastTimeChecked);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public static void DB_Delete(Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = @"delete from Profiles where Id='" + user.Id + "'";
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

    }
}
