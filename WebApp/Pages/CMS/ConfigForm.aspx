<%@ Page Language="C#"  MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true" CodeBehind="ConfigForm.aspx.cs" Inherits="WebApp.Pages.CMS.ConfigForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/Pages/CMS/ConfigForm.js" type="text/javascript"></script>
    <style type="text/css">
        .switchCommon
        {
            margin-left:10px;
            margin-top:5px;
        }

            .switchCommon span
            {
                height:25px;
            }

                .switchCommon .textbox
                {
                    margin-left:20px;
                    height:20px;
                }
        .switchTitle
        {
            width:80px;
            display:inline-block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',border:false">
            <table class="easyui-datagrid" id="datagrid" style="width: 100%; height: auto;" border="0"
                data-options="
                rownumbers:true,
                striped:true,
                fitColumns: true,
                singleSelect:true,
                rowStyler: function(index,row){
					        if (row.IsLocked){
						        return 'color:red;';
					        }
				        }">
                <thead>
                    <tr>
                        <th data-options="field:'ID',hidden:true">ID
                        </th>
                        <th data-options="field:'DisplayName',width:150">表单名称
                        </th>
                        <th data-options="field:'TitleFlag',width:75,align:'center',formatter:format_check">文章标题
                        </th>
                        <th data-options="field:'ShortTitleFlag',width:75,align:'center',formatter:format_check">短标题
                        </th>
                        <th data-options="field:'DescriptionFlag',width:75,align:'center',formatter:format_check">描述内容
                        </th>
                        <th data-options="field:'CutPathFlag',width:75,align:'center',formatter:format_check">PDF上传
                        </th>
                         <th data-options="field:'KeywordsFlag',width:75,align:'center',formatter:format_check">关键字
                        </th>
                        <th data-options="field:'PublishTimeFlag',width:75,align:'center',formatter:format_check">发布日期
                        </th>
                        <th data-options="field:'AuthorFlag',width:75,align:'center',formatter:format_check">作者
                        </th>
                        <th data-options="field:'PicPathFlag',width:75,align:'center',formatter:format_check">上传图片
                        </th>
                        <th data-options="field:'ArticleContentFlag',width:75,align:'center',formatter:format_check">详细内容
                        </th>
                        <th data-options="field:'PublishFlag',width:75,align:'center',formatter:format_check">立即发布
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


    <div id="w" class="easyui-window" title="" data-options="modal:true,closed:true,iconCls:'',minimizable:false,maximizable:true" style="width: 500px; height: 550px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">
                
                <div class="switchCommon">白色输入框为标签名，可不填</div>               
                <div class="switchCommon"><label class="switchTitle">文章标题</label>&nbsp;<input class="easyui-switchbutton" checked id="titleFlag" /><input class="easyui-textbox" id="titleCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">短标题</label>&nbsp;<input class="easyui-switchbutton" checked id="shortTitleFlag" /><input class="easyui-textbox" id="shortTitleCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">描述内容</label>&nbsp;<input class="easyui-switchbutton" checked id="descriptionFlag" /><input class="easyui-textbox" id="descriptionCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">PDF上传</label>&nbsp;<input class="easyui-switchbutton" checked id="cutSrcFlag" /></div>
                <div class="switchCommon"><label class="switchTitle">关键字</label>&nbsp;<input class="easyui-switchbutton" checked id="keywordsFlag" /><input class="easyui-textbox" id="keywordsCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">发布日期</label>&nbsp;<input class="easyui-switchbutton" checked id="publishTimeFlag" /><input class="easyui-textbox" id="publishTimeCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">作者</label>&nbsp;<input class="easyui-switchbutton" checked id="authorFlag" /><input class="easyui-textbox" id="authorCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">上传图片</label>&nbsp;<input class="easyui-switchbutton" checked id="imgUploadFlag" /></div>
                <div class="switchCommon"><label class="switchTitle">详细内容</label>&nbsp;<input class="easyui-switchbutton" checked id="articleContentFlag" /><input class="easyui-textbox" id="articleContentCaption" /></div>
                <div class="switchCommon"><label class="switchTitle">发布</label>&nbsp;<input class="easyui-switchbutton" checked id="publishFlag" /></div>
            </div>
            <div data-options="region:'south',border:false" style="text-align: right; padding: 5px 0 0 0;">
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" style="width: 55px;" onclick="save_data();">保存</a>
                <a href="#" class="easyui-linkbutton" onclick="close_dialog();" style="width: 55px;">关闭</a>
            </div>
        </div>
    </div>
</asp:Content>

