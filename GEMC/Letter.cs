using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace GEMC
{
    public class Letter : DefaultDBClass
    {

        private string _pid;
        public string ProfileId
        {
            get { return _pid; }
            set { _pid = value; }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        private string _to;
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private DateTime _sendingTime;
        public DateTime SendingTime
        {
            get { return _sendingTime; }
            set { _sendingTime = value; }
        }

        public Letter()
        {

        }

        public Letter(string profileid, string subject, string body, string from, string to,string category, DateTime time)
        {
            this.ProfileId = profileid;
            this.Subject = subject;
            this.Body = body;
            this.From = from;
            this.To = to;
            this.Category = category;
            this.SendingTime = time;
        }

        public Letter(SqlDataReader dr)
        {
            this.ProfileId = dr[1].ToString();
            this.Subject = dr[2].ToString();
            this.Body = dr[3].ToString();
            this.From = dr[4].ToString();
            this.To = dr[5].ToString();
            this.Category = dr[6].ToString();
            this.SendingTime = Convert.ToDateTime(dr[7]); 
        }



         

        public static void AddLetterToDB(Profile user, Letter letter)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=
                C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            StringBuilder sb = new StringBuilder();

            cmd.CommandText = "insert into Mail (Id, ProfileId, Subject,Body, AdressFrom, AdressTo, Category, Time)"
            + " values (@id, @pid, @s, @b, @af, @at, @c, @t)";

            letter.SetId();

            cmd.Parameters.AddWithValue("@id", letter.Id);
            cmd.Parameters.AddWithValue("@pid", user.Id);

            cmd.Parameters.AddWithValue("@s", letter.Subject);
            cmd.Parameters.AddWithValue("@b", letter.Body);
            cmd.Parameters.AddWithValue("@af", letter.From);
            cmd.Parameters.AddWithValue("@at", letter.To);
            cmd.Parameters.AddWithValue("@c", letter.Category);
            cmd.Parameters.AddWithValue("@t", letter.SendingTime);

            cmd.ExecuteNonQuery();
            _connection.Close();

            MessageBox.Show("Sended and added to db");

        }

        public static void DeleteLetterFromDB(Letter letter)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=
                        C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            StringBuilder sb = new StringBuilder();

            cmd.CommandText = "@delete from Animes where Id='" + letter.Id + "'";


            cmd.ExecuteNonQuery();
            _connection.Close();

            MessageBox.Show("Deleted");
        }

    }
}
