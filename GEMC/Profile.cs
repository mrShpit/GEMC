namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Collections.ObjectModel;

    public class Profile : DefaultDBClass
    {
        public Profile()
        {
        }

        public Profile(string name, string adr, string pas, string server)
        {
            this.Name = name;
            this.Adress = adr;
            this.Password = pas;
            this.Server = server;
            this.ProxyMailFolders = new List<ProxyList>();
        }

        public List<ProxyList> ProxyMailFolders
        {
            get; set;
        }

        private string _name;

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _adress;

        public string Adress
        {
            get { return this._adress; }
            set { this._adress = value; }
        }

        private string _pass;

        public string Password
        {
            get { return this._pass; }
            set { this._pass = value; }
        }

        private DateTime _lastTimeChecked;

        public DateTime LastTimeChecked
        {
            get { return this._lastTimeChecked; }
            set { this._lastTimeChecked = value; }
        }

        private string _server;

        public string Server
        {
            get { return this._server; }
            set { this._server = value; }
        }

        private int _smtpport;

        public int SmtpPort
        {
            get { return this._smtpport; }
            set { this._smtpport = value; }
        }

        private int _popport;

        public int PopPort
        {
            get { return this._popport; }
            set { this._popport = value; }
        }

        #region -- DBmethods --

        public static void DB_Add(Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
                                                                C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = "insert into Profiles (Id, Name, Adress,Password, LastTimeChecked, Server, SmtpPort, PopPort)"
            + " values (@id, @name, @adr, @pas, @ltc, @s, @smtp, @pop)";
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
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Profile> profileList = new List<Profile>();
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

                    profileList.Add(user);
                }
            }

            _connection.Close();

            return profileList;
        }

        public static void DB_Update(Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
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
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = @"delete from Profiles where Id='" + user.Id + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"delete from Mail where ProfileId='" + user.Id + "'";
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public static Profile DB_GetByID(string id)
        {
            Profile prof = new Profile();
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Profile> profileList = new List<Profile>();
            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Profiles where Id='" + id + "'";

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

                    prof = user;
                }
            }

            return prof;
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }
    }
}
