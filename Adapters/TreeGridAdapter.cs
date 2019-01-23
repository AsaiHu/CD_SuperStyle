using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Data;
using DAL.Business;
using DAL;
using MyOrm.Common;

namespace Adapters
{
    public class TreeGridAdapter
    {
        /// <summary>
        /// 根据“模块”数据生成TreeGrid格式的Json数据
        /// </summary>
        /// <returns></returns>
        public JArray GetModule()
        {
            DataView dv = ServiceFactory.Factory.ModulesService.GetAll().DefaultView;

            JArray array = new JArray();
            dv.RowFilter = "UpperModuleID is null";
            dv.Sort = "Sequence";
            for (int i = 0; i < dv.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = dv[i]["ID"].ToString().Trim();
                obj["name"] = dv[i]["DisplayName"].ToString().Trim();
                obj["src"] = dv[i]["Src"].ToString().Trim();
                obj["sequence"] = dv[i]["Sequence"].ToString().Trim();
                obj["visible"] = string.Empty;
                obj["add"] = string.Empty;
                obj["update"] = string.Empty;
                obj["delete"] = string.Empty;
                obj["submit"] = string.Empty;
                obj["query"] = string.Empty;
                obj["updatetime"] = Convert.ToDateTime(dv[i]["UpdateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                obj["iconCls"] = dv[i]["ImageUrl"].ToString().Trim();
                array.Add(obj);

                this.GetModule(ref array, ref dv, dv[i]["ID"].ToString().Trim());

                dv.RowFilter = "UpperModuleID is null";
                dv.Sort = "Sequence";
            }
            return array;
        }

        private void GetModule(ref JArray _json_array, ref DataView _dv, string _pid)
        {
            _dv.RowFilter = "UpperModuleID='" + _pid + "'";
            _dv.Sort = "Sequence";
            for (int i = 0; i < _dv.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = _dv[i]["ID"].ToString().Trim();
                obj["name"] = _dv[i]["DisplayName"].ToString().Trim();
                obj["src"] = _dv[i]["Src"].ToString().Trim();
                obj["sequence"] = _dv[i]["Sequence"].ToString().Trim();
                obj["visible"] = Convert.ToBoolean(_dv[i]["Hide"]) ? "0" : "1";
                obj["add"] = Convert.ToBoolean(_dv[i]["AddFlag"]) ? "1" : "0";
                obj["update"] = Convert.ToBoolean(_dv[i]["UpdateFlag"]) ? "1" : "0";
                obj["delete"] = Convert.ToBoolean(_dv[i]["DeleteFlag"]) ? "1" : "0";
                obj["submit"] = Convert.ToBoolean(_dv[i]["SubmitFlag"]) ? "1" : "0";
                obj["query"] = Convert.ToBoolean(_dv[i]["QueryFlag"]) ? "1" : "0";
                obj["updatetime"] = Convert.ToDateTime(_dv[i]["UpdateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                obj["iconCls"] = _dv[i]["ImageUrl"].ToString().Trim();
                obj["_parentId"] = _pid;
                _json_array.Add(obj);

                this.GetModule(ref _json_array, ref _dv, _dv[i]["ID"].ToString().Trim());

                _dv.RowFilter = "UpperModuleID='" + _pid + "'";
                _dv.Sort = "Sequence";
            }
        }
    }
}
