<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="WebApp.Pages.CMS.ArticleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Scripts/kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/kindeditor/kindeditor-all.js"></script>

    <script type="text/javascript" src="../../Scripts/kindeditor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../../Scripts/Pages/CMS/ProductForm.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrap" class="easyui-layout" style="width: 100%; height: 500px;" fit="true" scroll="no">
        <div region="center" title="" id="center">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'center',border:false">
                    <table class="easyui-datagrid" id="datagrid" style="width: 100%; height: auto;" border="0" data-options="
                   rownumbers:true,
        striped:true,
        fitColumns: true,
        singleSelect:true
                ">
                        <thead>
                            <tr>
                                <th data-options="field:'Name',width:400">产品名
                                </th>
                                <th data-options="field:'PublishTime',width:400">发表时间
                                </th>

                            </tr>
                        </thead>
                    </table>
                </div>
                <div data-options="region:'south',border:false" style="height: 60px;">
                    <div id="pagination" class="easyui-pagination" style="background: #efefef; border: 1px solid #ccc;"
                        data-options="onSelectPage: function(pageNumber, pageSize){page_number=pageNumber;page_size=pageSize;query_data();$('#content').panel('refresh', url_query+'?pageNumber='+pageNumber+'&pageSize='+pageSize);}">
                    </div>
                </div>
            </div>
        </div>
    </div>

   
    <div id="w" class="easyui-window" title="产品信息" data-options="modal:true,closed:false,iconCls:'',minimizable:false" style="width: 780px; height: 750px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">

                <div style="width: 60%; float: left;" id="boxWindowLeft">
                    <div style="margin: 10px 0px 5px 10px;" id="boxTitle">
                        <input class="easyui-textbox" label="产品名:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valName" data-options="required:true" />
                    </div>
                   
                    <div style="margin: 0px 0px 5px 10px;" id="boxPublishTime">
                        <input class="easyui-datetimebox" label="发布日期:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valPublishTime" />
                    </div>
                </div>

                <div style="width: 40%; float: left;" id="boxWindowRight">
                    <div style="margin: 10px 0px 0px 0px; width: 96%; height: 236px; padding: 1px; border: 1px solid #ccc;" id="boxImg">
                        <input type="file" id="file_upload" style="display: none;" onchange="action_upload_image('file_upload','src','img');" />
                        <img style="width: 100%; height: 100%;" title="上传图片" id="img" src="../../Images/web/default.png"  onclick="action_open_file('file_upload');" alt="上传图片" />
                        <input id="src" type="hidden" />
                    </div>
                </div>
            </div>
            <div data-options="region:'south',border:false" style="text-align: right; padding: 5px 0 0 0;">
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" style="width: 55px;" onclick="save_data();">保存</a>
                <a href="#" class="easyui-linkbutton" onclick="close_dialog();" style="width: 55px;">关闭</a>
            </div>
        </div>
    </div>
</asp:Content>
