namespace GEMC
{
    using System;
    using System.Windows;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Net.Mail;
    using System.Net.Security;
    using System.IO;

    public class InitiatePOP : ServerProtocol
    {
        public override SslStream Authenticate(Profile user)
        {
            DateTime lastCheck = user.LastTimeChecked;
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect("pop." + user.Server, user.PopPort);
            System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());
            sslstream.AuthenticateAsClient("pop." + user.Server);
            System.IO.StreamWriter sw = new StreamWriter(sslstream);

            if (user.Server == "gmail.com")
            {
                sw.WriteLine("USER recent:" + user.Adress); // Google требует особого входа
            }
            else
            {
                sw.WriteLine("USER " + user.Adress);
            }

            sw.Flush();
            sw.WriteLine("PASS " + user.Password);
            sw.Flush();

            return sslstream;
        }

        public override int SendRequestToServer(SslStream sslstream, string requestLine)
        {
            System.IO.StreamWriter sw = new StreamWriter(sslstream);
            sw.WriteLine("LIST");
            sw.Flush();
            int counter = 0;
            string strTemp = string.Empty;
            string messageNum = string.Empty;
            System.IO.StreamReader reader = new StreamReader(sslstream);

            while ((strTemp = reader.ReadLine()) != null)
            {
                if (counter == 3)
                {
                    messageNum = strTemp;
                    break;
                }

                counter++;
            }

            messageNum = messageNum.Split(' ')[1];
            int mesNum = Convert.ToInt32(messageNum);
            return mesNum;
        }

        public override object PullDataFromServer(SslStream sslstream, int mesNum)
        {
            bool done = false;
            int cntr = 0;
            string str = string.Empty;
            string strTemp = string.Empty;
            List<Letter> mail = new List<Letter>();

            while (!done)
            {
                StreamWriter sw = new StreamWriter(sslstream);
                StreamReader reader = new StreamReader(sslstream);

                sw.WriteLine("RETR " + mesNum.ToString());
                sw.Flush();
                while ((strTemp = reader.ReadLine()) != null)
                {
                    if (strTemp == ".")
                    {
                        break;
                    }

                    if (strTemp.IndexOf("-ERR") != -1)
                    {
                        break;
                    }

                    str += strTemp + "\n";
                }

                mesNum--;

                done = true;
                cntr++;
            }

            return str;
        }

        public override void LogOut(SslStream sslstream)
        {
            StreamWriter sw = new StreamWriter(sslstream);
            sw.WriteLine("Quit ");
            sw.Flush();
        }
    }
}
