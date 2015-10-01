using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GEMC
{
    class LetterBuildingDirector
    {
        private readonly ILetterBuilder _builder;

        public LetterBuildingDirector(ILetterBuilder builder)
        {
            this._builder = builder;
            
        }

        public void ConstructDefault(string profileid, string subject, string body, string from, string to,string category)
        {
            this._builder.BuildMain(profileid, subject, body);
            this._builder.BuildFromTo(from, to);
            this._builder.BuildAnother(category);
        }

        public void ConstructFromDataReader(SqlDataReader dr)
        {
            this._builder.BuildMain(dr[1].ToString(),  dr[2].ToString(), dr[3].ToString());
            this._builder.BuildFromTo(dr[4].ToString(), dr[5].ToString());
            this._builder.BuildAnother(dr[6].ToString());
        }

        public void ConstructFromMailCode(string MailCode)
        {
            //
        }

    }
}
