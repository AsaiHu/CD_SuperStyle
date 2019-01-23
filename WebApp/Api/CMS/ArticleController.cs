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
    public class ArticleController : BaseController
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
                    IArticlesService service = ServiceFactory.Factory.ArticlesService;
                    ConditionSet condition = new ConditionSet();
                    condition.Add(new SimpleCondition("ClassCode", classCode));
                    if (classCode != null && classCode != string.Empty)
                    {
                        ConditionSet _condition = new ConditionSet(ConditionJoinType.Or);
                        _condition.Add(new SimpleCondition("Title", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("ShortTitle", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("ArticleContent", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("SimpleContent", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("ClientContent", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("ArticleSource", ConditionOperator.Like, "%" + keyword + "%"));
                        _condition.Add(new SimpleCondition("PublishTime", ConditionOperator.Like, "%" + keyword + "%"));
                        condition.Add(_condition);
                    }
                    int totalCount = service.Count(condition);
                    List<ArticlesView> list = service.SearchSection(condition, ((int)pageNumber - 1) * (int)pageSize, (int)pageSize, "PublishTime", System.ComponentModel.ListSortDirection.Descending);
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
                IArticlesService service = ServiceFactory.Factory.ArticlesService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                Articles article = service.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(article);
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
                json.Data = ServiceFactory.Factory.ArticlesService.DeleteID(id) ? JsonUtil.GetSuccessForString("删除已成功") : JsonUtil.GetFailForString("删除失败");
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Save(Articles entity)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IArticlesService service = ServiceFactory.Factory.ArticlesService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", entity.ID));
                //查询数据
                Articles old_entity = service.SearchOne(condition);
                if (old_entity == null)
                {
                    Articles article = new Articles();
                    article.Title = entity.Title;
                    article.ShortTitle = entity.ShortTitle;
                    article.ClassID = entity.ClassID;
                    if (entity.PicPath != "")
                    {
                        article.PicPath = entity.PicPath;
                    }
                    else
                    {
                        article.PicPath = null;
                    }
                    if (entity.CutPath != "")
                    {
                        article.CutPath = entity.CutPath;
                    }
                    else
                    {
                        article.CutPath = null;
                    }
                    article.ArticleContent = entity.ArticleContent;
                    article.ArticleSource = entity.ArticleSource;
                    if (entity.Author != null && entity.Author != string.Empty)
                    {
                        article.Author = entity.Author;
                    }
                    else {
                        article.Author = Security.CurrentUser.UserName;
                    }
                    article.IsPublished = entity.IsPublished;
                    try
                    {
                        if (entity.PublishTime == null)
                        {
                            article.PublishTime = DateTime.Now;
                        }
                        else
                        {
                            article.PublishTime = Convert.ToDateTime(entity.PublishTime);
                        }
                    }
                    catch
                    {
                        article.PublishTime = DateTime.Now;
                    }
                    article.TopMost = false;
                    article.AnsCounter = 0;
                    article.HitCounter = 0;
                    article.AllowDiscuss = false;
                    article.Keywords = entity.Keywords;
                    article.Description = entity.Description;
                    article.CreateIP = Network.ClientIP;
                    article.CreateTime = DateTime.Now;
                    article.Creator = Security.CurrentUser.UserName;
                    article.UpdateIP = Network.ClientIP;
                    article.UpdateTime = DateTime.Now;
                    article.Updater = Security.CurrentUser.UserName;
                    service.Insert(article);


                    json.Data = JsonUtil.GetSuccessForString("新增已完成");
                }
                else
                {

                    old_entity.Title = entity.Title;
                    old_entity.ShortTitle = entity.ShortTitle;
                    old_entity.ClassID = entity.ClassID;
                    if (entity.PicPath != "")
                    {
                        old_entity.PicPath = entity.PicPath;
                    }
                    else
                    {
                        old_entity.PicPath = null;

                    }
                    if (entity.CutPath != "")
                    {
                        old_entity.CutPath = entity.CutPath;
                    }
                    else
                    {
                        old_entity.CutPath = null;
                    }
                    old_entity.ArticleContent = entity.ArticleContent;
                    old_entity.ArticleSource = entity.ArticleSource;
                    if (entity.Author != null && entity.Author != string.Empty)
                    {
                        old_entity.Author = entity.Author;
                    }
                    else
                    {
                        old_entity.Author = Security.CurrentUser.UserName;
                    }
                    old_entity.IsPublished = entity.IsPublished;
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
                    old_entity.TopMost = false;
                    old_entity.AnsCounter = 0;
                    old_entity.HitCounter = 0;
                    old_entity.AllowDiscuss = false;
                    old_entity.Keywords = entity.Keywords;
                    old_entity.Description = entity.Description;
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