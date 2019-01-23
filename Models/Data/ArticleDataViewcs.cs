using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data
{
    public class ArticleDataViewcs
    {
        public int? ID { get; set; }
        public int? ClassID { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string PicPath { get; set; }
        public string JpgPath { get; set; }
        public string CutPath { get; set; }
        public string ArticleContent { get; set; }
        public string SimpleContent { get; set; }
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
}
