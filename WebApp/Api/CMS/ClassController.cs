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
using log4net;

namespace WebApp.Api.CMS
{
    public class ClassController : BaseController
    {
        public JsonResult Query(string adapter)
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

                }
                else if (data_adapter == Enum_Adapter.Tree)
                {
                    JArray array = new TreeAdapter().GetClasses();
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
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                Classes entity = ServiceFactory.Factory.ClassesService.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(entity);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult GetByCode(string classCode)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ClassCode", classCode));
                Classes entity = ServiceFactory.Factory.ClassesService.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(entity);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }
    }
}