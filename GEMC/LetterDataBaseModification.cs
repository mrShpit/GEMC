namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class LetterDataBaseModification : LetterDecorator
    {
        public LetterDataBaseModification(Letter letterItem) : base(letterItem)
        {
        }

        public void AddLetterToDB(Profile user)
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            StringBuilder sb = new StringBuilder();
            cmd.CommandText = "insert into Mail (Id, ProfileId, Subject,Body, AdressFrom, AdressTo, Category, Time)"
            + " values (@id, @pid, @s, @b, @af, @at, @c, @t)";
            letterItem.SetId();
            cmd.Parameters.AddWithValue("@id", letterItem.Id);
            cmd.Parameters.AddWithValue("@pid", user.Id);
            cmd.Parameters.AddWithValue("@s", letterItem.Subject);
            cmd.Parameters.AddWithValue("@b", letterItem.Body);
            cmd.Parameters.AddWithValue("@af", letterItem.From);
            cmd.Parameters.AddWithValue("@at", letterItem.To);
            cmd.Parameters.AddWithValue("@c", letterItem.Category);
            cmd.Parameters.AddWithValue("@t", letterItem.SendingTime);
            cmd.ExecuteNonQuery();
            _connection.Close();
            MessageBox.Show("Sended and added to db");
        }

        public void DeleteLetterFromDB()
        {
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            _connection.Open();
            StringBuilder sb = new StringBuilder();
            cmd.CommandText = "@delete from Animes where Id='" + letterItem.Id + "'";
            cmd.ExecuteNonQuery();
            _connection.Close();
            MessageBox.Show("Deleted");
        }
    }
}
