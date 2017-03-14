using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.OutputCache.V2
{
     /// <summary>
     /// 标记无效的缓存
     /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class InvalidateCacheOutputAttribute : BaseCacheAttribute
    {
        private string _controller;
        private readonly string _methodName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodName">模块名称</param>
        public InvalidateCacheOutputAttribute(string methodName)
            : this(methodName, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodName">模块名称</param>
        /// <param name="type">类型</param>
        public InvalidateCacheOutputAttribute(string methodName, Type type = null)
        {
            _controller = type != null ? type.FullName : null;
            _methodName = methodName;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null && !actionExecutedContext.Response.IsSuccessStatusCode) return;
            _controller = _controller ?? actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName;

            var config = actionExecutedContext.Request.GetConfiguration();
            EnsureCache(config, actionExecutedContext.Request);

            // 获取缓存KEY 
            var key = actionExecutedContext.Request.GetConfiguration().CacheOutputConfiguration().MakeBaseCachekey(_controller, _methodName);
            if (WebApiCache.Contains(key))
            {
                WebApiCache.RemoveStartsWith(key);
            }
        }
    }
}