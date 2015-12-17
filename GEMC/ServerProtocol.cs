namespace GEMC
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Net.Mail;
    using System.Net.Security;

    public abstract class ServerProtocol : InternetProtocol
    {
        public sealed override object InitiateProtocol(Profile user, string request)
        {
            SslStream ssl = this.Authenticate(user);
            int flag = this.SendRequestToServer(ssl, request);
            object result = this.PullDataFromServer(ssl, flag);
            this.LogOut(ssl);

            return result;
        }

        public abstract SslStream Authenticate(Profile user);

        public abstract int SendRequestToServer(SslStream ssl, string requestLine);

        public abstract object PullDataFromServer(SslStream ssl, int flag);

        public abstract void LogOut(SslStream ssl);
    }
}
