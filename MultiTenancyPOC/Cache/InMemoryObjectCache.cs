using System.Collections.Specialized;
using System.Runtime.Caching;
using MultiTenancyPOC.Cache.Contracts;
using MultiTenancyPOC.Cache.Enums;

namespace MultiTenancyPOC.Cache
{   
    public class InMemoryObjectCache: IObjectCache
    {
        private MemoryCache MemoryCache { get; set; }

        public InMemoryObjectCache(CacheIdentifiers cacheIdentifiers)
        {
            MemoryCache = new MemoryCache(cacheIdentifiers.ToString());
        }
        public bool Add(CacheItem item, CacheItemPolicy policy)
        {
            return MemoryCache.Add(item, policy);
        }

        public bool Contains(string key, string regionName = null)
        {
            return MemoryCache.Contains(key);
        }

        public object Get(string key, string regionName = null)
        {
            return MemoryCache.Get(key);
        }
    }
}