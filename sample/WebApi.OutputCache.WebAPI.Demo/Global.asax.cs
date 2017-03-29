using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApi.OutputCache.WebAPI.Demo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Mvc 路由注册 
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);  

            // WebAPI 路由注册
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}