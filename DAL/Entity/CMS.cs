using DAL.Data;
using MyOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;

namespace DAL
{
    [Serializable]
    [Table("CMS_Class")]
    [Description("内容分类数据表")]
    public class Classes : EntityBase
    {
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string UpperClassCode { get; set; }
        [ForeignType(typeof(Classes))]
        public string UpperClassID { get; set; }
        public string Description { get; set; }
        public string LinkUrl { get; set; }
        public int? Sequence { get; set; }
   
    }

    [Serializable]
    [Description("内容分类数据视图")]
    public class ClassesView : Classes
    {
        [ForeignColumn(typeof(Classes), Property = "ClassName")]
        public string UpperClassName { get; set; }
    }

    [Serializable]
    [Table("CMS_Article")]
    [Description("内容数据表")]
    public class Articles : BusinessEntity
    {
        [ForeignType(typeof(Classes))]
        public int? ClassID { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string PicPath { get; set; }
        public string JpgPath { get; set; }
        public string CutPath { get; set; }
        [Column("ArticleContent", Length = Int32.MaxValue)]
        public string ArticleContent { get; set; }
        [Column("SimpleContent", Length = Int32.MaxValue)]
        public string SimpleContent { get; set; }
        [Column("ClientContent", Length = Int32.MaxValue)]
        public string ClientContent { get; set; }
        public string ArticleSource { get; set; }
        public string Author { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishTime { get; set; }
        public bool? TopMost { get; set; }
        public int? AnsCounter { get; set; }
        public int? HitCounter { get; set; }
        public bool? AllowDiscuss { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
    }


    [Serializable]
    [Description("文章数据视图")]
    public class ArticlesView : Articles
    {
        [ForeignColumn(typeof(Classes))]
        public string ClassName { get; set; }
        [ForeignColumn(typeof(Classes))]
        public string ClassCode { get; set; }
 

    }

    [Serializable]
    [Table("CMS_Config")]
    [Description("输入表单配置信息")]
    public class Configs : EntityBase
    {
        [ForeignType(typeof(Modules))]
        public int? ModuleID { get; set; }
        public bool TitleFlag { get; set; }
        public string TitleCaption { get; set; }
        public bool ShortTitleFlag { get; set; }
        public string ShortTitleCaption { get; set; }
        public bool PicPathFlag { get; set; }
        public bool ArticleContentFlag { get; set; }
        public string ArticleContentCaption { get; set; }
        public bool DescriptionFlag { get; set; }
        public string DescriptionCaption { get; set; }
        public bool CutPathFlag { get; set; }
        public bool AuthorFlag { get; set; }
        public string AuthorCaption { get; set; }
        public bool KeywordsFlag { get; set; }
        public string KeywordsCaption { get; set; }
        public bool PublishFlag { get; set; }
        public bool PublishTimeFlag { get; set; }
        public string PublishTimeCaption { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }

    [Serializable]
    [Description("输入表单配置数据视图")]
    public class ConfigsView : Configs
    {
        [ForeignColumn(typeof(Modules))]
        public string DisplayName { get; set; }
    }

    [Serializable]
    [Table("CMS_Character")]
    [Description("卡通人物表")]
    public class Character : BusinessEntity
    {       
        public string Name { get; set; }
        [Column("Introduce", Length = Int32.MaxValue)]
        public string Introduce { get; set; }        
        public string ExtendMean { get; set; }
        public string RepresentVal { get; set; }
        public int Sequence { get; set; }
    }

    [Serializable]
    [Table("CMS_Contact")]
    [Description("联系方式表")]
    public class Contact : BusinessEntity
    {
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
    }

    [Serializable]
    [Table("CMS_Product")]
    [Description("产品表")]
    public class Product : BusinessEntity
    {
        public string ClassCode { get; set; }
        public string Parameter { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime? PublishTime { get; set; }
    }
}