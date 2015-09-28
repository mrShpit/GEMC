using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMC
{
    public class DefaultDBClass
    {
        
            private string p_Id;
            public string Id
            {
                get { return p_Id; }
                set { p_Id = value; }
            }
            public void SetId()
            {
                Id = Guid.NewGuid().ToString();
            }
        
    }
}
