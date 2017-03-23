using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.OutputCache.V2.Demo.ViewModel
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class IdentityUser
    { 
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户身份票据
        /// </summary>
        public string Ticket { get; set; }
    }
}
