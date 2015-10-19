namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Net.NetworkInformation;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Web;

    public sealed class PostClient
    {
        public static readonly PostClient Instance = new PostClient();

        public PostClient()
        {
        }

        public static PostClient instance
        {
            get
            {
                return Instance;
            }
        }

        public void SendLetterSMTP(Profile sender, Letter letter)
        {
            // SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            // SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 25);
            // SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
            SmtpClient smtp = new SmtpClient("smtp." + sender.Server, sender.SmtpPort); // Сервер и порт
            smtp.Credentials = new System.Net.NetworkCredential(sender.Adress, sender.Password);  // Логин и пароль
            ////Формирование письма
            MailMessage message = new MailMessage();
            message.From = new MailAddress(sender.Adress);
            message.To.Add(new MailAddress(letter.To));
            message.Subject = letter.Subject;
            message.Body = letter.Body;
            smtp.EnableSsl = true;
            smtp.Send(message); // Отправка
        }

        public List<Letter> CheckForNewLetters(Profile user)
        {
            List<Letter> newMail = new List<Letter>();
            DateTime lastCheck = user.LastTimeChecked;
            TcpClient tcpclient = new TcpClient(); 
            tcpclient.Connect("pop." + user.Server, user.PopPort); 
            System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());
            sslstream.AuthenticateAsClient("pop." + user.Server); 
            System.IO.StreamWriter sw = new StreamWriter(sslstream); 
            System.IO.StreamReader reader = new StreamReader(sslstream);
           
            sw.WriteLine("USER " + user.Adress); 
            sw.Flush();
            sw.WriteLine("PASS " + user.Password);
            sw.Flush();
            sw.WriteLine("LIST");
            sw.Flush();            
            int counter = 0;
            string strTemp = string.Empty;
            string messageNum = string.Empty;
            while ((strTemp = reader.ReadLine()) != null)
            {
                if (counter == 3)
                {
                    messageNum = strTemp;
                    break;
                }

                counter++;
            }

            sw.WriteLine("Quit ");
            sw.Flush();
            messageNum = messageNum.Split(' ')[1];
            MessageBox.Show(messageNum);
            int mesNum = Convert.ToInt32(messageNum);
            tcpclient = new TcpClient();
            tcpclient.Connect("pop." + user.Server, user.PopPort);
            sslstream = new SslStream(tcpclient.GetStream());
            sslstream.AuthenticateAsClient("pop." + user.Server);
            sw = new StreamWriter(sslstream);
            reader = new StreamReader(sslstream);
            sw.WriteLine("USER " + user.Adress);
            sw.Flush();
            sw.WriteLine("PASS " + user.Password);
            sw.Flush();
            strTemp = string.Empty;
            string str = string.Empty;
            bool done = false;
            int cntr = 0;       
            while (!done)
            {
                str = string.Empty;

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
                
                TestWindow t = new TestWindow(str);
                t.Show();

                mesNum--;        

                // Нужен конструктор для сборки письма из поп-кода
                // if(letter.SendingTime<user.LastTimeChecked)
                // if(Cntr==1)
                    done = true;
               cntr++;
            }

            sw.WriteLine("Quit ");
            sw.Flush();
            user.LastTimeChecked = DateTime.Now;
            Profile.DB_Update(user);
            return newMail;
        }

        public List<Letter> DownloadMailHistory(Profile user)
        {
            List<Letter> mail = new List<Letter>();

            TcpClient tcpclient = new TcpClient(); 
            tcpclient.Connect("pop." + user.Server, user.PopPort); // gmail uses port number 995 for POP 
            System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream()); // This is Secure Stream // opened the connection between client and POP Server
            sslstream.AuthenticateAsClient("pop." + user.Server); // authenticate as client 
            System.IO.StreamWriter sw = new StreamWriter(sslstream); // Asssigned the writer to stream
            System.IO.StreamReader reader = new StreamReader(sslstream); // Assigned reader to stream
            sw.WriteLine("USER " + user.Adress); // refer POP rfc command, there very few around 6-9 command
            sw.Flush();
            sw.WriteLine("PASS " + user.Password);
            sw.Flush();
            sw.WriteLine("LIST");
            sw.Flush();
            string strTemp = string.Empty;
            string str = string.Empty;
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

            MessageBox.Show(str);
            sw.WriteLine("Quit ");
            sw.Flush();
            return mail;
        }
    }
}
