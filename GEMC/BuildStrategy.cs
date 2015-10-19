namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Joins a first name and a last name together into a single string.
    /// </summary>
    /// <param name="firstName">The first name to join.</param>
    /// <param name="lastName">The last name to join.</param>
    /// <returns>The joined names.</returns>
    public abstract class BuildListStrategy
    {
        public abstract void Fill(List<ProxyLetter> list, Profile user);

        public abstract void Sort(List<ProxyLetter> list);
    }
}
