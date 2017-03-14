using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace WebApi.OutputCache.V2.Extensions.Key
{
     
    /// <summary>
    /// KEY 生成规则
    /// </summary>
    public class UserRequestCacheKeyGenerator : DefaultCacheKeyGenerator
    {
        /// <summary>
        /// 过滤参数名
        /// </summary>
        private string[] IgnoreRequestNameList { get; set; } = new[] { "_", "_t", "t" };

        private readonly string _key;

        public UserRequestCacheKeyGenerator(string key)
        {
            _key = key;
        }


        public override string MakeCacheKey(HttpActionContext context, MediaTypeHeaderValue mediaType, bool excludeQueryString = false)
        { 
            var controller = context.ControllerContext.ControllerDescriptor.ControllerType.FullName;
            var action = context.ActionDescriptor.ActionName;
            var key = context.Request.GetConfiguration().CacheOutputConfiguration().MakeBaseCachekey(controller, action);
            var actionParameters = context.ActionArguments.Where(x => x.Value != null&& !IgnoreRequestNameList.Contains(x.Key)).Select(x => x.Key + "=" + GetValue(x.Value)); 
            string parameters  = "-" + string.Join("&", actionParameters); 
            parameters = parameters + ("-" + "UserName");  
            var cachekey = string.Format("{0}{1}:{2}", key, parameters, mediaType);
            return cachekey; 
        }

         
    }
}
