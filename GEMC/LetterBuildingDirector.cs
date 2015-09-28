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

        public void ConstructDefault(string name, string subject, string body, string from, string to,string category)
        {
            this._builder.BuildMain(name, subject, body);
            this._builder.BuildFromTo(from, to);
            this._builder.BuildAnother(category);
        }

        public void ConstructFromDataReader(SqlDataReader dr)
        {
            this._builder.BuildMain(dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            this._builder.BuildFromTo(dr[5].ToString(), dr[6].ToString());
            this._builder.BuildAnother(dr[7].ToString());
        }

        
    }
}
