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
    public class ContactController : BaseController
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
                    IContactService service = ServiceFactory.Factory.ContactService;
                    ConditionSet condition = new ConditionSet();
                  
                    int totalCount = service.Count(condition);
                    List<Contact> list = service.SearchSection(condition, ((int)pageNumber - 1) * (int)pageSize, (int)pageSize, "ID", System.ComponentModel.ListSortDirection.Descending);
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
                IContactService service = ServiceFactory.Factory.ContactService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                Contact Contact = service.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(Contact);
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
                json.Data = ServiceFactory.Factory.ContactService.DeleteID(id) ? JsonUtil.GetSuccessForString("删除已成功") : JsonUtil.GetFailForString("删除失败");
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Save(Contact entity)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IContactService service = ServiceFactory.Factory.ContactService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", entity.ID));
                //查询数据
                Contact old_entity = service.SearchOne(condition);
                if (old_entity == null)
                {
                    Contact Contact = new Contact();
                    Contact.Telephone = entity.Telephone;
                    Contact.MobilePhone = entity.MobilePhone;
                    Contact.Fax = entity.Fax;
                    Contact.Address = entity.Address;
                    Contact.CreateIP = Network.ClientIP;
                    Contact.CreateTime = DateTime.Now;
                    Contact.Creator = Security.CurrentUser.UserName;
                    Contact.UpdateIP = Network.ClientIP;
                    Contact.UpdateTime = DateTime.Now;
                    Contact.Updater = Security.CurrentUser.UserName;
                    service.Insert(Contact);


                    json.Data = JsonUtil.GetSuccessForString("新增已完成");
                }
                else
                {
                    old_entity.Telephone = entity.Telephone;
                    old_entity.MobilePhone = entity.MobilePhone;
                    old_entity.Fax = entity.Fax;
                    old_entity.Address = entity.Address;
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