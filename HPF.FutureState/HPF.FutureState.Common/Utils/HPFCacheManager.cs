using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace HPF.FutureState.Common.Utils
{
    public class HPFCacheManager
    {
        private readonly ICacheManager _HPFCache;

        private static readonly HPFCacheManager instance = new HPFCacheManager();
        /// <summary>
        /// Singleton
        /// </summary>
        public static HPFCacheManager Instance
        {
            get
            {
                return instance;
            }
        }        

        protected HPFCacheManager()
        {
            _HPFCache = CacheFactory.GetCacheManager();                        
        }

        /// <summary>
        /// Add an object to cache with typename as a key
        /// </summary>
        /// <param name="value"></param>
        public void Add(object value)
        {
            var key = value.GetType().FullName;
            AddToCache(value, key);
        }        
        /// <summary>
        /// Add an object to cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            AddToCache(value, key);
        }
        /// <summary>
        /// Get an object T from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetData<T>()
        {
            return (T)_HPFCache.GetData(typeof (T).FullName);
        }

        /// <summary>
        /// Get an object from cache by key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData<T>(string key)
        {
            return (T)_HPFCache.GetData(key);
        }

        /// <summary>
        /// Remove cache item
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            _HPFCache.Remove(key);
        }

        /// <summary>
        /// Remove cache item T
        /// </summary>        
        public void RemoveCache<T>()
        {
            _HPFCache.Remove(typeof(T).FullName);
        }

        private void AddToCache(object value, string key)
        {
            var duration = GetDuration();
            _HPFCache.Add(key, value, CacheItemPriority.Normal, null,
                          new SlidingTime(TimeSpan.FromSeconds(duration)));
        }

        private static int GetDuration()
        {
            return 5;
        }
    }
}
