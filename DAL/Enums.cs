using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 错误码定义
    /// </summary>
    public enum Enum_StatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumDisplayName("成功")]
        Success = 0,

        /// <summary>
        /// 必填项为空
        /// </summary>
        [EnumDisplayName("必填项为空")]
        Error_Required = 1001,

        /// <summary>
        /// 日期格式错误
        /// </summary>
        [EnumDisplayName("日期格式错误")]
        Error_DateFormatError = 1002,

        /// <summary>
        /// 数值格式错误
        /// </summary>
        [EnumDisplayName("数值格式错误")]
        Error_NumberFormatError = 1003,

        /// <summary>
        /// 数据不存在
        /// </summary>
        [EnumDisplayName("数据不存在")]
        Error_DataNotExist = 1004,

        /// <summary>
        /// 文件不存在
        /// </summary>
        [EnumDisplayName("文件不存在")]
        Error_FileNotExist = 1005,

        /// <summary>
        /// 未登录
        /// </summary>
        [EnumDisplayName("未登录")]
        Error_NoLogin = 2001,

        /// <summary>
        /// 超期
        /// </summary>
        [EnumDisplayName("超期")]
        Error_Expired = 3001,

        /// <summary>
        /// 签名错误
        /// </summary>
        [EnumDisplayName("签名错误")]
        Error_Token = 3002,

        /// <summary>
        /// 文件转换失败
        /// </summary>
        [EnumDisplayName("文件转换失败")]
        Error_FileParseFailed = 9005,
        /// <summary>
        /// 文件上传失败
        /// </summary>
        [EnumDisplayName("文件上传失败")]
        Error_FileUploadFailed = 9006,


        /// <summary>
        /// 访问被拒绝
        /// </summary>
        [EnumDisplayName("访问被拒绝")]
        Error_AccessDeny = 9007,

        /// <summary>
        /// 请求参数异常
        /// </summary>
        [EnumDisplayName("请求参数异常")]
        Error_Request = 9008,

        /// <summary>
        /// 失败
        /// </summary>
        [EnumDisplayName("失败")]
        Fail = 9009
    }

    /// <summary>
    /// 数据结构（适配）属性
    /// </summary>
    public enum Enum_Adapter
    {
        /// <summary>
        /// 无
        /// </summary>
        [EnumDisplayName("无")]
        None = 1,

        /// <summary>
        /// 树形列表结构
        /// </summary>
        [EnumDisplayName("树形列表结构")]
        TreeGrid = 2,

        /// <summary>
        /// 树形下拉框结构
        /// </summary>
        [EnumDisplayName("树形下拉框结构")]
        ComboTree = 3,

        /// <summary>
        /// 数据列表结构
        /// </summary>
        [EnumDisplayName("数据列表结构")]
        DataList = 4,

        /// <summary>
        /// 树形结构
        /// </summary>
        [EnumDisplayName("树形结构")]
        Tree = 5,

        /// <summary>
        /// 下拉框
        /// </summary>
        [EnumDisplayName("下拉框")]
        Combobox = 6
    }



    /// <summary>
    /// 性别
    /// </summary>
    public enum Enum_Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        [EnumDisplayName("男")]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [EnumDisplayName("女")]
        Female = 2
    }
    /// <summary>
    /// 期刊分类
    /// </summary>
    public enum Enum_CategroyType
    {
        /// <summary>
        /// 学术年谱
        /// </summary>
        [EnumDisplayName("学术年谱")]
        XSNP = 1,
        /// <summary>
        /// 东吴讲堂
        /// </summary>
        [EnumDisplayName("东吴讲堂")]
        DWJT = 2,
        /// <summary>
        /// 双语经典
        /// </summary>
        [EnumDisplayName("双语经典")]
        SYJD = 3,
        /// <summary>
        /// 哲学与文化
        /// </summary>
        [EnumDisplayName("哲学与文化")]
        ZXWH = 4,
        /// <summary>
        /// 文学翻译论坛
        /// </summary>
        [EnumDisplayName("文学翻译论坛")]
        WXFY = 5,
        /// <summary>
        /// 诗学
        /// </summary>
        [EnumDisplayName("诗学")]
        SX = 6,
        /// <summary>
        /// 学术史研究
        /// </summary>
        [EnumDisplayName("学术史研究")]
        XSSYJ = 7,
        /// <summary>
        /// 随笔与书评
        /// </summary>
        [EnumDisplayName("随笔与书评")]
        SBYSP = 8,
        /// <summary>
        /// 现代中国文学
        /// </summary>
        [EnumDisplayName("现代中国文学")]
        XDZGWX = 9,
        /// <summary>
        /// 历史学
        /// </summary>
        [EnumDisplayName("历史学")]
        LSX = 10,
        /// <summary>
        /// 中国文学
        /// </summary>
        [EnumDisplayName("中国文学")]
        ZGWX = 11,
        /// <summary>
        /// 世界文学
        /// </summary>
        [EnumDisplayName("世界文学")]
        SJWX = 12,
        /// <summary>
        /// 东吴研究
        /// </summary>
        [EnumDisplayName("东吴研究")]
        DWYJ = 13,
        /// <summary>
        /// 论点摘编
        /// </summary>
        [EnumDisplayName("论点摘编")]
        LDZB = 14,
        /// <summary>
        /// 范小青《桂香街》评论
        /// </summary>
        [EnumDisplayName("范小青《桂香街》评论")]
        GHJ = 15,
        /// <summary>
        /// 散文研究
        /// </summary>
        [EnumDisplayName("散文研究")]
        SWYJ = 16
    }

}
