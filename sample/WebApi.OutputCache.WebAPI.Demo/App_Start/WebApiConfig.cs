using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.Core.Cache;
using WebApi.OutputCache.V2;
using WebApi.OutputCache.V2.Extensions.Key;
using WebApi.OutputCache.WebAPI.Demo.Areas.HelpPage;
using WebApi.OutputCache.WebAPI.Demo.Formatter;

namespace WebApi.OutputCache.WebAPI.Demo
{
    /// <summary>
    /// MVC 路由注册
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            // 注册帮助文档路由
            config.SetDocumentationProvider(new XmlDocumentationProvider(
                HttpContext.Current.Server.MapPath(
                    "~/bin/WebApi.OutputCache.WebAPI.Demo.XML")));

            config.Formatters.Clear();
            // 注入自定义Json/Xml格式化器规则处理
            config.Formatters.Insert(0, new JsonNetFormatter());
            config.Formatters.Insert(1, new XmlNetFormatter());
            // 根据参数选择 格式化处理
            config.Formatters[0].AddQueryStringMapping("type", "json", "application/json");
            config.Formatters[1].AddQueryStringMapping("type", "xml", "application/xml");
  


            // 注册缓存策略
            config.CacheOutputConfiguration()
                .RegisterCacheOutputProvider(
                () => new MemoryCacheDefault());

            // 注册缓存Key 生成策略
            config.CacheOutputConfiguration()
                .RegisterCacheKeyGeneratorProvider(
                () => new UserRequestCacheKeyGenerator("_userRequest"));
        }
    }
}
