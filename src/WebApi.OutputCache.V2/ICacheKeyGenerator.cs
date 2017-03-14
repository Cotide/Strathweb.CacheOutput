using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace WebApi.OutputCache.V2
{
    /// <summary>
    /// 缓存KEY 生成接口
    /// </summary>
    public interface ICacheKeyGenerator
    {
        /// <summary>
        /// 生成缓存KEY
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mediaType"></param>
        /// <param name="excludeQueryString"></param>
        /// <returns></returns>
        string MakeCacheKey(HttpActionContext context, MediaTypeHeaderValue mediaType, bool excludeQueryString = false);
    }
}
