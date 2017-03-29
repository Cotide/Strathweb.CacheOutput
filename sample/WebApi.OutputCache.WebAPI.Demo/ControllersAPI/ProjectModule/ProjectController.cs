using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.OutputCache.WebAPI.Demo.ControllersAPI.ProjectModule
{
    /// <summary>
    /// 产品模块
    /// </summary>
    public class ProjectController : ApiController
    {

        /// <summary>
        /// 获取当前用户所有产品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Json("Hello World");
        }
    }
}
