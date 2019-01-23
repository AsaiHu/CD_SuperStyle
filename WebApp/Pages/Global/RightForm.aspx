<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="RightForm.aspx.cs" Inherits="WebApp.Pages.Global.RightForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/Pages/Global/RightForm.js" type="text/javascript"></script>

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
                        <th data-options="field:'RoleCode',width:150">角色代码
                        </th>
                        <th data-options="field:'RoleName',width:150">角色名称
                        </th>
                        <th data-options="field:'Description',width:350">描述
                        </th>
                        <th data-options="field:'UpdateTime',width:150">最后修改时间
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
                <ul id="tt" class="easyui-tree" data-options="url:'/Data/tree_data1.json',method:'get',animate:true,checkbox:true">
                </ul>
            </div>
            <div data-options="region:'south',border:false" style="text-align: right; padding: 5px 0 0 0;">
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" style="width: 55px;" onclick="save_data();">保存</a>
                <a href="#" class="easyui-linkbutton" onclick="close_dialog();" style="width: 55px;">关闭</a>
            </div>
        </div>
    </div>
</asp:Content>
