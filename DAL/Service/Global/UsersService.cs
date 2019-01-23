
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Business;
using MyOrm.Common;
using Newtonsoft.Json;
using log4net;

namespace DAL
{
    public interface IUsersService : IEntityService<Users>, IEntityViewService<Users>, IEntityService, IEntityViewService
    {
        int Login(string userId, string userPwd);

        [Transaction]
        bool Insert(Users user, List<UserInRole> roles);

        [Transaction]
        bool Delete(int userId);

        [Transaction]
        bool Update(Users user, List<UserInRole> roles);
    }

    public class UsersService : ServiceBase<Users, Users>, IUsersService
    {
        public readonly static ILog logger = LogManager.GetLogger("ServiceInterceptor");

        public int Login(string userId, string userPwd)
        {
            logger.Info("Begin To Login,User Id:" + userId + ",User Pwd:" + userPwd);
            try
            {
                string message = string.Empty;
                List<Users> list = this.Search(new SimpleCondition("UserID", ConditionOperator.Equals, userId));
                if (list.Count <= 0)
                {
                    message = "用户名错误";
                    logger.Error("User Id Error,User Id:" + userId + ",User Pwd:" + userPwd);
                    throw new CustomException(message);
                }
                Users u = list[0];
                if (u.Password != userPwd)
                {
                    message = "密码错误";
                    logger.Error("User Pwd Error,User Id:" + userId + ",User Pwd:" + userPwd);
                    throw new CustomException(message);
                }
                logger.Info("Login Successfully,User Id:" + userId + ",User Pwd:" + userPwd);

                u.LastLogin = DateTime.Now;
                this.Update(u);
                return u.ID;
            }
            catch (Exception ex)
            {
                logger.Error("Login Exception,User Id:" + userId + ",User Pwd:" + userPwd, ex);
                throw ex;
            }
        }

        public bool Insert(Users user, List<UserInRole> roles)
        {
            logger.Info("Begin To Insert Data,User:" + JsonConvert.SerializeObject(user) + ",Roles:" + JsonConvert.SerializeObject(roles));
            try
            {
                Factory.UsersService.Insert(user);

                if (roles != null)
                {
                    for (int i = 0; i < roles.Count; i++)
                    {
                        UserInRole role_user = new UserInRole();
                        role_user.RoleID = roles[i].RoleID;
                        role_user.UserID = user.ID;
                        Factory.UserInRoleService.Insert(role_user);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Insert User Failed", ex);
                throw ex;
            }
        }

        public bool Delete(int userId)
        {
            logger.Info("Begin To Delete User,User Id:" + userId);
            try
            {
                Factory.UsersService.DeleteID(userId);
                List<UserInRole> list = Factory.UserInRoleService.Search(new SimpleCondition("UserID", userId));
                Factory.UserInRoleService.BatchDelete(list);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Delete User Failed", ex);
                throw ex;
            }
        }

        public bool Update(Users user, List<UserInRole> roles)
        {
            try
            {
                logger.Info("Begin To Insert Data,User:" + JsonConvert.SerializeObject(user) + ",Roles:" + JsonConvert.SerializeObject(roles));

                Factory.UsersService.Update(user);
                List<UserInRole> list = Factory.UserInRoleService.Search(new SimpleCondition("UserID", user.ID));
                Factory.UserInRoleService.BatchDelete(list);

                if (roles != null)
                {
                    for (int i = 0; i < roles.Count; i++)
                    {
                        UserInRole role_user = new UserInRole();
                        role_user.RoleID = roles[i].RoleID;
                        role_user.UserID = user.ID;
                        Factory.UserInRoleService.Insert(role_user);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Update User Failed", ex);
                throw ex;
            }
        }
    }
}