namespace GEMC
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class ProxyLetter : INotifyPropertyChanged
    {
        public static List<ProxyLetter> CheckedItems = new List<ProxyLetter>();

        public static void UncheckAll()
        {
            while (CheckedItems.Count != 0)
            {
                CheckedItems[0].IsChecked = false;
            }
        }

        public ProxyLetter(string id, string subject, DateTime sendtime)
        {
            this.Id = id;
            this.Subject = subject;
            this.SendTime = sendtime;
        }

        public ProxyLetter(string id, string subject, DateTime sendtime, string interlocutor)
        {
            this.Id = id;
            this.Subject = subject;
            this.SendTime = sendtime;
            this.Interlocutor = interlocutor;
        }

        private bool isChecked = false;

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
                this.NotifyPropertyChanged("isChecked");
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "select * from Mail where Id='" + this.Id + "'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    letter = new Letter(dr);
                }
            }

            sqlconnectionClass.CloseConnection();

            return letter;
        }

        private Letter letter = new Letter();
    }
}