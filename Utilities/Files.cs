using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Data.OleDb;

namespace Utilities
{
    public class Files
    {
        static System.Drawing.Image oImg = null;
        static Bitmap bmp = null;
        static Graphics g = null;

        #region Upload

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="filePic">FileUpload</param>
        /// <param name="page">Page</param>
        /// <returns>上传成功的相对路径</returns>
        public static string Upload(FileUpload filePic, Page page)
        {
            HttpRequest Request = page.Request;
            string FileUrl = "";
            if (filePic.HasFile)
            {
                FileUrl = "/Uploads/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string realpath = Request.MapPath(FileUrl);
                if (!System.IO.Directory.Exists(realpath))
                {
                    System.IO.Directory.CreateDirectory(realpath);
                }
                System.Web.HttpPostedFile _postedFile = filePic.PostedFile;
                System.String _fileName, _fileExtension;
                _fileName = System.IO.Path.GetFileName(_postedFile.FileName);
                int IntFileSize = _postedFile.ContentLength;
                _fileExtension = System.IO.Path.GetExtension(_fileName);
                string str_time_Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                //string str_time_Data = DateTime.Now.ToString();
                //str_time_Data = str_time_Data.Replace(":", "");
                //str_time_Data = str_time_Data.Replace("-", "");
                //str_time_Data = str_time_Data.Replace(" ", "");
                //str_time_Data = str_time_Data.Replace("/", "");
                _fileName = str_time_Data + _fileExtension;
                FileUrl = FileUrl + _fileName;
                _postedFile.SaveAs(Request.MapPath(FileUrl));
            }
            return FileUrl;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="file">HtmlInputFile</param>
        /// <param name="page">Page</param>
        /// <returns>上传成功的相对路径</returns>
        public static string Upload(HtmlInputFile file, Page page)
        {
            HttpRequest Request = page.Request;
            string FileUrl = "";
            if (file.PostedFile.FileName != string.Empty)
            {
                FileUrl = "/Uploads/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string realpath = Request.MapPath(FileUrl);
                if (!System.IO.Directory.Exists(realpath))
                {
                    System.IO.Directory.CreateDirectory(realpath);
                }
                System.Web.HttpPostedFile _postedFile = file.PostedFile;
                System.String _fileName, _fileExtension;
                _fileName = System.IO.Path.GetFileName(_postedFile.FileName);
                int IntFileSize = _postedFile.ContentLength;
                _fileExtension = System.IO.Path.GetExtension(_fileName);
                string str_time_Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                //string str_time_Data = DateTime.Now.ToString();
                //str_time_Data = str_time_Data.Replace(":", "");
                //str_time_Data = str_time_Data.Replace("-", "");
                //str_time_Data = str_time_Data.Replace(" ", "");
                //str_time_Data = str_time_Data.Replace("/", "");
                _fileName = str_time_Data + _fileExtension;
                FileUrl = FileUrl + _fileName;
                _postedFile.SaveAs(Request.MapPath(FileUrl));
            }
            return FileUrl;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="page">Page</param>
        /// <returns>上传成功的相对路径</returns>
        public static string Upload(Page page)
        {
            HttpRequest Request = page.Request;
            string FileUrl = "";
            if (page.Request.Files.Count > 0)
            {
                FileUrl = "/Uploads/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                HttpPostedFile _postedFile = page.Request.Files[0];
                if (_postedFile.ContentLength > 0)
                {
                    string realpath = Request.MapPath(FileUrl);
                    if (!System.IO.Directory.Exists(realpath))
                    {
                        System.IO.Directory.CreateDirectory(realpath);
                    }
                    System.String _fileName, _fileExtension;
                    _fileName = System.IO.Path.GetFileName(_postedFile.FileName);
                    int IntFileSize = _postedFile.ContentLength;
                    _fileExtension = System.IO.Path.GetExtension(_fileName);
                    string str_time_Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //string str_time_Data = DateTime.Now.ToString();
                    //str_time_Data = str_time_Data.Replace(":", "");
                    //str_time_Data = str_time_Data.Replace("-", "");
                    //str_time_Data = str_time_Data.Replace(" ", "");
                    //str_time_Data = str_time_Data.Replace("/", "");
                    _fileName = str_time_Data + _fileExtension;

                    FileUrl = FileUrl + _fileName;
                    _postedFile.SaveAs(Request.MapPath(FileUrl));
                }
            }
            return FileUrl;
        }

        #endregion

        #region   MakeMyThumbnail

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="fromImg">图片对象</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <returns>缩略图</returns>
        public static Bitmap MakeMyThumbnail(System.Drawing.Image fromImg, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            int ow = fromImg.Width;
            int oh = fromImg.Height;

            //新建一个画板
            Graphics g = Graphics.FromImage(bmp);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            g.DrawImage(fromImg, new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, ow, oh),
                GraphicsUnit.Pixel);

            return bmp;

        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="SourceFile">原始图片路径</param>
        /// <param name="strSavePathFile">缩略图路径</param>
        /// <param name="ThumbWidth">缩略图宽度</param>
        /// <param name="ThumbHeight">缩略图高度</param>
        /// <param name="BgColor">背景色，可为null</param>
        public static bool MakeMyThumbnail(string SourceFile, string strSavePathFile, int ThumbWidth, int ThumbHeight, string BgColor)
        {
            bool result = false;
            try
            {
                oImg = System.Drawing.Image.FromFile(SourceFile);
                //小图 
                int intwidth, intheight;
                if (oImg.Width > oImg.Height)
                {
                    if (oImg.Width > ThumbWidth)
                    {
                        intwidth = ThumbWidth;
                        intheight = (oImg.Height * ThumbWidth) / oImg.Width;
                    }
                    else
                    {
                        intwidth = oImg.Width;
                        intheight = oImg.Height;
                    }
                }
                else
                {
                    if (oImg.Height > ThumbHeight)
                    {
                        intwidth = (oImg.Width * ThumbHeight) / oImg.Height;
                        intheight = ThumbHeight;
                    }
                    else
                    {
                        intwidth = oImg.Width;
                        intheight = oImg.Height;
                    }
                }
                //构造一个指定宽高的Bitmap 
                bmp = new Bitmap(intwidth, intheight);
                Graphics g = Graphics.FromImage(bmp);
                Color myColor;
                if (BgColor == null)
                    myColor = Color.FromName("white");
                else
                    myColor = Color.FromName(BgColor);
                //用指定的颜色填充Bitmap 
                g.Clear(myColor);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //开始画图 
                g.DrawImage(oImg, new Rectangle(0, 0, intwidth, intheight), new Rectangle(0, 0, oImg.Width, oImg.Height), GraphicsUnit.Pixel);
                bmp.Save(strSavePathFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (oImg != null)
                    oImg.Dispose();
                if (bmp != null)
                    bmp.Dispose();
                if (g != null)
                    g.Dispose();
                GC.Collect();
            }
            return result;
        }

        #endregion

        #region   SaveCutPic
        /// <summary>
        /// 按照指定尺寸裁剪图片
        /// </summary>
        /// <param name="pPath">原始图片路径</param>
        /// <param name="pSavedPath">裁剪完成后图片保存路径</param>
        /// <param name="pPartStartPointX">裁剪图片的x坐标</param>
        /// <param name="pPartStartPointY">裁剪图片的y坐标</param>
        /// <param name="pPartWidth">裁剪图片的宽度</param>
        /// <param name="pPartHeight">裁剪图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片的x坐标</param>
        /// <param name="pOrigStartPointY">原始图片的y坐标</param>
        /// <param name="imageWidth">原始图片的宽度</param>
        /// <param name="imageHeight">原始图片的高度</param>
        /// <returns>裁剪完成后的图片保存路径</returns>
        public static string SaveCutPic(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            using (System.Drawing.Image originalImg = System.Drawing.Image.FromFile(pPath))
            {
                if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                {
                    return SaveCutPic(pPath, pSavedPath, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight,
                            pOrigStartPointX, pOrigStartPointY);

                }
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                string filePath = pSavedPath + "\\" + filename;
                if (!Directory.Exists(pSavedPath))
                    Directory.CreateDirectory(pSavedPath);

                Bitmap thumimg = MakeMyThumbnail(originalImg, imageWidth, imageHeight);

                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);

                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                ///文字水印  
                Graphics G = Graphics.FromImage(partImg);
                //Font f = new Font("Lucida Grande", 6);
                //Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                G.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(thumimg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("Xuanye", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                partImg.Save(filePath, ImageFormat.Jpeg);

                partImg.Dispose();
                thumimg.Dispose();
                return filename;
            }
        }

        /// <summary>
        /// 按照指定尺寸裁剪图片
        /// </summary>
        /// <param name="pPath">原始图片路径</param>
        /// <param name="pSavedPath">裁剪完成后图片保存路径</param>
        /// <param name="pPartStartPointX">裁剪图片的x坐标</param>
        /// <param name="pPartStartPointY">裁剪图片的y坐标</param>
        /// <param name="pPartWidth">裁剪图片的宽度</param>
        /// <param name="pPartHeight">裁剪图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片的x坐标</param>
        /// <param name="pOrigStartPointY">原始图片的y坐标</param>
        /// <returns>裁剪完成后的图片保存路径</returns>
        public static string SaveCutPic(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            string filePath = pSavedPath + "\\" + filename;

            using (System.Drawing.Image originalImg = System.Drawing.Image.FromFile(pPath))
            {
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                ///注释 文字水印  
                Graphics G = Graphics.FromImage(partImg);
                //Font f = new Font("Lucida Grande", 6);
                //Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                G.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("Xuanye", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                partImg.Save(filePath, ImageFormat.Jpeg);
                partImg.Dispose();
            }
            return filename;
        }

        #endregion

        /// <summary>
        /// 验证上传文件大小
        /// </summary>
        /// <param name="_fileupload">FileUpload控件</param>
        /// <returns>上传文件是否超出限制</returns>
        public static bool IsValidSize(FileUpload _fileupload)
        {
            int IntFileSize = _fileupload.FileBytes.Length;
            if (IntFileSize > Config.MaxLength)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证上传文件扩展名
        /// </summary>
        /// <param name="_fileupload">FileUpload控件</param>
        /// <param name="_str_ext">文件后缀，对个文件以|分隔</param>
        /// <returns>文件格式是否合法</returns>
        public static bool IsValidFile(FileUpload _fileupload, string _str_ext)
        {
            //获取上传文件的文件名
            string _file_name = Path.GetFileName(_fileupload.FileName);
            //获取上传文件的扩展名
            string _file_ext = Path.GetExtension(_file_name).ToLower();
            if (_str_ext.IndexOf(_file_ext) == -1) return false;
            return true;
        }

        #region   ConverToJpgImage

        /// <summary>
        /// 生成文章列表上的缩略图
        /// </summary>
        /// <param name="picPath">用户上传的标题图片的相对路径</param>
        /// <param name="page">Page对象</param>
        /// <returns></returns>
        public static string ConverToJpgImage(string picPath, Page page)
        {
            if (!picPath.ToLower().EndsWith(".jpg"))
            {
                string _picPath = page.Server.MapPath(picPath);
                string jpgPath = picPath.Substring(0, picPath.LastIndexOf(".")) + ".jpg";
                string _jpgPath = _picPath.Substring(0, _picPath.LastIndexOf(".")) + ".jpg";
                if (!File.Exists(_jpgPath))
                {
                    new ImageConvert().Convert(_picPath, _jpgPath, "jpg");
                }
                return jpgPath;
            }
            else
            {
                return picPath;
            }
        }

        #endregion

        #region   ConverToSmallImage

        /// <summary>
        /// 生成文章列表上的缩略图
        /// </summary>
        /// <param name="jpgPath">用户上传的JPG标题图片的相对路径</param>
        /// <param name="page">Page对象</param>
        /// <returns></returns>
        public static string ConverToSmallImage(string jpgPath, Page page)
        {
            int max_width = 180;
            string _jpgPath = page.Server.MapPath(jpgPath);
            System.Drawing.Image jpgImage = System.Drawing.Image.FromFile(_jpgPath);
            if (jpgImage.Width > max_width)
            {
                string smallPath = jpgPath.Substring(0, jpgPath.LastIndexOf(".")) + "_" + max_width.ToString() + ".jpg";
                string _smallPath = page.Server.MapPath(smallPath);
                if (!File.Exists(_smallPath))
                {
                    int width = max_width;
                    int height = jpgImage.Height / (jpgImage.Width / max_width);
                    new ImageConvert().Convert(_jpgPath, _smallPath, "jpg", width, height);
                }
                return smallPath;
            }
            else
            {
                return jpgPath;
            }
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
            string[] imageList = HTML.GetImageList(articleContent);
            string simpleContent = articleContent;
            for (int i = 0; i < imageList.Length; i++)
            {
                simpleContent = simpleContent.Replace(imageList[i], "{#image" + i.ToString() + "#}");
            }
            simpleContent = HTML.EraseHTML(simpleContent, false);
            return simpleContent;
        }

        public static string ConvertToSimpleContentExceptAnchor(string articleContent)
        {
            string[] imageList = HTML.GetImageList(articleContent);
            string simpleContent = articleContent;
            for (int i = 0; i < imageList.Length; i++)
            {
                simpleContent = simpleContent.Replace(imageList[i], "{#image" + i.ToString() + "#}");
            }
            simpleContent = HTML.EraseHTMLExceptAnchor(simpleContent, false);
            return simpleContent;
        }

        #endregion

        #region   ConvertToSimpleContent

        /// <summary>
        /// 截取内容描述
        /// </summary>
        /// <param name="article"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ConvertToSimpleContent(string article, int length)
        {
            string simpleContent = HTML.EraseHTML(article, true);
            simpleContent = simpleContent.Length > length ? simpleContent.Substring(0, length - 1) + "..." : simpleContent;
            return simpleContent;
        }

        #endregion


        /// <summary>
        /// 数据导出至Excel
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="file_name">文件保存的相对路径</param>
        /// <param name="page">Page对象</param>
        /// <param name="message"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExportToExcel(DataTable data, string sheet_name, Page page, ref string message, ref string file_src)
        {
            try
            {
                string file_name = sheet_name + ".xls";
                file_src = "/uploads/excel/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string file_path = page.MapPath(file_src);
                if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);
                string save_path = file_path + file_name;
                if (File.Exists(save_path)) File.Delete(save_path);
                ExcelDivert excel = new ExcelDivert();
                excel.name = sheet_name;
                excel.ExPort(file_path + file_name, data);
                file_src = file_src + file_name;
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }

    public enum FileType { TXT, XML, Excel, TXTComma, TXTTAB }

    public interface IDivert
    {
        string name
        {
            get;
            set;
        }
        DataTable InPort(string path);
        void ExPort(string path, DataTable dt);
    }

    #region Clase DivertClass
    /// <summary>
    /// Clase DivertClass
    /// </summary>
    public class DivertClass
    {
        //private DivertClass(){}
        public static IDivert Instance(FileType ft)
        {
            switch (ft)
            {

                case FileType.TXT:
                    return new TXTDivert();
                case FileType.TXTComma:
                    return new TXTDivert(',');
                case FileType.TXTTAB:
                    return new TXTDivert('\t');
                case FileType.XML:
                    return new XMLDivert();
                case FileType.Excel:
                    return new ExcelDivert();
                default:
                    throw new Exception("请指定要导入/导出的文件类型！");
            }
        }
    }
    #endregion

    #region Class XMLDivert:IDivert
    /// <summary>
    /// Class XMLDivert:IDivert
    /// </summary>
    public class XMLDivert : IDivert
    {

        private string m_Name;
        public string name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public DataTable InPort(string path)
        {
            DataTable dt = new DataTable();
            dt.ReadXml(path);
            return dt;
        }

        public void ExPort(string path, DataTable dt)
        {
            dt.WriteXml(path);
        }
    }
    #endregion

    #region class TXTDivert:IDivert
    /// <summary>
    /// class TXTDivert:IDivert
    /// </summary>
    public class TXTDivert : IDivert
    {
        public TXTDivert()
        {
        }
        public TXTDivert(char Seprate)
        {
            this.m_Seprate = Seprate;
        }
        private char m_Seprate = '\t';
        private string m_Name;
        public string name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public DataTable InPort(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

            //表头
            string s = sr.ReadLine();
            string[] title = s.Split(m_Seprate);
            System.Data.DataTable dt = new System.Data.DataTable();

            //内容
            for (int i = 0; i < title.Length; i++)
            {
                dt.Columns.Add(title[i]);
            }
            try
            {
                while (true)
                {
                    s = sr.ReadLine();
                    if (s == null) break;
                    dt.Rows.Add(s.Split(m_Seprate));
                }

            }
            catch (Exception err)
            {
                throw err;
            }
            sr.Close();
            fs.Close();
            DataTable table = dt.Copy();
            return table;
        }

        public void ExPort(string path, DataTable dt)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default, 256);
            if (this.m_Seprate == '\0')
            {
                this.m_Seprate = '\t';
            }
            //				//表头
            //				for(int j=0;j<dt.Columns.Count;j++)
            //				{
            //					sw.Write(dt.Columns[j].ToString());
            //					if(j!=(dt.Columns.Count-1))sw.Write(this.m_Seprate);
            //				}
            if (dt.Rows.Count != 0) sw.Write("\r\n");

            //内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(dt.Rows[i][j].ToString());
                    if (j != (dt.Columns.Count - 1)) sw.Write(this.m_Seprate);
                }
                if (i != (dt.Rows.Count - 1)) sw.Write("\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
    #endregion

    #region  class ExcelDivert:IDivert
    /// <summary>
    /// class ExcelDivert:IDivert
    /// </summary>
    public class ExcelDivert : IDivert
    {
        private string m_Name;
        public string name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public static DataTable GetExcelTableName(string path)
        {
            //try
            //{
            if (System.IO.File.Exists(path))
            {
                //OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 8.0;");
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");//HDR设置为NO  则不把第一行作为列名
                conn.Open();
                DataTable _Table = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                conn.Close();
                return _Table;
            }
            return null;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public DataTable InPort(string path)
        {
            if (this.name == null)
            {
                throw new Exception("请指定要导入的数据表名！");
            }
            //OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;IMEX=1'");
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");//HDR设置为NO  则不把第一行作为列名
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM [" + this.name + "$]";
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = cmd;
            DataSet ds = new DataSet();
            ad.Fill(ds, "dt");
            conn.Close();
            DataTable table = ds.Tables[0].Copy();
            return table;
        }

        public DataTable InPort(string path, string hdr)
        {
            if (this.name == null)
            {
                throw new Exception("请指定要导入的数据表名！");
            }
            //OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'");
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");//HDR设置为NO  则不把第一行作为列名
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM [" + this.name + "$]";
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = cmd;
            DataSet ds = new DataSet();
            ad.Fill(ds, "dt");
            conn.Close();
            DataTable table = ds.Tables[0].Copy();
            return table;
        }

        public void ExPort(string path, DataTable dt)
        {
            //#region use oledb
            if (this.name == null)
            {
                this.name = dt.TableName;
            }


            //创建文件
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);


            Stream fsr = Assembly.Load("Utility").GetManifestResourceStream("Utility.Resources.ExcelFile.xls");
            byte[] bt = new Byte[fsr.Length];
            fsr.Read(bt, 0, bt.Length);
            fsr.Close();
            fs.Write(bt, 0, bt.Length);
            fs.Close();

            OleDbConnection conn = null;
            try
            {
                //conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 8.0;");
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");//HDR设置为NO  则不把第一行作为列名
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                //表头
                string ColumnsInfo = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].DataType == typeof(DateTime))
                    {
                        ColumnsInfo += "[" + dt.Columns[i].ColumnName + "]" + " date,";
                    }
                    else if (dt.Columns[i].DataType == typeof(int))
                    {
                        ColumnsInfo += "[" + dt.Columns[i].ColumnName + "]" + " int,";
                    }
                    else if (dt.Columns[i].DataType == typeof(decimal))
                    {
                        ColumnsInfo += "[" + dt.Columns[i].ColumnName + "]" + " decimal,";
                    }
                    else
                    {
                        ColumnsInfo += "[" + dt.Columns[i].ColumnName + "]" + " char,";
                    }
                }
                if (ColumnsInfo.Length > 0) ColumnsInfo = ColumnsInfo.Substring(0, ColumnsInfo.Length - 1);
                cmd.CommandText = "CREATE TABLE [" + this.name + "] (" + ColumnsInfo + ")";
                cmd.ExecuteNonQuery();

                //纪录
                string values = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    values = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].DataType == typeof(string))
                        {
                            values += "'" + dt.Rows[i][j].ToString() + "',";
                        }
                        else
                        {
                            if (dt.Rows[i][j].ToString() == string.Empty)
                            {
                                values += "null,";
                            }
                            else
                            {
                                values += "'" + dt.Rows[i][j].ToString() + "',";
                            }
                        }
                    }
                    if (values.Length > 0) values = values.Substring(0, values.Length - 1);
                    cmd.CommandText = "INSERT INTO [" + this.name + "$] Values(" + values + ")";
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "Drop Table [Sheet1$]";
                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ee)
            {
                conn.Close();
                throw ee;

            }
        }
    }
    #endregion
}
