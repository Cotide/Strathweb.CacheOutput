using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2.Demo.Model;
using WebApi.OutputCache.V2.Extensions;

namespace WebApi.OutputCache.V2.Demo.Controllers.Extensions
{
    /// <summary>
    /// Action 缓存
    /// </summary>
    public  class ActionCacheController : ApiController
    {

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="t">参数值</param>
        /// <returns></returns>
        // [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        [ActionCacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)] 
        public IHttpActionResult Get(string t = "")
        {
            return Json(DateTime.Now.ToString("yyyy - MM - dd HH:mm:ss"));
        }

    }
}
