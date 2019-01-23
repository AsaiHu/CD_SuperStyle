using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Utilities;
using Models.Data;
using Newtonsoft.Json.Linq;

namespace WebApp.Api
{
    public class FileController : BaseController
    {
        public JsonResult Upload()
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {

                HttpContext.Response.ContentType = "text/html";
                System.Web.HttpFileCollectionBase files = HttpContext.Request.Files;
                if (files.Count > 0)
                {
                    string save_url = "/Uploads/" + DateTime.Now.ToString("yyyyMMdd");
                    string save_path = HttpContext.Server.MapPath(save_url);
                    if (!Directory.Exists(save_path)) Directory.CreateDirectory(save_path);

                    //string file_name = Path.GetFileName(files[0].FileName);
                    string file_name = DateTime.Now.ToString("yyMMddhhmss") + "_" + Path.GetFileName(files[0].FileName);
                    string url = save_url + "/" + file_name;
                    string save_name = HttpContext.Server.MapPath(url);
                    if (System.IO.File.Exists(save_name)) System.IO.File.Delete(save_name);
                    files[0].SaveAs(save_name);

                    json.Data = JsonUtil.GetSuccessForString(url);
                }
                else
                {
                    json.Data = JsonUtil.GetFail(DAL.Enum_StatusCode.Error_FileUploadFailed);
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

        public JsonResult ParseToSWF()
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                HttpContext.Response.ContentType = "text/html";
                System.Web.HttpFileCollectionBase files = HttpContext.Request.Files;
                if (files.Count > 0)
                {
                    string save_url = "/Uploads/" + DateTime.Now.ToString("yyyyMMdd");
                    string save_path = HttpContext.Server.MapPath(save_url);
                    if (!Directory.Exists(save_path)) Directory.CreateDirectory(save_path);

                    //string file_name = Path.GetFileName(files[0].FileName);
                    string file_name = DateTime.Now.ToString("yyMMddhhmss") + "_" + Path.GetFileName(files[0].FileName);
                    string url = save_url + "/" + file_name;
                    string save_name = HttpContext.Server.MapPath(url);
                    if (System.IO.File.Exists(save_name)) System.IO.File.Delete(save_name);
                    files[0].SaveAs(save_name);

                    //string source_path = HttpContext.Server.MapPath(url);
                    //string target_url = url.Replace(".pdf", ".swf");
                    //string target_path = HttpContext.Server.MapPath(target_url);
                    //if (System.IO.File.Exists(target_path)) System.IO.File.Delete(target_path);


                    //if (SwfUtil.PDFToSWF(Config.Pdf2SwfPath, source_path, target_path))
                    //{
                    //    JObject obj = new JObject();
                    //    obj["source_url"] = url;
                    //    obj["target_url"] = target_url;
                    //    json.Data = JsonUtil.GetSuccessForObject(obj);
                    //}
                    //else
                    //    json.Data = JsonUtil.GetFail(DAL.Enum_StatusCode.Error_FileParseFailed);

                    JObject obj = new JObject();
                    obj["source_url"] = url;
                    obj["target_url"] = null;
                    json.Data = JsonUtil.GetSuccessForObject(obj);
                }
                else
                {
                    json.Data = JsonUtil.GetFail(DAL.Enum_StatusCode.Error_FileUploadFailed);
                }
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);
            }
            return json;
        }

        public JsonResult Cut(ImageCutDataView entity)
        {
            CustomJsonResult json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentType = "text/plain";

            try
            {
                string cutPath = entity.Src.Substring(0, entity.Src.LastIndexOf(@"/")) + @"/";
                cutPath += Files.SaveCutPic(Server.MapPath(entity.Src), Server.MapPath(cutPath), 0, 0, entity.DropWidth,
                                        entity.DropHeight, entity.CutLeft, entity.CutTop, entity.ImageWidth, entity.ImageHeight);

                json.Data = JsonUtil.GetSuccessForString(cutPath);
            }
            catch (Exception ex)
            {
                json.Data = JsonUtil.GetFailForString(ex.Message);

            }
            return json;
        }

    }
}