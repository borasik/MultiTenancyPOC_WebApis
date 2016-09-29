using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultiTenancyPOC.Cache.Contracts;
using MultiTenancyPOC.Cache.Enums;

namespace MultiTenancyPOC.Cache
{
    public static class CacheFactory
    {
        private static Dictionary<string, IObjectCache> ObjectCaches { get; } = new Dictionary<string, IObjectCache>();

        public static IObjectCache GetCache(CacheType cacheType, CacheIdentifiers cacheIdentifiers)
        {            
            switch (cacheType)
            {
                case CacheType.InMemoryObjectCache:
                    IObjectCache inMemoryObjectCache;
                    if (!ObjectCaches.TryGetValue(cacheType.ToString() + cacheIdentifiers, out inMemoryObjectCache))
                    {
                        inMemoryObjectCache = new InMemoryObjectCache(cacheIdentifiers);
                        ObjectCaches.Add(cacheType.ToString() + cacheIdentifiers, inMemoryObjectCache);
                        return inMemoryObjectCache;
                    }

                    return inMemoryObjectCache;

                case CacheType.RedisObjectCache:
                    break;
                case CacheType.AzureRedisObjectCache:
                    break;
                default:
                    return null;
            }

            return null;
        }
    }
}