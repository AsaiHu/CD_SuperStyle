<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true" CodeBehind="CharacterForm.aspx.cs" Inherits="WebApp.Pages.CMS.ArticleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Scripts/kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/kindeditor/kindeditor-all.js"></script>

    <script type="text/javascript" src="../../Scripts/kindeditor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../../Scripts/Pages/CMS/CharacterForm.js"></script>

    <script type="text/javascript">
        var editor1;
        KindEditor.ready(function (K) {
            editor1 = K.create('#valContent', {
                cssPath: '/Scripts/kindeditor/plugins/code/prettify.css',
                uploadJson: '/Scripts/kindeditor/upload_json.ashx',
                fileManagerJson: '/Scripts/kindeditor/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    this.sync();
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                },
                afterBlur: function () {
                    this.sync();
                }
            });
            prettyPrint();
        });
    </script>
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
                                <th data-options="field:'Name',width:400">昵称
                                </th>
                                <th data-options="field:'Sequence',width:400">排序
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

   
    <div id="w" class="easyui-window" title="人物信息" data-options="modal:true,closed:false,iconCls:'',minimizable:false" style="width: 780px; height: 500px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">

                <div style="width: 100%; float: left;" id="boxWindowLeft">
                    <div style="margin: 10px 0px 5px 10px;" id="boxName">
                        <input class="easyui-textbox" label="昵称:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valName" data-options="required:true" />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxExtend">
                        <input class="easyui-textbox" label="引申:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valExtendMean" />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxRepresent">
                        <input class="easyui-textbox" label="代表:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valRepresentVal" />
                    </div>

                    <div style="margin: 0px 0px 5px 10px;" id="boxSequence">
                        <input class="easyui-textbox" label="顺序:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valSequence"  />
                    </div>
                </div>

                <div id="tabs" class="easyui-tabs" style="width: 98%; height: 55%;">
                    <div id="tabs_1" title="人物详情" style="padding: 10px;">
                        <input class="ckeditor" label="" labelposition="left" multiline="true" style="width: 100%; height: 310px;"
                            id="valContent" />
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
