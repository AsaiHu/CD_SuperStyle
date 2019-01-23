using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class HTML
    {
        #region   EraseHTML
        /// <summary>
        /// 移除所有的html标签
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="eraseAll"></param>
        /// <returns></returns>
        public static string EraseHTML(string strHtml, bool eraseAll)
        {

            string html = strHtml;

            if (eraseAll)
            {
                html = Regex.Replace(html, @"<!--[\s\S]*?-->", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*tr[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*p[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*br[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*div[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<STYLE[\s\S]*?</STYLE>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<iframe[\s\S]*?</iframe>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<option[\s\S]*?</option>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[\?!A-Za-z/][^><]*>", "");
                html = Regex.Replace(html, "\r", "");
                html = Regex.Replace(html, "\n", "\r\n");
                html = Regex.Replace(html, @"[　|\s]*\r\n[　|\s]*\r\n", "\r\n");
                html = FormatHTML(html, eraseAll);
            }
            else
            {
                //替换段落与回车
                html = Regex.Replace(html, @"<p>", "[p]", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"</p>", "[/p]", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"</br>", "[/br]", RegexOptions.IgnoreCase);

                html = Regex.Replace(html, @"<!--[\s\S]*?-->", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<[//]*tr[^>]*>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<[//]*p[^>]*>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<[//]*br[^>]*>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<[//]*div[^>]*>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<STYLE[\s\S]*?</STYLE>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<iframe[\s\S]*?</iframe>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<option[\s\S]*?</option>", "", RegexOptions.IgnoreCase);
                //html = Regex.Replace(html, @"<[\?!A-Za-z/][^><]*>", "");
                html = Regex.Replace(html, "\r", "");
                html = Regex.Replace(html, "\n", "\r\n");
                html = Regex.Replace(html, @"[　|\s]*\r\n[　|\s]*\r\n", "\r\n");
                //html = FormatHTML(html, eraseAll);

                html = html.Replace("[p]", "<p>");
                html = html.Replace("[/p]", "</p>");
                html = html.Replace("[/br]", "</br>");
            }
            return html;
        }

        public static string EraseHTMLExceptAnchor(string strHtml, bool eraseAll)
        {

            string html = strHtml;
            html = Regex.Replace(html, @"<a", "[a]", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</a>", "[/a]", RegexOptions.IgnoreCase);
            if (eraseAll)
            {
                html = Regex.Replace(html, @"<!--[\s\S]*?-->", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*tr[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*p[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*br[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*div[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<STYLE[\s\S]*?</STYLE>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<iframe[\s\S]*?</iframe>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<option[\s\S]*?</option>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[\?!A-Za-z/][^><]*>", "");
                html = Regex.Replace(html, "\r", "");
                html = Regex.Replace(html, "\n", "\r\n");
                html = Regex.Replace(html, @"[　|\s]*\r\n[　|\s]*\r\n", "\r\n");
                html = FormatHTML(html, eraseAll);
            }
            else
            {
                //替换段落与回车
                html = Regex.Replace(html, @"<p>", "[p]", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"</p>", "[/p]", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"</br>", "[/br]", RegexOptions.IgnoreCase);

                html = Regex.Replace(html, @"<!--[\s\S]*?-->", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*tr[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*p[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*br[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[//]*div[^>]*>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<STYLE[\s\S]*?</STYLE>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<iframe[\s\S]*?</iframe>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<option[\s\S]*?</option>", "", RegexOptions.IgnoreCase);
                html = Regex.Replace(html, @"<[\?!A-Za-z/][^><]*>", "");
                html = Regex.Replace(html, "\r", "");
                html = Regex.Replace(html, "\n", "\r\n");
                html = Regex.Replace(html, @"[　|\s]*\r\n[　|\s]*\r\n", "\r\n");
                html = FormatHTML(html, eraseAll);

                html = html.Replace("[p]", "<p>");
                html = html.Replace("[/p]", "</p>");
                html = html.Replace("[/br]", "</br>");
            }
            html = html.Replace("[a]", "<a ");
            html = html.Replace("[/a]", "</a>");
            return html;
        }

        #endregion

        #region   FormatHTML
        /// <summary>
        /// 格式化所有的html标签
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        private static string FormatHTML(string str, bool eraseAll)
        {
            //替换<P>
            string html = Regex.Replace(str, " ", " ");
            if (eraseAll)
            {
                html = Regex.Replace(html, "&nbsp;", " ");
                html = Regex.Replace(html, "&nbsp", " ");
            }
            html = Regex.Replace(html, "&#8226;", " ");
            html = Regex.Replace(html, "&#8226", " ");
            html = Regex.Replace(html, "&#146;", "'");
            html = Regex.Replace(html, "&#147;", "“");
            html = Regex.Replace(html, "&#148;", "”");
            html = Regex.Replace(html, "&#160;", "");
            html = Regex.Replace(html, "&amp;", "&");
            html = Regex.Replace(html, "&copy;", "?");
            html = Regex.Replace(html, "&#150;", "–");
            html = Regex.Replace(html, "&quot;", " ");
            html = Regex.Replace(html, "&lt;", " ");
            html = Regex.Replace(html, "&gt;", " ");
            html = Regex.Replace(html, "&#13;&#10;", "");
            html = Regex.Replace(html, "&ldquo;", "“");
            html = Regex.Replace(html, "&rdquo;", "”");
            html = Regex.Replace(html, "\r", "");
            html = Regex.Replace(html, "\n", "");
            html = Regex.Replace(html, "\t", "");
            html = Regex.Replace(html, " ", "");
            return html;
        }

        #endregion

        #region   GetImageUrl

        /// <summary>
        /// html解析，获取img标签
        /// </summary>
        /// <param name="HtmlText"></param>
        /// <returns></returns>
        public static string[] GetImageList(string html)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(html);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Value;
            return sUrlList;
        }

        /// <summary>
        /// html解析，获取img标签
        /// </summary>
        /// <param name="HtmlText"></param>
        /// <returns></returns>
        public static string[] GetImageUrl(string html)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(html);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }

        #endregion

        #region   ConvertToSimpleContent

        /// <summary>
        /// 将HTML转换成简单的HTML文本（只包含段落、换行、空格），其中图片用特定字符标注（例如：{#image1#}）
        /// </summary>
        /// <param name="articleContent"></param>
        /// <returns></returns>
        public static string ConvertToSimpleContent(string articleContent)
        {
            string[] imageList = GetImageList(articleContent);
            string simpleContent = articleContent;
            for (int i = 0; i < imageList.Length; i++)
            {
                simpleContent = simpleContent.Replace(imageList[i], "{#image" + i.ToString() + "#}");
            }
            simpleContent = EraseHTML(simpleContent, false);
            return simpleContent;
        }

        public static string ConvertToSimpleContentExceptAnchor(string articleContent)
        {
            string[] imageList = GetImageList(articleContent);
            string simpleContent = articleContent;
            for (int i = 0; i < imageList.Length; i++)
            {
                simpleContent = simpleContent.Replace(imageList[i], "{#image" + i.ToString() + "#}");
            }
            simpleContent = EraseHTMLExceptAnchor(simpleContent, false);
            return simpleContent;
        }

        #endregion
    }
}
