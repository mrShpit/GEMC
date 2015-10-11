using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMC
{
    public abstract class ILetterBuilder
    {
        public abstract void BuildMain(string profileid, string subject, string body);
        public abstract void BuildFromTo(string from, string to);
        public abstract void BuildAnother(string data);
        public abstract Letter GetResult();
    }

}
