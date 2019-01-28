using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Business;
using Models;
using MyOrm.Common;
using Newtonsoft.Json.Linq;
using Utilities;



namespace WebApp.Api
{
    public class WebController : BaseController
    {
        [AllowAnonymous]
        public JsonResult GetClass(string classCode)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Classes entity = ServiceFactory.Factory.ClassesService.SearchOne(new SimpleCondition("ClassCode", classCode));
                json.Data = JsonUtil.GetSuccessForObject(entity);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        [AllowAnonymous]
        public JsonResult SearchArticle(string classCode, string keyword, int? pageNumber, int? pageSize, string sortColumn, string sortMode,string searchMode)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                IArticlesService service = ServiceFactory.Factory.ArticlesService;
                ConditionSet condition = new ConditionSet();
                condition.Add(new SimpleCondition("IsPublished", true));
                if (classCode != null && classCode != "")
                {
                    if (classCode.Length == 3)
                    {
                        condition.Add(new SimpleCondition("ClassCode",ConditionOperator.Like, classCode+"%"));
                    }
                    else if (classCode.Length > 10) {
                        ConditionSet __condition = new ConditionSet(ConditionJoinType.Or);
                        string[] classCodeArray = classCode.Split('$');
                        for (int i = 0; i < classCodeArray.Length; i++) {
                            if (classCodeArray[i].Length == 3)
                            {
                                __condition.Add(new SimpleCondition("ClassCode", ConditionOperator.Like, classCodeArray[i] + "%"));
                            }
                            else {
                                __condition.Add(new SimpleCondition("ClassCode",classCodeArray[i]));
                            }
                        }
                        condition.Add(__condition);
                    }
                    else
                    {
                        condition.Add(new SimpleCondition("ClassCode", classCode));
                    }
                }
                if (keyword != null && keyword != string.Empty)
                {
                    ConditionSet _condition = new ConditionSet(ConditionJoinType.Or);

                    if (searchMode == "accurate")
                    {
                        _condition.Add(new SimpleCondition("Keywords", ConditionOperator.Like, keyword));
                    }
                    else
                    {
                        _condition.Add(new SimpleCondition("Title", ConditionOperator.Like, "%"+keyword+"%"));
                        _condition.Add(new SimpleCondition("ArticleContent", ConditionOperator.Like, "%"+keyword+"%"));
                        _condition.Add(new SimpleCondition("Keywords", ConditionOperator.Like, "%"+keyword+"%"));
                    }
                    condition.Add(_condition);
                }
                if (pageNumber != null && pageSize != null)
                {
                    int totalCount = service.Count(condition);
                    List<ArticlesView> list = service.SearchSection(condition, new SectionSet()
                    {
                        Orders = new Sorting[]{
                            new Sorting(){
                                Direction=sortMode.ToUpper()=="DESC"?ListSortDirection.Descending:ListSortDirection.Ascending,
                                PropertyName=sortColumn
                            }
                        },
                        SectionSize = (int)pageSize,
                        StartIndex = ((int)pageNumber - 1) * (int)pageSize
                    });
                    json.Data = JsonUtil.GetSuccessForObject(list, totalCount);
                }
                else
                {
                    List<ArticlesView> list = service.SearchWithOrder(condition, new Sorting[]{
                        new Sorting(){
                            Direction=sortMode.ToUpper()=="DESC"?ListSortDirection.Descending:ListSortDirection.Ascending,
                            PropertyName=sortColumn
                        }
                    });
                    json.Data = JsonUtil.GetSuccessForObject(list);
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }



        [AllowAnonymous]
        public JsonResult GetArticle(int id)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Articles article = ServiceFactory.Factory.ArticlesService.SearchOne(new SimpleCondition("ID", id));
                json.Data = JsonUtil.GetSuccessForObject(article);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        [AllowAnonymous]
        public JsonResult GetCharacter()
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                List<Character> body = ServiceFactory.Factory.CharacterService.SearchSection(null, new SectionSet()
                {
                    Orders = new Sorting[]{
                            new Sorting(){
                                Direction=ListSortDirection.Ascending,
                                PropertyName="Sequence"
                            }
                        },
                    SectionSize = 5,
                    StartIndex = 0
                });
                json.Data = JsonUtil.GetSuccessForObject(body);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        [AllowAnonymous]
        public JsonResult GetContact()
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Contact body = ServiceFactory.Factory.ContactService.SearchOne(null);
                json.Data = JsonUtil.GetSuccessForObject(body);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }


        [AllowAnonymous]
        public JsonResult GetConfig(int ModuleID)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                Configs config = ServiceFactory.Factory.ConfigsService.SearchOne(new SimpleCondition("ModuleID", ModuleID));
                json.Data = JsonUtil.GetSuccessForObject(config);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }
    }
}