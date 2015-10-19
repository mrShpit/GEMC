namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DefaultDBClass
    {
            private string p_Id;

            public string Id
            {
                get { return this.p_Id; }
                set { this.p_Id = value; }
            }

            public void SetId()
            {
                this.Id = Guid.NewGuid().ToString();
            }
    }
}
