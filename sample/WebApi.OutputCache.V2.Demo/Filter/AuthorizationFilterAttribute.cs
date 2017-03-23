//using System;
//using System.Linq;
//using System.Net.Http;
//using System.Security;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters; 
//using Newtonsoft.Json;
//using System.Web.Security;
//using WebApi.OutputCache.V2.Demo.Controllers.Extensions;

//namespace WebApi.OutputCache.V2.Demo.Filter
//{
//    public class AuthorizationFilterAttribute : ActionFilterAttribute
//    {
//        private SecurityHeader _securityHeader;
//        private SecurityCookieHeader _cookieHeader;
//        public override void OnActionExecuting(HttpActionContext actionContext)
//        {
//            try
//            {
//                var controller = (UserController)actionContext.ControllerContext.Controller;
//                var header =
//                    actionContext.Request.Headers.GetCookies(FormsAuthentication.FormsCookieName);
//                if (header != null && header.Count > 0)
//                {
//                    var cookie =
//                        header.First()
//                            .Cookies.FirstOrDefault(one => one.Name == FormsAuthentication.FormsCookieName);

//                    if (cookie != null && cookie.Value != null)
//                    {
//                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

//                        if (ticket != null)
//                        {
//                            _cookieHeader =
//                                JsonConvert.DeserializeObject<SecurityCookieHeader>(ticket.UserData);
//                            _securityHeader = controller.CacheProvider.Get<SecurityHeader>(_cookieHeader.Email);
//                            if (_securityHeader == null)
//                            {
//                                SetSecurityHeader();
//                            }
//                            if (!SecurityHelper.Login(_securityHeader).Identity.IsAuthenticated)
//                            {
//                                throw new SecurityException("Unable to login");
//                            }
//                            controller.SecurityHeader = _securityHeader;
//                            base.OnActionExecuting(actionContext);
//                        }
//                        else
//                        {
//                            throw new SecurityException("Unable to login");
//                        }
//                    }
//                    else
//                    {
//                        throw new SecurityException("Unable to login");
//                    }
//                }
//                else
//                {
//                    throw new SecurityException("Unable to login");
//                }

//            }
//            catch (SecurityException exception)
//            {
//                var controller = (AuthorizedController)actionContext.ControllerContext.Controller;
//                controller.ExceptionHandler.HandleException(exception);
//                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
//                {
//                    Content = new StringContent(JsonConvert.SerializeObject(
//                        new ExecutionResult<bool?>
//                        {
//                            ErrorMessage = "Invalid login.",
//                            Result = null,
//                            Success = false
//                        }))
//                };
//            }

//        }

//        private void SetSecurityHeader()
//        {
//            // cache the user data for speed and set it on a property of AuthorizedController to identify the user inside the controller action
//        }
//    }
//}
