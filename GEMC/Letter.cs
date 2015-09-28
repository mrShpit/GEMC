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

        private string _name;
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
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

        public Letter()
        {

        }

        public static List<Letter> PullMailFromDB(Profile user)
        {

            List<Letter> mail = new List<Letter>();

            LetterBuilderStandart letBuilder = new LetterBuilderStandart();
            LetterBuildingDirector letDirector = new LetterBuildingDirector(letBuilder);


            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;
                AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    letDirector.ConstructFromDataReader(dr);
                    Letter letter = letBuilder.GetResult();

                    mail.Add(letter);
                }
            }

            _connection.Close();

            return mail;
        }
         

        public static void AddLetterToDB(Profile user, Letter letter)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\MailClientDataBase.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            StringBuilder sb = new StringBuilder();

            cmd.CommandText = "insert into Mail (Id, ProfileId, Name, Subject,Body, AdressFrom, AdressTo, Category)"
            + " values (@id, @pid, @name, @s, @b, @af, @at, @c)";

            letter.SetId();

            cmd.Parameters.AddWithValue("@id", letter.Id);
            cmd.Parameters.AddWithValue("@pid", user.Id);
            cmd.Parameters.AddWithValue("@name", letter.Name);
            cmd.Parameters.AddWithValue("@s", letter.Subject);
            cmd.Parameters.AddWithValue("@b", letter.Body);
            cmd.Parameters.AddWithValue("@af", letter.From);
            cmd.Parameters.AddWithValue("@at", letter.To);
            cmd.Parameters.AddWithValue("@c", letter.Category);

            cmd.ExecuteNonQuery();
            _connection.Close();

            MessageBox.Show("Added");

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
