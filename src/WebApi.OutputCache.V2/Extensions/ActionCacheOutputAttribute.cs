using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.OutputCache.Core.Time;
using WebApi.OutputCache.V2.Extensions.Key;

namespace WebApi.OutputCache.V2.Extensions
{
    /// <summary>
    /// Action 缓存处理特性
    /// </summary>
    public  class ActionCacheOutputAttribute : CacheOutputAttribute
    {
        


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheSecondsTime">缓存时间 (秒)</param> 
        public ActionCacheOutputAttribute(int cacheSecondsTime)
        {
            // 缓存时间设置
            ClientTimeSpan = cacheSecondsTime; 
            ServerTimeSpan = cacheSecondsTime;
            // 禁止参数做为KEY值
            ExcludeQueryStringFromCacheKey = true;
            // 自定义KEY生成规则
            CacheKeyGenerator = typeof(UserRequestCacheKeyGenerator);
        }



        public override void OnActionExecuting(HttpActionContext actionContext)
        {
             
            // Do SomeThing 
            base.OnActionExecuting(actionContext);
        }


        public override async Task OnActionExecutedAsync(
            HttpActionExecutedContext actionExecutedContext, 
            CancellationToken cancellationToken)
        {
            // Do SomeThing  
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken); 
        }

    }
}
