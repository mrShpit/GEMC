﻿namespace GEMC
{
    using System.Net.Sockets;
    using System.Net.Security;

    public class ImapConsole
    {
        public static TcpClient _tcpc;
        public static SslStream _ssl;
        private ImapCommand _command;

        public ImapConsole(SslStream ssl)
        {
            _ssl = ssl;
        }

        public ImapConsole()
        {
        }

        public void SetSSLConnection(Profile user)
        {
            _tcpc = new TcpClient("imap." + user.Server, 993);
            _ssl = new System.Net.Security.SslStream(_tcpc.GetStream());
        }
       
        public SslStream SetSSLConnectionAndReturn(Profile user)
        {
            _tcpc = new TcpClient("imap." + user.Server, 993);
            _ssl = new System.Net.Security.SslStream(_tcpc.GetStream());
            return _ssl;
        }

        public void DisposeConnection()
        {
            _tcpc = null;
            _ssl = null;
        }

        public void SendCommand(ImapCommand command)
        {
            this._command = command;
        }

        public object ExecuteCommand()
        {
            return this._command.Execute();
        }
    }
}
