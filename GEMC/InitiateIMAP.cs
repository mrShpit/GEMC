namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Sockets;
    using System.Net.Mail;
    using System.Net.Security;

    public class InitiateIMAP : ServerProtocol
    {
        public override SslStream Authenticate(Profile user)
        {
            ImapConsole console = new ImapConsole();
            SslStream ssl = console.SetSSLConnectionAndReturn(user);
            console.SendCommand(new ImapAuthenticate(user));
            console.ExecuteCommand();

            return ssl;
        }

        public override int SendRequestToServer(SslStream ssl, string request)
        {
            PostClient pc = PostClient.instance;
            pc.ImapRequest("$ SELECT INBOX\r\n");

            return 0;
        }

        public override object PullDataFromServer(SslStream ssl, int flag)
        {
            ImapConsole console = new ImapConsole(ssl);

            console.SendCommand(new ImapGetTextOfLetter(flag));
            string letterText = (string)console.ExecuteCommand();

            return letterText;
        }

        public override void LogOut(SslStream ssl)
        {
            PostClient pc = PostClient.instance;
            pc.ImapRequest("$ LOGOUT\r\n");
        }
    }  
}
