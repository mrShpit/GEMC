namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProxyList
    {
        private List<ProxyLetter> _list = new List<ProxyLetter>();
        private BuildListStrategy _buildstrategy;

        public void SetFillStrategy(BuildListStrategy buildstrategy)
        {
            this._buildstrategy = buildstrategy;
        }

        public void Fill(Profile user)
        {
            this._buildstrategy.Fill(this._list, user);
        }

        public void Sort()
        {
            this._buildstrategy.Sort(this._list);
        }

        public List<ProxyLetter> ReturnList()
        {
            return this._list;
        }
    }
}
