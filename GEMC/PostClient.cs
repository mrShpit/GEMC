namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Windows;
    using System.Text;

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

        public void CheckLetterByIMAP(Profile user, int letterNum)
        {
            Letter newLetter = new Letter();
            ImapConsole console = new ImapConsole();
            console.SetSSLConnection(user);

            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();
            this.ImapRequest("$ SELECT INBOX\r\n");

            console.SendCommand(new ImapGetHeaderOfLetter(letterNum));
            List<string> letterHeader = (List<string>)console.ExecuteCommand();

            foreach (string line in letterHeader)
            {
                if (line.Contains("From: "))
                {
                    MessageBox.Show(line.Substring(6));
                }

                if (line.Contains("Date: "))
                {
                    MessageBox.Show(line.Substring(6));
                }

                if (line.Contains("Subject: "))
                {
                    MessageBox.Show(line.Substring(9));
                }
            }

            console.SetSSLConnection(user);
            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();
            this.ImapRequest("$ SELECT INBOX\r\n");

            console.SendCommand(new ImapGetTextOfLetter(letterNum));
            newLetter.Body = (string)console.ExecuteCommand();
        }

        public string ImapRequest(string commandText)
        {
            byte[] dummy;
            byte[] buffer;
            int bytes = -1;
            string str = string.Empty;

            if (commandText != string.Empty)
            {
                if (ImapConsole._tcpc.Connected)
                {
                    dummy = Encoding.ASCII.GetBytes(commandText);
                    ImapConsole._ssl.Write(dummy, 0, dummy.Length);
                }
            }

            ImapConsole._ssl.Flush();
            buffer = new byte[2048];
            bytes = ImapConsole._ssl.Read(buffer, 0, 2048);
            str = Encoding.ASCII.GetString(buffer);
            ImapConsole._ssl.ReadTimeout = 1000;
            return str.Replace("\0", string.Empty);
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
            smtp.Send(message);
        }

        public int GetTotalLetterNum(Profile user)
        {
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect("pop." + user.Server, user.PopPort);
            System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());
            sslstream.AuthenticateAsClient("pop." + user.Server);
            System.IO.StreamWriter sw = new StreamWriter(sslstream);
            System.IO.StreamReader reader = new StreamReader(sslstream);

            if (user.Server == "gmail.com")
            {
                sw.WriteLine("USER recent:" + user.Adress);
            }
            else
            {
                sw.WriteLine("USER " + user.Adress);
            }

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
            return Convert.ToInt32(messageNum);
        }

        public List<Letter> CheckForNewLettersPOP(Profile user)
        {
            List<Letter> newMail = new List<Letter>();
            DateTime lastCheck = user.LastTimeChecked;
            TcpClient tcpclient = new TcpClient(); 
            tcpclient.Connect("pop." + user.Server, user.PopPort); 
            System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());
            sslstream.AuthenticateAsClient("pop." + user.Server); 
            System.IO.StreamWriter sw = new StreamWriter(sslstream); 
            System.IO.StreamReader reader = new StreamReader(sslstream);

            if (user.Server == "gmail.com")
            {
                sw.WriteLine("USER recent:" + user.Adress);
            }
            else
            {
                sw.WriteLine("USER " + user.Adress);
            }

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
                    done = true;
               cntr++;
            }

            sw.WriteLine("Quit ");
            sw.Flush();
            user.LastTimeChecked = DateTime.Now;
            Profile.DB_Update(user);
            return newMail;
        } // Не использовать
    }
}
