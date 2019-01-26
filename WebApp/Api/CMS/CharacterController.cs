using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Business;
using Models;
using MyOrm.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Utilities;
using Adapters;
using System.Data;

namespace WebApp.Api
{
    public class CharacterController : BaseController
    {
        public JsonResult Query(string classCode, string keyword, string adapter, int? pageNumber, int? pageSize)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Enum_Adapter data_adapter = Enum_Adapter.None;
                if (adapter != null)
                    data_adapter = (Enum_Adapter)Convert.ToInt16(adapter);

                if (data_adapter == Enum_Adapter.None)
                {
                    ICharacterService service = ServiceFactory.Factory.CharacterService;
                    ConditionSet condition = new ConditionSet();
                  
                    int totalCount = service.Count(condition);
                    List<Character> list = service.SearchSection(condition, ((int)pageNumber - 1) * (int)pageSize, (int)pageSize, "Sequence", System.ComponentModel.ListSortDirection.Ascending);
                    json.Data = JsonUtil.GetSuccessForObject(list, totalCount);
                }
                else if (data_adapter == Enum_Adapter.DataList)
                {
                    JArray array = new DataListAdapter().GetUser();
                    json.Data = JsonUtil.GetSuccessForObject(array);
                }
                else
                {
                    json.Data = JsonUtil.GetFailForString("数据加载失败");
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Get(int id)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                ICharacterService service = ServiceFactory.Factory.CharacterService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                Character Character = service.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(Character);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Delete(int id)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                json.Data = ServiceFactory.Factory.CharacterService.DeleteID(id) ? JsonUtil.GetSuccessForString("删除已成功") : JsonUtil.GetFailForString("删除失败");
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Save(Character entity)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                ICharacterService service = ServiceFactory.Factory.CharacterService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", entity.ID));
                //查询数据
                Character old_entity = service.SearchOne(condition);
                if (old_entity == null)
                {
                    Character Character = new Character();
                    Character.Name = entity.Name;
                    Character.Introduce = entity.Introduce;
                    Character.ExtendMean = entity.ExtendMean;
                    Character.RepresentVal = entity.RepresentVal;
                    Character.Sequence = entity.Sequence;
                    Character.CreateIP = Network.ClientIP;
                    Character.CreateTime = DateTime.Now;
                    Character.Creator = Security.CurrentUser.UserName;
                    Character.UpdateIP = Network.ClientIP;
                    Character.UpdateTime = DateTime.Now;
                    Character.Updater = Security.CurrentUser.UserName;
                    service.Insert(Character);


                    json.Data = JsonUtil.GetSuccessForString("新增已完成");
                }
                else
                {
                    old_entity.Name = entity.Name;
                    old_entity.Introduce = entity.Introduce;
                    old_entity.ExtendMean = entity.ExtendMean;
                    old_entity.RepresentVal = entity.RepresentVal;
                    old_entity.Sequence = entity.Sequence;
                    old_entity.UpdateIP = Network.ClientIP;
                    old_entity.UpdateTime = DateTime.Now;
                    old_entity.Updater = Security.CurrentUser.UserName;
                    service.Update(old_entity);
                    json.Data = JsonUtil.GetSuccessForString("修改已完成");
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);
            }
            return json;
        }
    }
}