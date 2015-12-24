namespace GEMC
{
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Windows;

    public class Letter : DefaultDBClass
    {
        private string _pid;

        public string ProfileId
        {
            get { return this._pid; }
            set { this._pid = value; }
        }

        private string _subject;

        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        private string _body;

        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }

        private string _from;

        public string From
        {
            get { return this._from; }
            set { this._from = value; }
        }

        private string _to;

        public string To
        {
            get { return this._to; }
            set { this._to = value; }
        }

        private string _category;

        public string Category
        {
            get { return this._category; }
            set { this._category = value; }
        }

        public string SubjectPreview
        {
            get
            {
                if (this._subject.Length < 14)
                {
                    return this._subject;
                }
                else
                {
                    return this._subject.Substring(0, 14) + "...";
                }
            }
        }

        private DateTime _sendingTime;

        public DateTime SendingTime
        {
            get { return this._sendingTime; }
            set { this._sendingTime = value; }
        }

        public Letter()
        {
        }

        public Letter(string profileid, string subject, string body, string from, string to, string category, DateTime time)
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
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
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
        }

        public static void ChangeLetterFolder(ProxyLetter letter, string folderName)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = "Update Mail SET Category = '" + folderName + "' where Id = '" + letter.Id + "'";
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public static void DeleteLetterFromDB(string letterId)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            cmd.CommandText = "delete from Mail where Id='" + letterId + "'";
            cmd.ExecuteNonQuery();
            _connection.Close();
            MessageBox.Show("Deleted");
        }
    }
}