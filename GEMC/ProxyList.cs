namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProxyList
    {
        public ProxyList(string name, string id)
        {
            this.listName = name;
            this.ProxyMailList = new List<ProxyLetter>();
            this.masterId = id;
        }

        public static ProxyList GetRecieved(Profile user)
        {
            ProxyList proxies = new ProxyList("Входящие", user.Id);

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Inbox'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            _connection.Close();
            return proxies;
        }

        public static ProxyList GetSended(Profile user)
        {
            ProxyList proxies = new ProxyList("Отправленные", user.Id);

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Outbox'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[5].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            _connection.Close();
            return proxies;
        }

        public static ProxyList GetTrash(Profile user)
        {
            ProxyList proxies = new ProxyList("Корзина", user.Id);

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Trash'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            _connection.Close();
            return proxies;
        }

        public static ProxyList GetSpam(Profile user)
        {
            ProxyList proxies = new ProxyList("Спам", user.Id);

            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gleb\Desktop\GEMC\GEMC\EMCdataBase.mdf;Integrated Security=True;");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.Connection = _connection;
            _connection.Open();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Spam'";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            _connection.Close();
            return proxies;
        }

        public List<ProxyLetter> ProxyMailList
            {
                get { return this._list; }
                set { this._list = value; }
            }

        public int listItemsCount
        {
            get { return this._list.Count; }
            set { this.listItemsCount = value; }
        }

        public string masterId
        {
            get { return this._masterId; }
            set { this._masterId = value; }
        }

        public string listName
        {
            get { return this._listname; }
            set { this._listname = value; }
        }
        
        // Нижняя часть не пашет. Я починю ее когда овладею черной магией.
        // private BuildListStrategy _buildstrategy;
        public void SetFillStrategy(BuildListStrategy buildstrategy)
        {
            // this._buildstrategy = buildstrategy;
        }

        public void Fill(Profile user)
        {
            // this._buildstrategy.Fill(this._list, user);
        }

        private string _listname;

        private string _masterId;

        private List<ProxyLetter> _list = new List<ProxyLetter>();
    }
}
