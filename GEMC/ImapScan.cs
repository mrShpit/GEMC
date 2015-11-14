namespace GEMC
{
    using System;
    using System.Text;
    using System.Windows;
    using System.Net.Sockets;
    using System.Collections.Generic;
    using System.IO;

    public class ImapScan
    {
        private static System.Net.Sockets.TcpClient tcpc = null;
        private static System.Net.Security.SslStream ssl = null;
        private static byte[] dummy;
        private static byte[] buffer;
        private static int bytes = -1;

        public static void CheckForNewLettersIMAP(Profile user, int letterNum)
        {
            Letter newLetter = new Letter();
            newLetter.To = user.Adress;
            Authenticate(user);
            ImapRequest("$ SELECT INBOX\r\n");
            ImapRequest("$ STATUS INBOX (MESSAGES)\r\n");
            List<string> letterHeader = GetHeaderOfLetter(letterNum);

            foreach (string line in letterHeader)
            {
                if (line.Contains("From: "))
                {
                    newLetter.From = line.Substring(6);
                    MessageBox.Show(line.Substring(6));
                }

                if (line.Contains("Date: "))
                {
                    MessageBox.Show(line.Substring(6));
                }

                if (line.Contains("Subject: "))
                {
                    newLetter.Subject = line.Substring(9);
                    MessageBox.Show(line.Substring(9));
                }
            }

            Authenticate(user);
            ImapRequest("$ SELECT INBOX\r\n");
            newLetter.Body = GetTextOfLetter(letterNum);
        }

        private static List<string> GetHeaderOfLetter(int letterNum)
        {
            StringBuilder sb = new StringBuilder();
            string lastReadPart = string.Empty;
           
            sb.Append(lastReadPart = ImapRequest("$ FETCH " + letterNum + " BODY[header]\r\n"));
            while (true)
            {
                if (lastReadPart.Contains("$ OK ") && !lastReadPart.Contains("FLAGS"))
                {
                    break;
                }
                else
                {
                    sb.Append(lastReadPart = ImapRequest(string.Empty));
                }
            }

            List<string> message = new List<string>();
            foreach (string line in sb.ToString().Split('\n'))
            {
                message.Add(line + "\n");
            }
            
            sb.Clear();
            foreach (string line in message)
            {
                sb.Append(line);
            }

            ImapRequest("$ LOGOUT\r\n");
            return message;
        }

        private static string GetTextOfLetter(int letterNum)
        {
            StringBuilder sb = new StringBuilder();
            string lastReadPart = string.Empty;
            ImapRequest("$ FETCH " + letterNum + " BODY[text]\r\n");
            sb.Append(lastReadPart = ImapRequest("$ LOGOUT\r\n"));
            while (true)
            {
                if (lastReadPart.Contains("$ OK "))
                {
                    break;
                }
                else
                {
                    sb.Append(lastReadPart = ImapRequest(string.Empty));
                }
            }
            
            List<string> message = new List<string>();
            foreach (string line in sb.ToString().Split('\n'))
            {
                message.Add(line + "\n");
            }

            // убрать одну строчку в начале и три в конце
            message.RemoveAt(0);
            message.RemoveAt(message.Count - 1);
            message.RemoveAt(message.Count - 1);
            message.RemoveAt(message.Count - 1);

            sb.Clear();
            foreach (string line in message)
            {
                sb.Append(line);
            }

            TestWindow t = new TestWindow(sb.ToString());
            t.Show();
            return sb.ToString(); 
        }

        private static void Authenticate(Profile user)
        {
            tcpc = new TcpClient("imap." + user.Server, 993);
            ssl = new System.Net.Security.SslStream(tcpc.GetStream());
            ssl.AuthenticateAsClient("imap." + user.Server);
            ImapRequest("$ LOGIN " + user.Adress + " " + user.Password + "\r\n");
        }

        private static string ImapRequest(string commandText)
        {
            string strTemp = string.Empty;
            string str = string.Empty;

            if (commandText != string.Empty)
            {
                if (tcpc.Connected)
                {
                    dummy = Encoding.ASCII.GetBytes(commandText);
                    ssl.Write(dummy, 0, dummy.Length);
                }
            }

            ssl.Flush();
            buffer = new byte[2048];
            bytes = ssl.Read(buffer, 0, 2048);
            str = Encoding.ASCII.GetString(buffer);
            ssl.ReadTimeout = 1000;
            return str.Replace("\0", string.Empty);
        }
    }
}
