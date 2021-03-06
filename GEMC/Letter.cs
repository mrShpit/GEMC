﻿namespace GEMC
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

        public string FromDisplay
        {
            get
            {
                return "От: " + this.From;
            }
        }

        public string ToDisplay
        {
            get
            {
                return "Кому: " + this.To;
            }
        }

        public string WhenDisplay
        {
            get
            {
                return "Время отправки: " + this.SendingTime;
            }
        }

        public string SubjectDisplay
        {
            get
            {
                return "Тема: " + this.Subject;
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
            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

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

            sqlconnectionClass.CloseConnection();
        }

        public static void ChangeLetterFolderInDB(ProxyLetter letter, string folderName)
        {
            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "Update Mail SET Category = '" + folderName + "' where Id = '" + letter.Id + "'";
            cmd.ExecuteNonQuery();

            sqlconnectionClass.CloseConnection();
        }

        public static void DeleteLetterFromDB(string letterId)
        {
            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "delete from Mail where Id='" + letterId + "'";
            cmd.ExecuteNonQuery();

            sqlconnectionClass.CloseConnection();
            MessageBox.Show("Deleted");
        }
    }
}