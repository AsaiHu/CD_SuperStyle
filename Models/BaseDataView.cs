using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class BaseDataView
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }
}
