<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="UserForm.aspx.cs" Inherits="WebApp.Pages.Global.UserForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/Pages/Global/UserForm.js" type="text/javascript"></script>

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
                        <th data-options="field:'UserID',width:100">登录名
                        </th>
                        <th data-options="field:'UserName',width:150">用户名
                        </th>
                        <th data-options="field:'Telephone',width:120">联系电话
                        </th>
                        <th data-options="field:'Email',width:150">电子邮箱
                        </th>
                        <th data-options="field:'Description',width:200">描述
                        </th>
                        <th data-options="field:'UpdateTime',width:150">最后修改时间
                        </th>
                    </tr>
                </thead>
            </table>
        </div>

        <div data-options="region:'south',border:false" style="height: 60px;">
            <div id="pagination" class="easyui-pagination" style="background: #efefef; border: 1px solid #ccc;"
                data-options="onSelectPage: function(pageNumber, pageSize){page_number=pageNumber;page_size=pageSize;query_data();$('#content').panel('refresh', url_query+'&pageNumber='+pageNumber+'&pageSize='+pageSize);}">
            </div>
        </div>
    </div>

    
    <div id="w" class="easyui-window" title="" data-options="modal:true,closed:true,iconCls:'',minimizable:false,maximizable:true" style="width: 400px; height: 410px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding:5px;" data-options="region:'center'">
                <div id="tabs" class="easyui-tabs" style="width: 100%; height: 100%;">
                    <div id="tabs_1" title="基本信息" style="padding: 5px;">
                        <div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="登录名:" labelposition="left" style="width: 90%;"
                                    id="userId" data-options="required:true,validType:'length[3,10]'" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <input class="easyui-textbox" label="用户名:" labelposition="left" style="width: 90%;"
                                    id="userName" data-options="required:true" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <input class="easyui-passwordbox" label="密码:" labelposition="left" style="width: 90%;"
                                    id="userPwd" data-options="required:true,validType:'length[6,12]'" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <input class="easyui-passwordbox" label="重复密码:" labelposition="left" style="width: 90%;"
                                    id="confirmPwd" validtype="confirmPass['#userPwd']" data-options="required:true" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <input class="easyui-textbox" label="联系电话:" labelposition="left" style="width: 90%;"
                                    id="userTel" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <input class="easyui-textbox" label="电子邮箱:" labelposition="left" style="width: 90%;"
                                    id="userEmail" data-options="required:false,validType:'email'" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <div style="width: 85px; float: left;">其他:</div>
                                <div style="float: left;">
                                    <div>
                                        <input class="easyui-switchbutton" checked id="userLocked" style="height: 25px;" />&nbsp;锁定当前账号
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <input class="easyui-switchbutton" checked id="changePwd" style="height: 25px;" />&nbsp;首次登陆系统必须修改密码
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div title="隶属于" data-options="iconCls:'icon-role',animate:true" style="padding: 5px;">
                        <div class="easyui-datalist" id="userRoles" title="" style="width: 100%; height: 100%;"
                            data-options="
			                border:0,
                            url: '',
			                method: 'get',
			                checkbox: true,
			                selectOnCheck: false,
			                onBeforeSelect: function(){return false;}
			                ">
                        </div>
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
