using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DAL.Business;
using System.Data;
using Utilities;
using DAL;
using MyOrm.Common;

namespace WebApp.Pages
{
    public partial class MasterForm : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ModuleId"] != null)
                {
                    string moduleId = Request.QueryString["ModuleId"].ToString();
                    this.GetModule(moduleId);
                    this.GetPermission(moduleId);
                    this.BuildToolBarButtons(moduleId);
                }
            }
        }

        private void GetModule(string moduleId)
        {
            Modules module = ServiceFactory.Factory.ModulesService.SearchOne(new SimpleCondition("ID", moduleId));
            if (module != null)
            {
                this.Page.Title = module.DisplayName + "_苏州威博世网络科技有限公司";
            }
        }

        private bool permissionAdd = true;
        private bool permissionEdit = true;
        private bool permissionDelete = true;
        private bool permissionSubmit = true;
        private bool permissionQuery = true;

        /// <summary>
        /// 获得页面的权限
        /// </summary>
        private void GetPermission(string moduleId)
        {
            int _moduleId = Convert.ToInt16(moduleId);
            DataView dv = ServiceFactory.Factory.RightsService.Search((int)Security.CurrentUser.ID, _moduleId).DefaultView;
            if (dv.Count <= 0) Response.Redirect(Utilities.Pages.URL_ERROR);


            for (int i = 0; i < dv.Count; i++)
            {
                this.permissionAdd = Convert.ToBoolean(dv[i]["HasAdd"]);
                this.permissionEdit = Convert.ToBoolean(dv[i]["HasUpdate"]);
                this.permissionDelete = Convert.ToBoolean(dv[i]["HasDelete"]);
                this.permissionSubmit = Convert.ToBoolean(dv[i]["HasSubmit"]);
                this.permissionQuery = Convert.ToBoolean(dv[i]["HasQuery"]);
            }
        }

        public void BuildToolBarButtons(string moduleId)
        {
            Modules module = ServiceFactory.Factory.ModulesService.SearchOne(new SimpleCondition("ID", moduleId));
            if (module != null)
            {
                if ((bool)module.RefreshFlag || (bool)module.AddFlag || (bool)module.UpdateFlag || (bool)module.DeleteFlag || (bool)module.SubmitFlag || (bool)module.QueryFlag || (bool)module.KeywordFlag)
                {
                    string linkbtn_template = "<a id=\"a_{0}\" class=\"easyui-linkbutton\" style=\"float:left\" plain=\"true\" href=\"javascript:void(0);\" icon=\"{1}\"  {2} title=\"{3}\" onclick=\"{4}\">{5}</a>";
                    string textbox_template = "<div style=\"float:left;padding-left:3px;padding-top:1px;margin-right:1px;\"><input class=\"easyui-textbox\" id=\"input_{0}\" {1} style=\"width:100px;float:right;\" data-options=\"prompt:'请输入关键字'\" /></div>";

                    StringBuilder sb = new StringBuilder();
                    if ((bool)module.RefreshFlag)
                    {
                        sb.Append(string.Format(linkbtn_template, "refresh", module.RefreshIcon, "", module.RefreshCaption, module.RefreshFunc, module.RefreshCaption));
                        sb.Append("<div class='datagrid-btn-separator'></div> ");
                    }
                    if ((bool)module.AddFlag)
                    {
                        sb.Append(string.Format(linkbtn_template, "add", module.AddIcon, permissionAdd ? "" : "disabled=\"True\"", module.AddCaption, module.AddFunc, module.AddCaption));
                    }
                    if ((bool)module.UpdateFlag)
                    {
                        sb.Append(string.Format(linkbtn_template, "edit", module.UpdateIcon, permissionEdit ? "" : "disabled=\"True\"", module.UpdateCaption, module.UpdateFunc, module.UpdateCaption));
                    }
                    if ((bool)module.DeleteFlag)
                    {
                        sb.Append(string.Format(linkbtn_template, "delete", module.DeleteIcon, permissionDelete ? "" : "disabled=\"True\"", module.DeleteCaption, module.DeleteFunc, module.DeleteCaption));
                    }
                    if ((bool)module.AddFlag || (bool)module.UpdateFlag || (bool)module.DeleteFlag)
                    {
                        sb.Append("<div class='datagrid-btn-separator'></div> ");
                    }
                    if ((bool)module.SubmitFlag)
                    {
                        sb.Append(string.Format(linkbtn_template, "delete", module.SubmitIcon, permissionSubmit ? "" : "disabled=\"True\"", module.SubmitCaption, module.SubmitFunc, module.SubmitCaption));
                        sb.Append("<div class='datagrid-btn-separator'></div> ");
                    }
                    if ((bool)module.QueryFlag)
                    {
                        if ((bool)module.KeywordFlag)
                        {
                            sb.Append(string.Format(textbox_template, "keyword", permissionQuery ? "" : "disabled=\"true\""));
                        }
                        sb.Append(string.Format(linkbtn_template, "search", module.QueryIcon, permissionQuery ? "" : "disabled=\"True\"", module.QueryCaption, module.QueryFunc, module.QueryCaption));
                        sb.Append("<div class='datagrid-btn-separator'></div> ");
                    }
                    sb.Append(string.Format(linkbtn_template, "help", "icon-page-help", "", "帮助", "", "帮助"));
                    ////sb.Append(string.Format(linkbtn_template, "setusermodulepermission", "icon-user_key", permissionUserModulePermission ? "" : "disabled=\"True\"", "设置用户的模块（菜单）访问权限", "用户模块权限"));
                    ////sb.Append(string.Format(linkbtn_template, "setrolemodulepermission", "icon-group_key", permissionRoleModulePermission ? "" : "disabled=\"True\"", "设置角色的模块（菜单）访问权限", "角色模块权限"));
                    ////sb.Append("<div class='datagrid-btn-separator'></div> ");
                    ////sb.Append(string.Format(linkbtn_template, "moduleconfig", "icon-table_gear", permissionRoleModulePermission ? "" : "disabled=\"True\"", "设置模块的可用性", "模块配置"));
                    //return "<div style=\"padding:1px;\">" + sb.ToString() + "</div>";
                    this.toolbar.InnerHtml = "<div style=\"padding:1px;\">" + sb.ToString() + "</div>";
                }
            }
        }
    }
}
