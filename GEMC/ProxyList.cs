using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMC
{
    class ProxyList
    {
        private List<ProxyLetter> _list = new List<ProxyLetter>();
        private BuildListStrategy _buildstrategy;

        public void SetFillStrategy(BuildListStrategy buildstrategy)
        {
            this._buildstrategy = buildstrategy;
        }

        public void Fill(Profile user)
        {
            _buildstrategy.Fill(_list, user);
        }
        public void Sort()
        {
            _buildstrategy.Sort(_list);
        }
       
    }
}
