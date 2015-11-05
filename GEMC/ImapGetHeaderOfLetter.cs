namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImapGetHeaderOfLetter : ImapCommand
    {
        private int letterNum;

        public ImapGetHeaderOfLetter(int letterNumber)
        {
            this.letterNum = letterNumber;
        }

        public override object Execute()
        {
            PostClient pc = PostClient.instance;
            StringBuilder sb = new StringBuilder();
            string lastReadPart = string.Empty;

            sb.Append(lastReadPart = pc.ImapRequest("$ FETCH " + this.letterNum + " BODY[header]\r\n"));
            while (true)
            {
                if (lastReadPart.Contains("$ OK ") && !lastReadPart.Contains("FLAGS"))
                {
                    break;
                }
                else
                {
                    sb.Append(lastReadPart = pc.ImapRequest(string.Empty));
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

            pc.ImapRequest("$ LOGOUT\r\n");
            return message;
        }
    }
}
