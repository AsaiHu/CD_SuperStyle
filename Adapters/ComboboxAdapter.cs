using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using DAL;
using DAL.Business;
using MyOrm.Common;
using System.Data;

namespace Adapters
{
    public class ComboboxAdapter
    {
        public JArray GetEnum(Type enumType)
        {
            DataView dv = DAL.Util.GetAll(enumType).DefaultView;

            JArray array = new JArray();
            for (int i = 0; i < dv.Count; i++)
            {
                JObject obj = new JObject();
                obj["id"] = dv[i]["Value"].ToString().Trim();
                obj["text"] = dv[i]["String"].ToString().Trim();
                array.Add(obj);
            }
            return array;
        }
    }
}
