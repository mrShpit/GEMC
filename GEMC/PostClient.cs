using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;

namespace GEMC
{
    public sealed class PostClient
        
    {

        static readonly PostClient _instance = new PostClient();

        PostClient()
        {

        }

        public static PostClient Instance
        {
            get
            {
                return _instance;
            }
        }

        public void SendLetterSMTP(Profile sender, Letter letter)
        {

            //SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            //SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 25);
            //SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);


            SmtpClient Smtp = new SmtpClient("smtp." + sender.Server, sender.SmtpPort); //Сервер и порт
            Smtp.Credentials = new System.Net.NetworkCredential(sender.Adress, sender.Password);  //Логин и пароль
            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(sender.Adress);
            Message.To.Add(new MailAddress(letter.To));
            Message.Subject = letter.Subject;
            Message.Body = letter.Body;
            Smtp.EnableSsl = true;
            //Отправка
            Smtp.Send(Message);
        }


        public List<Letter> CheckForNewLetters(Profile user)
        {
            List<Letter> NewMail = new List<Letter>();
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

            

            int Counter = 0;
            string strTemp = string.Empty;
            string MessageNum = "";
            while ((strTemp = reader.ReadLine()) != null)
            {
                if (Counter == 3)
                {
                    MessageNum = strTemp;
                    break;
                }
                Counter++;
            }
            sw.WriteLine("Quit ");
            sw.Flush();
            MessageNum = MessageNum.Split(' ')[1];
            MessageBox.Show(MessageNum);
            int MesNum = Convert.ToInt32(MessageNum);



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
            bool Done = false;

            int Cntr = 0;
           
            while(!Done)
            {
                str = "";

                sw.WriteLine("RETR " + MesNum.ToString());
                //sw.WriteLine("RETR 465");
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

                MesNum--;
                


                // Нужен конструктор для сборки письма из поп-кода

                //if(letter.SendingTime<user.LastTimeChecked)
                //if(Cntr==1)
                    Done = true;

               Cntr++;

            }
            sw.WriteLine("Quit ");
            sw.Flush();
            



            user.LastTimeChecked = DateTime.Now;
            Profile.DB_Update(user);
            return NewMail;
        }


        public List<Letter> DownloadMailHistory(Profile user)
        {

            List<Letter> mail = new List<Letter>();

            TcpClient tcpclient = new TcpClient(); // create an instance of TcpClient
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
            string str = "";

 
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
