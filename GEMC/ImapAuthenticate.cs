namespace GEMC
{
    public class ImapAuthenticate : ImapCommand
    {
        private Profile user;

        public ImapAuthenticate(Profile profile)
        {
            this.user = profile;
        }

        public override object Execute()
        {
            PostClient pc = PostClient.instance;
            ImapConsole._ssl.AuthenticateAsClient("imap." + this.user.Server);
            pc.ImapRequest("$ LOGIN " + this.user.Adress + " " + this.user.Password + "\r\n");
            return 0;
        }
    }
}
