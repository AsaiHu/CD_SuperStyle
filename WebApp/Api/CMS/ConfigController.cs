using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Business;
using Models;
using MyOrm.Common;
using Newtonsoft.Json.Linq;
using Utilities;
using Adapters;

namespace WebApp.Api
{
    public class ConfigController : BaseController
    {
        public JsonResult Query(string keyword,int? pageNumber, int? pageSize)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IConfigsService service = ServiceFactory.Factory.ConfigsService;
                ConditionSet condition = new ConditionSet();
                if (keyword != null && keyword != string.Empty)
                {
                    ConditionSet _condition = new ConditionSet(ConditionJoinType.Or);
                    _condition.Add(new SimpleCondition("DisplayName", ConditionOperator.Like, "%" + keyword + "%"));
                    condition.Add(_condition);
                }
                int totalCount = service.Count(condition);
                List<ConfigsView> configs = service.SearchSection(condition, ((int)pageNumber - 1) * (int)pageSize, (int)pageSize, "ID", System.ComponentModel.ListSortDirection.Descending);

                json.Data = JsonUtil.GetSuccessForObject(configs, totalCount);
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
                IConfigsService service = ServiceFactory.Factory.ConfigsService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("ID", id));
                //查询数据
                Configs role = service.SearchOne(condition);
                json.Data = JsonUtil.GetSuccessForObject(role);
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
                json.Data = ServiceFactory.Factory.ConfigsService.DeleteID(id) ? JsonUtil.GetSuccessForString("删除已成功") : JsonUtil.GetFailForString("删除失败");
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult Save(Configs r)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IConfigsService service = ServiceFactory.Factory.ConfigsService;
                Configs config = ServiceFactory.Factory.ConfigsService.SearchOne(new SimpleCondition("ID", r.ID));
                if (config == null)
                {
                    Configs _r = new Configs();
                    _r.ModuleID = r.ModuleID;
                    _r.TitleFlag = r.TitleFlag;
                    _r.ShortTitleFlag = r.ShortTitleFlag;
                    _r.PicPathFlag = r.PicPathFlag;
                    _r.ArticleContentFlag = r.ArticleContentFlag;
                    _r.DescriptionFlag = r.DescriptionFlag;
                    _r.CutPathFlag = r.CutPathFlag;
                    _r.AuthorFlag = r.AuthorFlag;
                    _r.KeywordsFlag = r.KeywordsFlag;
                    _r.PublishFlag = r.PublishFlag;
                    _r.PublishTimeFlag = r.PublishTimeFlag;
                    _r.TitleCaption = r.TitleCaption;
                    _r.ShortTitleCaption = r.ShortTitleCaption;
                    _r.ArticleContentFlag = r.ArticleContentFlag;
                    _r.DescriptionCaption = r.DescriptionCaption;
                    _r.AuthorCaption = r.AuthorCaption;
                    _r.KeywordsCaption = r.KeywordsCaption;
                    _r.PublishTimeCaption = r.PublishTimeCaption;
                    _r.Width = r.Width;
                    _r.Height = r.Height;
                    
                    service.Insert(_r);
                    json.Data = JsonUtil.GetSuccessForString("新增已完成");
                }
                else
                {
                    config.ModuleID = r.ModuleID;
                    config.TitleFlag = r.TitleFlag;
                    config.ShortTitleFlag = r.ShortTitleFlag;
                    config.PicPathFlag = r.PicPathFlag;
                    config.ArticleContentFlag = r.ArticleContentFlag;
                    config.DescriptionFlag = r.DescriptionFlag;
                    config.CutPathFlag = r.CutPathFlag;
                    config.AuthorFlag = r.AuthorFlag;
                    config.KeywordsFlag = r.KeywordsFlag;
                    config.PublishFlag = r.PublishFlag;
                    config.PublishTimeFlag = r.PublishTimeFlag;
                    config.TitleCaption = r.TitleCaption;
                    config.ShortTitleCaption = r.ShortTitleCaption;
                    config.ArticleContentFlag = r.ArticleContentFlag;
                    config.DescriptionCaption = r.DescriptionCaption;
                    config.AuthorCaption = r.AuthorCaption;
                    config.KeywordsCaption = r.KeywordsCaption;
                    config.PublishTimeCaption = r.PublishTimeCaption;
                    config.Width = r.Width;
                    config.Height = r.Height;
                    service.Update(config);
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