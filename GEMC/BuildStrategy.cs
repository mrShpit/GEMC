using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMC
{
    abstract class BuildListStrategy
    {
        public abstract void Fill(List<ProxyLetter> list, Profile user);
        public abstract void Sort(List<ProxyLetter> list);
    }
}
