namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Mail;

    public class ExtendedAttachment
    {
        public Attachment AttachedObject = null;

        public ExtendedAttachment(string file)
        {
            this.AttachedObject = new Attachment(file);
        }

        private string particalName;

        public string ParticalName
        {
            get
            {
                if (this.AttachedObject.Name.Length < 14)
                {
                    return this.AttachedObject.Name;
                }
                else
                {
                    return this.AttachedObject.Name.Substring(0, 14) + "...";
                }
            }

            set
            {
                this.particalName = value;
            }
        }
    }
}
