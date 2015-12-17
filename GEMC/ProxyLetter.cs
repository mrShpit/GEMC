namespace GEMC
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class ProxyLetter
    {
        public static List<ProxyLetter> CheckedItems = new List<ProxyLetter>();

        public static void UncheckAll()
        {
            for (int i = 0; i < CheckedItems.Count; i++)
            {
                CheckedItems[i].IsChecked = false;
            }
        }

        public ProxyLetter(string id, string subject, DateTime sendtime)
        {
            this.Id = id;
            this.Subject = subject;
            this.SendTime = sendtime;
        }

        private bool isChecked;

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
                if (this.isChecked == true)
                {
                    ProxyLetter.CheckedItems.Add(this);
                }
                else
                {
                    ProxyLetter.CheckedItems.Remove(this);
                }
            }
        }

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

        private string subject;

        public string Subject
        {
            get
            {
                return this.subject;
            }

            set
            {
                this.subject = value;
            }
        }

        public string SubjectPreview
        {
            get
            {
                if (this.subject.Length < 20)
                {
                    return this.subject;
                }
                else
                {
                    return this.subject.Substring(0, 20) + "...";
                }
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

        public Letter GetLetter()
        {
            Letter letter = new Letter();

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

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

        private Letter letter = new Letter();
    }
}