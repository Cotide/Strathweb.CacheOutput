using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;
using WebApi.OutputCache.Core.Cache;
using WebApi.OutputCache.V2.Demo.App_Start.Formatter;
using WebApi.OutputCache.V2.Demo.Controllers.Extensions;
using WebApi.OutputCache.V2.Extensions.Key;

namespace WebApi.OutputCache.V2.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 配置信息
            const string baseAddress = "http://localhost:9999";

            var config = new HttpSelfHostConfiguration(baseAddress);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action= "Index",
                    id = RouteParameter.Optional
                }
            );

            //var filter = new AuthorizationFilterAttribute();
            config.Formatters.Clear();
            // 注入自定义Json/Xml格式化器规则处理
            config.Formatters.Insert(0, new JsonNetFormatter());
            config.Formatters.Insert(1, new XmlNetFormatter());
            // 根据参数选择 格式化处理
            config.Formatters[0].AddQueryStringMapping("type","json", "application/json");
            config.Formatters[1].AddQueryStringMapping("type", "xml", "application/xml");



            // 注册缓存策略
            config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new MemoryCacheDefault());
            // 注册缓存Key 生成策略
            config.CacheOutputConfiguration().RegisterCacheKeyGeneratorProvider(() => new UserRequestCacheKeyGenerator("_userRequest"));


            // 注册服务  
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.ReadKey();
            server.CloseAsync().Wait();

        }
    }
}
