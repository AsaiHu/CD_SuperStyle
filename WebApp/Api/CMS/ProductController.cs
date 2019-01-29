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
    public class ProductController : BaseController
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
                    IProductService service = ServiceFactory.Factory.ProductService;
                    ConditionSet condition = new ConditionSet();
                    condition.Add(new SimpleCondition("ClassCode", classCode));
                    int totalCount = service.Count(condition);
                    List<Product> list = service.SearchSection(condition, ((int)pageNumber - 1) * (int)pageSize, (int)pageSize, "PublishTime", System.ComponentModel.ListSortDirection.Descending);
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
                IProductService service = ServiceFactory.Factory.ProductService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                Product Product = service.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(Product);
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
                json.Data = ServiceFactory.Factory.ProductService.DeleteID(id) ? JsonUtil.GetSuccessForString("删除已成功") : JsonUtil.GetFailForString("删除失败");
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Save(Product entity)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IProductService service = ServiceFactory.Factory.ProductService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", entity.ID));
                //查询数据
                Product old_entity = service.SearchOne(condition);
                if (old_entity == null)
                {
                    Product Product = new Product();
                    Product.ClassCode = entity.ClassCode;
                    Product.Name = entity.Name;
                    Product.Parameter = entity.Parameter;
                    Product.ImgUrl = entity.ImgUrl;
                    try
                    {
                        if (entity.PublishTime == null)
                        {
                            Product.PublishTime = DateTime.Now;
                        }
                        else
                        {
                            Product.PublishTime = Convert.ToDateTime(entity.PublishTime);
                        }
                    }
                    catch
                    {
                        Product.PublishTime = DateTime.Now;
                    }
                    Product.CreateIP = Network.ClientIP;
                    Product.CreateTime = DateTime.Now;
                    Product.Creator = Security.CurrentUser.UserName;
                    Product.UpdateIP = Network.ClientIP;
                    Product.UpdateTime = DateTime.Now;
                    Product.Updater = Security.CurrentUser.UserName;
                    service.Insert(Product);


                    json.Data = JsonUtil.GetSuccessForString("新增已完成");
                }
                else
                {
                    old_entity.ClassCode = entity.ClassCode;
                    old_entity.Parameter = entity.Parameter;
                    old_entity.Name = entity.Name;
                    old_entity.ImgUrl = entity.ImgUrl;
                    try
                    {
                        if (entity.PublishTime == null)
                        {
                            old_entity.PublishTime = DateTime.Now;
                        }
                        else
                        {
                            old_entity.PublishTime = Convert.ToDateTime(entity.PublishTime);
                        }
                    }
                    catch
                    {
                        old_entity.PublishTime = DateTime.Now;
                    }
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