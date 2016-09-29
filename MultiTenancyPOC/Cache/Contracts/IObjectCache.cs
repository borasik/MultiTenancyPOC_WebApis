using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancyPOC.Cache.Contracts
{
    public interface IObjectCache
    {
        bool Add(CacheItem item, CacheItemPolicy policy);
        bool Contains(string key, string regionName = null);
        object Get(string key, string regionName = null);

    }
}
