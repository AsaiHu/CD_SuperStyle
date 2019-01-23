﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.Business;
using Newtonsoft.Json.Linq;
using DAL;
using MyOrm.Common;

namespace Adapters
{
    public class ComboTreeAdapter
    {
        /// <summary>
        /// 根据“模块”数据生成ComboTree格式的Json数据
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
                obj["text"] = dv[i]["DisplayName"].ToString().Trim();
                JArray _array = this.GetModule(ref dv, dv[i]["ID"].ToString().Trim());
                if (_array.Count > 0)
                {
                    obj["children"] = _array;
                }
                array.Add(obj);

                dv.RowFilter = "UpperModuleID is null";
                dv.Sort = "Sequence";
            }
            return array;
        }

        private JArray GetModule(ref DataView _dv, string _pid)
        {
            JArray array = new JArray();

            _dv.RowFilter = "UpperModuleID='" + _pid + "'";
            _dv.Sort = "Sequence";
            for (int i = 0; i < _dv.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = _dv[i]["ID"].ToString().Trim();
                obj["text"] = _dv[i]["DisplayName"].ToString().Trim();
                JArray _array = this.GetModule(ref _dv, _dv[i]["ID"].ToString().Trim());
                if (_array.Count > 0)
                {
                    obj["children"] = _array;
                }
                array.Add(obj);

                _dv.RowFilter = "UpperModuleID='" + _pid + "'";
                _dv.Sort = "Sequence";
            }
            return array;
        }

    }
}
