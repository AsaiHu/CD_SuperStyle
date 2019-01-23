using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Business;
using DAL;
using Newtonsoft.Json.Linq;
using MyOrm.Common;

namespace Adapters
{
    public class DataListAdapter
    {
        /// <summary>
        /// 根据“角色”数据生成DataList格式的Json数据
        /// </summary>
        /// <returns></returns>
        public JArray GetRole()
        {
            List<Roles> list = ServiceFactory.Factory.RolesService.Search(null);
            JArray array = new JArray();
            for (int i = 0; i < list.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = list[i].ID;
                obj["text"] = list[i].RoleName;
                array.Add(obj);
            }
            return array;
        }

        /// <summary>
        /// 根据“用户”数据生成DataList格式的Json数据
        /// </summary>
        /// <returns></returns>
        public JArray GetUser()
        {
            List<Users> list = ServiceFactory.Factory.UsersService.Search(null);
            JArray array = new JArray();
            for (int i = 0; i < list.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = list[i].ID;
                if (list[i].UserName == null || list[i].UserName == string.Empty)
                    obj["text"] = list[i].UserID;
                else
                    obj["text"] = list[i].UserID + "-" + list[i].UserName;
                array.Add(obj);
            }
            return array;
        }
    }
}
