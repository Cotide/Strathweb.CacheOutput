using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.OutputCache.V2.Extensions
{
    /// <summary>
    /// Action 缓存处理特性
    /// </summary>
    public  class ActionCacheOutputAttribute : CacheOutputAttribute
    {
        /// <summary>
        /// 忽略参数列表
        /// </summary>
        public string[] IgnoreRequestName = new[] {"_","_t","t"};


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
