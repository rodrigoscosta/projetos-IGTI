using Microsoft.Extensions.Caching.Memory;
using System;

namespace BookStore.Filters.ResourceFilters.Caching
{
    public class SimpleMemoryCache
    {
        private MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public T GetOrCreate<T>(object key, Func<T> createItem)
        {
            T cacheEntry;

            if (!cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = createItem();

                cache.Set(key, cacheEntry);
            }
            return cacheEntry;
        }

        public bool CheckIfExists(object key)
        {
            var entry = cache.Get(key);

            return entry != null;
        }
    }
}