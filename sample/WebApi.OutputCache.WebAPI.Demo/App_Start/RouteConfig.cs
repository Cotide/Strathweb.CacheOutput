using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApi.OutputCache.WebAPI.Demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
 
            // 设置默认跳转到HELP 页面
            routes.MapRoute(
                  "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Help", action = "Index", id = UrlParameter.Optional }, //这里要和Admin块下的默认控制器和action一样
                 new[] { "WebApi.OutputCache.WebAPI.Demo.Areas.HelpPage.Controllers" }// 这个是你控制器所在命名空间
             ).DataTokens.Add("area", "HelpPage");

         
            routes.MapRoute(
                name: "Default2",
                url: "{controller}/{action}/{id}",
                defaults: new
                { 
                    id = UrlParameter.Optional
                }
            );
     

        }
    }
}

