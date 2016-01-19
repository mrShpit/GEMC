namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Collections.ObjectModel;

    public class ProxyList
    {
        public ProxyList(string name, string id)
        {
            this.listName = name;
            this.ProxyMailList = new ObservableCollection<ProxyLetter>();
            this.ProfileId = id;
        }

        public static ProxyList GetRecieved(Profile user)
        {
            ProxyList proxies = new ProxyList("Входящие", user.Id);

            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Inbox'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            sqlconnectionClass.CloseConnection();
            return proxies;
        }

        public static ProxyList GetSended(Profile user)
        {
            ProxyList proxies = new ProxyList("Отправленные", user.Id);

            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Outbox'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[5].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            sqlconnectionClass.CloseConnection();
            return proxies;
        }

        public static ProxyList GetTrash(Profile user)
        {
            ProxyList proxies = new ProxyList("Корзина", user.Id);

            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Trash'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            sqlconnectionClass.CloseConnection();

            return proxies;
        }

        public static ProxyList GetSpam(Profile user)
        {
            ProxyList proxies = new ProxyList("Спам", user.Id);

            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            SqlCommand cmd = sqlconnectionClass.DeployConnectionAndCommand();

            cmd.CommandText = "select * from Mail where ProfileId='" + user.Id + "' and Category='Spam'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProxyLetter proxy = new ProxyLetter(dr[0].ToString(), dr[2].ToString(), Convert.ToDateTime(dr[7]));
                    proxy.Interlocutor = dr[4].ToString();
                    proxies.ProxyMailList.Add(proxy);
                }
            }

            sqlconnectionClass.CloseConnection();
            return proxies;
        }

        public ObservableCollection<ProxyLetter> ProxyMailList
            {
                get { return this._list; }
                set { this._list = value; }
            }

        public int listItemsCount
        {
            get { return this._list.Count; }
        }

        public string ProfileId
        {
            get { return this._masterId; }
            set { this._masterId = value; }
        }

        public string listName
        {
            get { return this._listname; }
            set { this._listname = value; }
        }

        private string _listname;

        private string _masterId;

        private ObservableCollection<ProxyLetter> _list = new ObservableCollection<ProxyLetter>();
    }
}
