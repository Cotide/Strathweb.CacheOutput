using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Security;
using WebApi.OutputCache.V2.Demo.Controllers.Base;
using WebApi.OutputCache.V2.Demo.ViewModel;

namespace WebApi.OutputCache.V2.Demo.Controllers.Extensions
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public  class UserController : BaseApiController
    {
        /// <summary>
        /// 获取当前登录用户
        /// </summary> 
        /// <returns></returns> 
        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(string.Format("Login User: {0}", User.Identity.Name));
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Login(string username, string password)
        {
            // 数据校验
            if (string.IsNullOrEmpty(username))
            { 
                throw new Exception("Please Enter UserName");
            }
              
            // 记录Cookies
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            0,
                            GetUserCookiesKey(username), DateTime.Now,
                            DateTime.Now.AddHours(1),
                            true,
                            username,
                            FormsAuthentication.FormsCookiePath); 

            // 记录Session
            var sessionTicket = new IdentityUser()
            {
                UserName = username,
                Ticket = FormsAuthentication.Encrypt(ticket)
            };

            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[UserSessionKey] = sessionTicket;

            return Json(sessionTicket);
        }


        public IHttpActionResult LogOut()
        {
            // 清除Cookies 
            FormsAuthentication.SignOut(); 
            // 清除Session
            HttpContext.Current.Session.Remove(UserSessionKey);
            return Ok();
        }
    }
}
