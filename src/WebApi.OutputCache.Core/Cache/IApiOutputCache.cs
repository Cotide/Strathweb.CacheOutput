using System;
using System.Collections.Generic;

namespace WebApi.OutputCache.Core.Cache
{
    /// <summary>
    /// 缓存接口定义
    /// </summary>
    public interface IApiOutputCache
    {
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">KEY 值</param>
        void RemoveStartsWith(string key);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">Key 值</param>
        void Remove(string key);
         
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存对象</typeparam>
        /// <param name="key">KEY 值</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// 获取缓存 (旧方法)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("Use Get<T> instead")]
        object Get(string key);


        /// <summary>
        /// 是否存在指定KEY缓存
        /// </summary>
        /// <param name="key">Key 值</param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">KEY 值</param>
        /// <param name="o">缓存内容</param>
        /// <param name="expiration">缓存时间</param>
        /// <param name="dependsOnKey">失效Key值</param>
        void Add(string key,
            object o,
            DateTimeOffset expiration, 
            string dependsOnKey = null);


        /// <summary>
        /// 获取所有缓存KEY
        /// </summary>
        IEnumerable<string> AllKeys { get; }
    }
}