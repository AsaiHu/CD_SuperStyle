using DAL.Business;
using log4net;
using MyOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utilities;

namespace WebApp
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //public readonly static ILog logger = LogManager.GetLogger("ServiceInterceptor");

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //var attributes = filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>();
            //bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase context)
        {
            var authorization = context.Request.Headers["Ticket"];
            if (authorization != null && !authorization.ToLower().Equals("null"))
            {
                if (ValidateTicket(authorization.ToString()))
                {
                    //logger.Info("ValidateTicket Successfully");
                    base.AuthorizeCore(context);
                    return true;
                }
            }
            context.Response.StatusCode = 401;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
             base.HandleUnauthorizedRequest(filterContext);
        }

        /// <summary>
        /// 校验用户名密码（正式环境中应该是数据库校验）
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        public bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);
            //logger.Info("ValidateTicket User:" + strUser + ",Pwd:" + strPwd);

            ConditionSet condition = new ConditionSet();
            condition.Add(new SimpleCondition("UserID", strUser));
            condition.Add(new SimpleCondition("Password", strPwd));
            DAL.Users user = ServiceFactory.Factory.UsersService.SearchOne(condition);
            return user != null ? true : false;
        }
    }
}