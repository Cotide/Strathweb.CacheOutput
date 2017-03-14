using System;

namespace WebApi.OutputCache.Core.Cache
{
    /// <summary>
    /// 缓存扩展
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">返回数据对象</typeparam>
        /// <param name="cache">缓存接口定义</param>
        /// <param name="key">KEY 值</param>
        /// <param name="expiry">缓存时间</param>
        /// <param name="resultGetter">处理回调方法</param>
        /// <param name="bypassCache">失效Key值</param>
        /// <returns></returns>
        public static T GetCachedResult<T>(this IApiOutputCache cache, 
            string key,
            DateTimeOffset expiry
            , Func<T> resultGetter, 
            bool bypassCache = true) where T : class
        {
            var result = cache.Get<T>(key);

            if (result == null || bypassCache)
            {
                result = resultGetter();
                if (result != null) cache.Add(key, result, expiry);
            }

            return result;
        }
    }
}