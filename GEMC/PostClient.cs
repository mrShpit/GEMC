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

        public Letter CheckLetterByIMAP(Profile user, int letterNum)
        {
            Letter newLetter = new Letter();
            newLetter.SetId();
            newLetter.ProfileId = user.Id;
            newLetter.To = user.Adress;
            newLetter.Category = "Inbox";

            bool subjectFound = false;
            bool dateFound = false;
            bool fromFound = false;

            ImapConsole console = new ImapConsole();
            console.SetSSLConnection(user);
            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();

            this.ImapRequest("$ SELECT INBOX\r\n");

            console.SendCommand(new ImapGetHeaderOfLetter(letterNum));
            List<string> letterHeader = (List<string>)console.ExecuteCommand();

            foreach (string line in letterHeader)
            {
                if (!fromFound && line.Contains("From: "))
                {
                    string thisLine = line;
                    if (line.Contains("<"))
                    {
                        thisLine = line.Substring(line.IndexOf('<') + 1, line.IndexOf('>') - line.IndexOf('<') - 1);
                        newLetter.From = thisLine;
                    }
                    else
                    {
                        newLetter.From = thisLine.Substring(6);
                    }

                    fromFound = true;
                }

                if (!dateFound && line.Length > 6 && line.Substring(0, 6) == "Date: ")
                {
                    string thisLine = line;
                    if (line.Contains("+"))
                    {
                        thisLine = line.Substring(0, line.IndexOf('+'));
                    }
                    else if (line.Contains("-"))
                    {
                        thisLine = line.Substring(0, line.IndexOf('-'));
                    }
                    
                    newLetter.SendingTime = Convert.ToDateTime(thisLine.Substring(6));
                    dateFound = true;
                }

                if (!subjectFound && line.Contains("Subject: "))
                {
                    newLetter.Subject = line.Substring(9).Replace("\r", string.Empty);
                    if (newLetter.Subject.Substring(0, 5) == "=?utf")
                    {
                        newLetter.Subject = newLetter.Subject.Substring(10, newLetter.Subject.Length - 10);
                        interpreterContext iC = new interpreterContext(newLetter.Subject);
                        DefaultExpression ex = new DefaultExpression();
                        ex.Interpret(iC);
                        newLetter.Subject = ex.UseEncoding(iC);
                    }

                    subjectFound = true;
                }

                if (subjectFound && dateFound && fromFound)
                {
                    break;
                }
            }

            console.SetSSLConnection(user);
            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();
            this.ImapRequest("$ SELECT INBOX\r\n");

            console.SendCommand(new ImapGetTextOfLetter(letterNum));
            newLetter.Body = (string)console.ExecuteCommand();

            return newLetter;
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
            int result = 0;
            ImapConsole console = new ImapConsole();
            console.SetSSLConnection(user);
            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();
            this.ImapRequest("$ SELECT INBOX\r\n");
            string answer = this.ImapRequest("$ STATUS INBOX (MESSAGES)\r\n");
            foreach (string line in answer.Split('\n'))
            {
                if (line.Contains("EXISTS"))
                {
                    result = Convert.ToInt32(line.Substring(2, line.IndexOf('E') - 2));
                    this.ImapRequest("$ LOGOUT\r\n");
                }
            }

            return result;
        }

        public List<Letter> PullFreshLetters(Profile user)
        {
            List<Letter> newLettersList = new List<Letter>();

            DateTime start = DateTime.Now;
            TimeSpan time;

            int totalLetterNumber = this.GetTotalLetterNum(user);

            for (int i = totalLetterNumber; i > 0; i--)
            {
                Letter newLetter = this.CheckLetterByIMAP(user, i);
                if (newLetter.SendingTime > user.LastTimeChecked)
                {
                    newLettersList.Add(newLetter);
                }
                else
                {
                    break;
                }
            }

            time = DateTime.Now - start;

            TimeSpan test = time;

            // user.LastTimeChecked = DateTime.Now;
            // Profile.DB_Update(user);
            return newLettersList;
        }
    }
}
