using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace WebApi.OutputCache.Core.Cache
{
    /// <summary>
    /// 缓存实现
    /// </summary>
    public class MemoryCacheDefault : IApiOutputCache
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        private static readonly MemoryCache Cache = MemoryCache.Default;

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public virtual void RemoveStartsWith(string key)
        {
            lock (Cache)
            {
                Cache.Remove(key);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存对象</typeparam>
        /// <param name="key">KEY 值</param>
        /// <returns></returns>
        public virtual T Get<T>(string key) where T : class
        {
            var o = Cache.Get(key) as T;
            return o;
        }

        /// <summary>
        /// 获取缓存 (旧方法)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("Use Get<T> instead")]
        public virtual object Get(string key)
        {
            return Cache.Get(key);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">Key 值</param>
        public virtual void Remove(string key)
        {
            lock (Cache)
            {
                Cache.Remove(key);
            }
        }

        /// <summary>
        /// 是否存在指定KEY缓存
        /// </summary>
        /// <param name="key">Key 值</param>
        /// <returns></returns>
        public virtual bool Contains(string key)
        {
            return Cache.Contains(key);
        }


        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">KEY 值</param>
        /// <param name="o">缓存内容</param>
        /// <param name="expiration">缓存时间</param>
        /// <param name="dependsOnKey">失效Key值</param>
        public virtual void Add(
            string key,
            object o, 
            DateTimeOffset expiration,
            string dependsOnKey = null)
        {
            // 缓存策略详细信息
            var cachePolicy = new CacheItemPolicy
            {
                // 获取或设置一个值，该值指示是否应在指定的持续时间之后逐出缓存项
                AbsoluteExpiration = expiration
            };

            // 缓存失效策略
            if (!string.IsNullOrWhiteSpace(dependsOnKey))
            {
                // 此专门的更改监视器用于监视中指定的缓存条目 keys 集合项更改时触发事件
                cachePolicy.ChangeMonitors.Add(
                    Cache.CreateCacheEntryChangeMonitor(new[] { dependsOnKey })
                );
            }
            lock (Cache)
            {
                // 记录缓存
                Cache.Add(key, o, cachePolicy);
            }
        }

        /// <summary>
        /// 获取所有缓存KEY
        /// </summary>
        public virtual IEnumerable<string> AllKeys
        {
            get
            {
                return Cache.Select(x => x.Key);
            }
        }
    }
}
