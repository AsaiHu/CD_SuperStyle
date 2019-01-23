using Adapters;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;
using Newtonsoft.Json.Linq;

namespace WebApp.Api.Global
{
    public class EnumController : BaseController
    {
        public JsonResult Query(string enumType)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";
            try
            {
                if (enumType.IndexOf("|") < 0)
                {
                    if (enumType.ToUpper() == "Adapter".ToUpper())
                    {
                        json.Data = JsonUtil.GetSuccessForObject(new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Adapter)));
                    }
                    else if (enumType.ToUpper() == "Sex".ToUpper())
                    {
                        json.Data = JsonUtil.GetSuccessForObject(new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Sex)));
                    }
                    else if (enumType.ToUpper() == "CategroyType".ToUpper())
                    {
                        json.Data = JsonUtil.GetSuccessForObject(new ComboboxAdapter().GetEnum(typeof(DAL.Enum_CategroyType)));
                    }

                    else
                    {
                        json.Data = JsonUtil.GetFail(DAL.Enum_StatusCode.Error_DataNotExist);
                    }
                }
                else
                {
                    JObject data = new JObject();
                    string[] _enumType = enumType.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < _enumType.Length; i++)
                    {
                        if (_enumType[i].ToUpper() == "Adapter".ToUpper())
                        {
                            data[_enumType[i]] = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Adapter));
                        }
                        else if (_enumType[i].ToUpper() == "Sex".ToUpper())
                        {
                            data[_enumType[i]] = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Sex));
                        }
                        else if (_enumType[i].ToUpper() == "CategroyType".ToUpper())
                        {
                            data[_enumType[i]] = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_CategroyType));
                        }
                    }
                    json.Data = JsonUtil.GetSuccessForObject(data);
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);
            }
            return json;
        }

        [AllowAnonymous]
        public JsonResult Bind(string enumType, string ticket)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";
            try
            {
                if (!new RequestAuthorizeAttribute().ValidateTicket(ticket))
                    throw new Exception("访问被拒绝");

                if (enumType.ToUpper() == "Adapter".ToUpper())
                {
                    json.Data = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Adapter));
                }
                else if (enumType.ToUpper() == "Sex".ToUpper())
                {
                    json.Data = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_Sex));
                }
                else if (enumType.ToUpper() == "CategroyType".ToUpper())
                {
                    json.Data = new ComboboxAdapter().GetEnum(typeof(DAL.Enum_CategroyType));
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