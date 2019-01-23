<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true" CodeBehind="ArticleForm.aspx.cs" Inherits="WebApp.Pages.CMS.ArticleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Scripts/kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/kindeditor/kindeditor-all.js"></script>

    <script type="text/javascript" src="../../Scripts/kindeditor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../../Scripts/Pages/CMS/ArticleForm.js"></script>

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
                                <th data-options="field:'Title',width:400">标题
                                </th>
                                <th data-options="field:'Keywords',width:150">关键字
                                </th>
                                <th data-options="field:'IsPublished',width:150,formatter:formart_public">发布状态
                                </th>
                                <th data-options="field:'PublishTime',width:150">发布时间
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

   
    <div id="w" class="easyui-window" title="新建文章信息" data-options="modal:true,closed:false,iconCls:'',minimizable:false" style="width: 780px; height: 750px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">

                <div style="width: 60%; float: left;" id="boxWindowLeft">
                    <div style="margin: 10px 0px 5px 10px;" id="boxTitle">
                        <input class="easyui-textbox" label="文章标题:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valTitle" data-options="required:true" />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxShortTitle">
                        <input class="easyui-textbox" label="短标题:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valShortTitle" />
                    </div>


                    <div style="margin: 0px 0px 5px 10px;" id="boxDescription">
                        <input class="easyui-textbox" label="描述内容:" labelposition="left" style="width: 95%; height: 50px;"
                            id="valDescription" data-options="multiline:true" />
                    </div>

                    <div style="margin: 0px 0px 5px 10px;" id="boxCutSrc">
                        <input type="file" id="file1" style="display: none;" onchange="action_upload_file('file1','valCutSrc');" />
                        <input class="easyui-textbox" label="PDF上传:" readonly="readonly" labelposition="left"
                            style="width: 80%; height: 27px;" data-options="
                        required:false"
                            id="valCutSrc" />
                        <a href="#" class="easyui-linkbutton" style="width: 14%; height: 27px;" onclick="action_open_file('file1');">选择</a>
                    </div>


                    <div style="margin: 0px 0px 5px 10px;" id="boxKeywords">
                        <input class="easyui-textbox" label="关键字:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valKeywords"  />
                    </div>
                    <div style="margin: 0px 0px 5px 10px;" id="boxPublishTime">
                        <input class="easyui-datetimebox" label="发布日期:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valPublishTime" />
                    </div>
                    <div style="margin: 0px 0px 10px 10px;" id="boxAnthor">
                        <input class="easyui-textbox" label="作者:" labelposition="left" style="width: 95%; height: 27px;"
                            id="valAnthor" />

                    </div>
                </div>

                <div style="width: 40%; float: left;" id="boxWindowRight">
                    <div style="margin: 10px 0px 0px 0px; width: 96%; height: 236px; padding: 1px; border: 1px solid #ccc;" id="boxImg">
                        <input type="file" id="file_upload" style="display: none;" onchange="action_upload_image('file_upload','src','img');" />
                        <img style="width: 100%; height: 100%;" title="上传图片" id="img" src="../../Images/web/default.png"  onclick="action_open_file('file_upload');" alt="上传图片" />
                        <input id="src" type="hidden" />
                    </div>
                </div>
                <div id="tabs" class="easyui-tabs" style="width: 98%; height: 55%;">
                    <div id="tabs_1" title="详细内容" style="padding: 10px;">
                        <input class="ckeditor" label="" labelposition="left" multiline="true" style="width: 100%; height: 310px;"
                            id="valContent" />
                    </div>
                </div>
                <div style="float: left;">
                    <div style="margin: 5px 0px 10px 0px;" id="boxPublished">
                        <input class="easyui-switchbutton" checked id="chkIsPublished" style="height: 25px;" />&nbsp;立即发布
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
