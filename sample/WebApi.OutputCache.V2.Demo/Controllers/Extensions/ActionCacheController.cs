using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers; 
using WebApi.OutputCache.V2.Extensions;

namespace WebApi.OutputCache.V2.Demo.Controllers.Extensions
{
    /// <summary>
    /// Action 缓存
    /// </summary>
    public  class ActionCacheController : ApiController
    {
          
        [ActionCacheOutput(50)] 
        public IHttpActionResult Get(string t = "",string name = "")
        {
            return Json(DateTime.Now.ToString("yyyy - MM - dd HH:mm:ss"));
        }  
    }

}
