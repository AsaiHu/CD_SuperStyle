<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true" CodeBehind="ContactForm.aspx.cs" Inherits="WebApp.Pages.CMS.ArticleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Scripts/kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/kindeditor/kindeditor-all.js"></script>

    <script type="text/javascript" src="../../Scripts/kindeditor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../../Scripts/Pages/CMS/ContactForm.js"></script>

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
                                <th data-options="field:'Telephone',width:200">电话
                                </th>
                                <th data-options="field:'MobilePhone',width:200">手机
                                </th>
                                <th data-options="field:'Fax',width:200">传真
                                </th>
                                <th data-options="field:'Address',width:400">地址
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

   
    <div id="w" class="easyui-window" title="联系方式" data-options="modal:true,closed:false,iconCls:'',minimizable:false" style="width: 780px; height: 750px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">

                <div style="width: 100%; float: left;" id="boxWindowLeft">
                    <div style="margin: 10px 0px 5px 10px;" id="boxTelephone">
                        <input class="easyui-textbox" label="电话:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valTelephone" data-options="required:true" />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxMobilePhone">
                        <input class="easyui-textbox" label="手机:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valMobilePhone" />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxFax">
                        <input class="easyui-textbox" label="传真:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valFax" />
                    </div>

                    <div style="margin: 0px 0px 5px 10px;" id="boxAddress">
                        <input class="easyui-textbox" label="地址:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valAddress"  />
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
