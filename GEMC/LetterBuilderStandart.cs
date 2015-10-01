using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GEMC
{
    public class LetterBuilderStandart : ILetterBuilder
    {
        private Letter _letter = new Letter();

        public override void BuildMain(string profileid, string subject, string body)
        {
            _letter.ProfileId= profileid;
            _letter.Subject = subject;
            _letter.Body = body;
        }

        public override void BuildFromTo(string from, string to)
        {
            _letter.From = from;
            _letter.To = to;
        }


        public override void BuildAnother(string category)
        {
            _letter.Category = category;
        }

        public override Letter GetResult()
        {
            return _letter;
        }
    }
}
