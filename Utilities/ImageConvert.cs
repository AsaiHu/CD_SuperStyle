using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Utilities;

namespace Utilities
{
    public class ImageConvert
    {
        private int ICON_W = 64;

        private int ICON_H = 64;

        public ImageConvert()
        {

        }

        //fileinpath,origaly picture file path,fileoutpath save filepath,index the ImageFormat you want to convert to

        public string Convert(string fileinpath, string fileoutpath, string index)
        {

            try
            {

                Bitmap bitmap = new Bitmap(fileinpath);
                index = index.ToLower();
                switch (index)
                {

                    case "jpg": bitmap.Save(fileoutpath, ImageFormat.Jpeg); break;

                    case "jpeg": bitmap.Save(fileoutpath, ImageFormat.Jpeg); break;

                    case "bmp": bitmap.Save(fileoutpath, ImageFormat.Bmp); break;

                    case "png": bitmap.Save(fileoutpath, ImageFormat.Png); break;

                    case "emf": bitmap.Save(fileoutpath, ImageFormat.Emf); break;

                    case "gif": bitmap.Save(fileoutpath, ImageFormat.Gif); break;

                    case "wmf": bitmap.Save(fileoutpath, ImageFormat.Wmf); break;

                    case "exif": bitmap.Save(fileoutpath, ImageFormat.Exif); break;

                    case "tiff":
                        {

                            Stream stream = File.Create(fileoutpath);

                            bitmap.Save(stream, ImageFormat.Tiff);

                            stream.Close();

                        } break;

                    case "ico":
                        {

                            Icon ico;

                            Stream stream = File.Create(fileoutpath);

                            ico = BitmapToIcon(bitmap, false);

                            ico.Save(stream);       //   save the icon

                            stream.Close();

                        }; break;

                    default: return "Error!";

                }

                return "Success!";

            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Convert(string fileinpath, string fileoutpath, string index, int width, int height)
        {

            if (width <= 0 || height <= 0)

                return "error!size illegal!";

            try
            {

                Bitmap mybitmap = new Bitmap(fileinpath);
                
                Bitmap bitmap = new Bitmap(mybitmap, width, height);
                
                index = index.ToLower();

                switch (index)
                {

                    case "jpg": bitmap.Save(fileoutpath, ImageFormat.Jpeg); break;

                    case "jpeg": bitmap.Save(fileoutpath, ImageFormat.Jpeg); break;

                    case "bmp": bitmap.Save(fileoutpath, ImageFormat.Bmp); break;

                    case "png": bitmap.Save(fileoutpath, ImageFormat.Png); break;

                    case "emf": bitmap.Save(fileoutpath, ImageFormat.Emf); break;

                    case "gif": bitmap.Save(fileoutpath, ImageFormat.Gif); break;

                    case "wmf": bitmap.Save(fileoutpath, ImageFormat.Wmf); break;

                    case "exif": bitmap.Save(fileoutpath, ImageFormat.Exif); break;

                    case "tiff":
                        {

                            Stream stream = File.Create(fileoutpath);

                            bitmap.Save(stream, ImageFormat.Tiff);

                            stream.Close();

                        } break;

                    case "ico":
                        {

                            if (height > 256 || width > 256)//ico maxsize 256*256

                                return "Error!Size illegal!";

                            Icon ico;

                            ICON_H = height;

                            ICON_W = width;

                            Stream stream = File.Create(fileoutpath);

                            ico = BitmapToIcon(mybitmap, true);

                            ico.Save(stream);       //   save the icon

                            stream.Close();

                        }; break;

                    default: return "Error!";

                }

                return "Success!";

            }

            catch (Exception ex)
            {
                //Utilities.Log.Error(ex);
                throw ex;
            }
        }


        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                 
                    break;
                case "W"://指定宽，高按比例                     
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        } 

        private Icon BitmapToIcon(Bitmap obm, bool preserve)
        {

            Bitmap bm;

            // if not preserving aspect

            if (!preserve)        // if not preserving aspect

                bm = new Bitmap(obm, ICON_W, ICON_H);  //   rescale from original bitmap

            // if preserving aspect drop excess significance in least significant direction

            else          // if preserving aspect
            {

                Rectangle rc = new Rectangle(0, 0, ICON_W, ICON_H);

                if (obm.Width >= obm.Height)   //   if width least significant
                {          //     rescale with width based on max icon height

                    bm = new Bitmap(obm, (ICON_H * obm.Width) / obm.Height, ICON_H);

                    rc.X = (bm.Width - ICON_W) / 2;  //     chop off excess width significance

                    if (rc.X < 0) rc.X = 0;

                }

                else         //   if height least significant
                {          //     rescale with height based on max icon width

                    bm = new Bitmap(obm, ICON_W, (ICON_W * obm.Height) / obm.Width);

                    rc.Y = (bm.Height - ICON_H) / 2; //     chop off excess height significance

                    if (rc.Y < 0) rc.Y = 0;

                }

                bm = bm.Clone(rc, bm.PixelFormat);  //   bitmap for icon rectangle

            }

            // create icon from bitmap

            Icon icon = Icon.FromHandle(bm.GetHicon()); // create icon from bitmap

            bm.Dispose();        // dispose of bitmap

            return icon;        // return icon

        }

        /****************************************/

        //public bool GenerateHighThumbnail(string oldImagePath, string newImagePath, int width, int height)
        //{
        //    try
        //    {
        //        System.Drawing.Image oldImage = System.Drawing.Image.FromFile(oldImagePath);
        //        int newWidth = AdjustSize(width, height, oldImage.Width, oldImage.Height).Width;
        //        int newHeight = AdjustSize(width, height, oldImage.Width, oldImage.Height).Height;

        //        System.Drawing.Image thumbnailImage = oldImage.GetThumbnailImage(newWidth, newHeight, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
        //        System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);


        //        //处理JPG质量的函数
        //        System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
        //        if (ici != null)
        //        {
        //            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
        //            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
        //            bm.Save(newImagePath, ici, ep);

        //            //释放所有资源，不释放，可能会出错误。
        //            ep.Dispose();
        //            ep = null;
        //        }
        //        ici = null;

        //        bm.Dispose();
        //        bm = null;

        //        thumbnailImage.Dispose();
        //        thumbnailImage = null;
        //        oldImage.Dispose();
        //        oldImage = null;
        //        return true;
        //    }
        //    catch { return false; }
        //}

        //private bool ThumbnailCallback()
        //{
        //    return false;
        //}
        //private ImageCodecInfo GetEncoderInfo(String mimeType)
        //{
        //    int j;
        //    ImageCodecInfo[] encoders;
        //    encoders = ImageCodecInfo.GetImageEncoders();
        //    for (j = 0; j < encoders.Length; ++j)
        //    {
        //        if (encoders[j].MimeType == mimeType)
        //            return encoders[j];
        //    }
        //    return null;
        //}
        //public struct PicSize
        //{
        //    public int Width;
        //    public int Height;
        //}
        //public PicSize AdjustSize(int spcWidth, int spcHeight, int orgWidth, int orgHeight)
        //{
        //    PicSize size = new PicSize();
        //    // 原始宽高在指定宽高范围内，不作任何处理 
        //    if (orgWidth <= spcWidth && orgHeight <= spcHeight)
        //    {
        //        size.Width = orgWidth;
        //        size.Height = orgHeight;
        //    }
        //    else
        //    {
        //        // 取得比例系数 
        //        float w = orgWidth / (float)spcWidth;
        //        float h = orgHeight / (float)spcHeight;
        //        // 宽度比大于高度比 
        //        if (w > h)
        //        {
        //            size.Width = spcWidth;
        //            size.Height = (int)(w >= 1 ? Math.Round(orgHeight / w) : Math.Round(orgHeight * w));
        //        }
        //        // 宽度比小于高度比 
        //        else if (w < h)
        //        {
        //            size.Height = spcHeight;
        //            size.Width = (int)(h >= 1 ? Math.Round(orgWidth / h) : Math.Round(orgWidth * h));
        //        }
        //        // 宽度比等于高度比 
        //        else
        //        {
        //            size.Width = spcWidth;
        //            size.Height = spcHeight;
        //        }
        //    }
        //    return size;
        //}

    }
}
