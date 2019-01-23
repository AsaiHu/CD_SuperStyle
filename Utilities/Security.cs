using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using MyOrm.Common;
using DAL.Business;
using System.ComponentModel;

namespace Utilities
{
    public class Security
    {
        public static string cookiename = "soochow_admin";

        public static bool IsLogin
        {
            get
            {
                return !object.Equals(null, HttpContext.Current.Request.Cookies[cookiename]);
            }
        }

        public static Users CurrentUser
        {
            get
            {
                if (IsLogin)
                {
                    string userID = HttpUtility.UrlEncode(HttpContext.Current.Request.Cookies[cookiename]["userid"].ToString());
                    //sy_User user = Tools.GetFromCache(userID) as sy_User;
                    //if (object.Equals(user, null))
                    //{ && c.EnterpriseID.Equals(enterpriseId)
                    Users u = ServiceFactory.Factory.UsersService.SearchOne(new SimpleCondition("ID", userID));
                    Cache.AddToCache("ADMIN_" + u.ID.ToString().Trim(), u);
                    return u;
                    //}
                    //else
                    //{
                    //    return user;
                    //}
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="message">执行信息</param>
        /// <returns></returns>
        public static bool Login(string userID, string password, ref string message)
        {
            try
            {
                int Id = ServiceFactory.Factory.UsersService.Login(userID, password);
                HttpCookie cookie = new HttpCookie(cookiename);
                cookie.Values.Add("userid", HttpUtility.UrlEncode(Id.ToString()));
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        public static void Logout()
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            Cache.RemoveCache("ADMIN_" + CurrentUser.UserID);
        }
    }
}
