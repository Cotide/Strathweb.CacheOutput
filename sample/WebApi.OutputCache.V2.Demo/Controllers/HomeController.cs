using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.Demo.Controllers.Base;

namespace WebApi.OutputCache.V2.Demo.Controllers
{
   
    public  class HomeController : BaseApiController
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Help()
        {
            var str = new List<string>
            {
                string.Format("/api/home/help - {0}", "API清单"),
                string.Format("/api/user/get - {0}", "获取当前登录用户"),
                string.Format("/api/user/login - {0}", "用户登录"),
                string.Format("/api/user/logout - {0}", "用户退出")
            };

            return Json(str);
        }
    }
}
