namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImapGetTextOfLetter : ImapCommand
    {
        private int letterNum;

        public ImapGetTextOfLetter(int letterNumber)
        {
            this.letterNum = letterNumber;
        }

        public override object Execute()
        {
            PostClient pc = PostClient.instance;

            StringBuilder sb = new StringBuilder();
            string lastReadPart = string.Empty;
            pc.ImapRequest("$ FETCH " + this.letterNum + " BODY[text]\r\n");
            sb.Append(lastReadPart = pc.ImapRequest("$ LOGOUT\r\n"));
            while (true)
            {
                if (lastReadPart.Contains("$ OK "))
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
    }
}
