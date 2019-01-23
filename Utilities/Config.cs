using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Config
    {
        /// <summary>
        /// 上传文件的最大长度
        /// </summary>
        public static int MaxLength
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["MaxLength"].ToString().Trim()) * 1024; }
        }

        /// <summary>
        /// pdf2swf转换工具路径
        /// </summary>
        public static string Pdf2SwfPath
        {
            get { return ConfigurationManager.AppSettings["Pdf2SwfPath"].ToString().Trim(); }
        }
    }
}
