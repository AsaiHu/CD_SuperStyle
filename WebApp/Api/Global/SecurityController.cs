using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Business;
using Models;
using MyOrm.Common;
using Utilities;
using System.Web.Security;

namespace WebApp.Api
{
    public class SecurityController : BaseController
    {
        [AllowAnonymous]
        public JsonResult Login(Users u)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                string message = string.Empty;
                if (!Security.Login(u.UserID, Utilities.Encrypt.Instance.EncryptString(u.Password), ref message))
                {
                    json.Data = JsonUtil.GetFailForString(message);
                }
                else
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, u.UserID, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", u.UserID, Encrypt.Instance.EncryptString(u.Password)),
                            FormsAuthentication.FormsCookiePath);
                    json.Data = JsonUtil.GetSuccessForString(FormsAuthentication.Encrypt(ticket));
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult ChangePwd(Users u)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IUsersService service = ServiceFactory.Factory.UsersService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", u.ID));
                //查询数据
                Users user = service.SearchOne(condition);
                if (user != null)
                {
                    user.Password = Encrypt.Instance.EncryptString(u.Password);
                    service.Update(user);
                    json.Data =JsonUtil.GetSuccessForString("密码修改已完成");
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Logout()
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Utilities.Security.Logout();
                json.Data=JsonUtil.GetSuccess();
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }
    }
}