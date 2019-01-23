<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="ModuleForm.aspx.cs" Inherits="WebApp.Pages.Global.ModuleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/Pages/Global/ModuleForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',border:false">
            <table id="treegrid" class="easyui-treegrid" style="width: 100%; height: auto" border="0"
                data-options="
				rownumbers: true,
				animate: true,
				collapsible: true,
				fitColumns: true,
				idField: 'id',
				treeField: 'name'
			">
                <thead>
                    <tr>
                        <th data-options="field:'id',hidden:true">ID
                        </th>
                        <th data-options="field:'name',width:150">模块名称
                        </th>
                        <th data-options="field:'src',width:200">链接地址
                        </th>
                        <th data-options="field:'sequence',width:75,align:'right'">顺序号
                        </th>
                        <th data-options="field:'visible',width:75,align:'center',formatter:format_check">可见
                        </th>
                        <th data-options="field:'add',width:75,align:'center',formatter:format_check">新增
                        </th>
                        <th data-options="field:'update',width:75,align:'center',formatter:format_check">修改
                        </th>
                        <th data-options="field:'delete',width:75,align:'center',formatter:format_check">删除
                        </th>
                        <th data-options="field:'submit',width:75,align:'center',formatter:format_check">提交
                        </th>
                        <th data-options="field:'query',width:75,align:'center',formatter:format_check">查询
                        </th>
                        <th data-options="field:'updatetime',width:150">最后修改时间
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="w" class="easyui-window" title="" data-options="modal:true,closed:true,iconCls:'',minimizable:false,maximizable:true" style="width: 500px; height: 550px; padding: 5px; overflow: hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div style="padding: 5px;" data-options="region:'center'">
                <div id="tabs" class="easyui-tabs" style="width: 100%; height: 100%;">
                    <div id="tabs_1" title="基本信息" style="padding: 10px">
                        <div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="模块名称:" labelposition="left" style="width: 90%;"
                                    id="moduleName" data-options="required:true" />
                            </div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-combotree" label="上级模块" labelposition="left" style="width: 90%;"
                                    id="upperModule" data-options="
					                valueField: 'id',
					                textField: 'text',
                                    editable:false 
					                " />
                            </div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="链接地址:" labelposition="left" style="width: 90%;"
                                    id="moduleSrc" />
                            </div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="图标路径:" labelposition="left" style="width: 90%;"
                                    id="moduleUrl" data-options="required:true" />
                            </div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-numberbox" label="顺序号:" labelposition="left" style="width: 90%;"
                                    id="moduleSeq" data-options="required:true" />
                            </div>

                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="描述信息:" labelposition="left" multiline="true" style="width: 90%; height: 60px;"
                                    id="moduleDesc" />
                            </div>
                            <div style="margin: 10px 0px 10px 10px;">
                                <input class="easyui-textbox" label="备注信息:" labelposition="left" multiline="true" style="width: 90%; height: 60px;"
                                    id="moduleNotes" />
                            </div>
                            <div style="margin: 0px 0px 10px 10px;">
                                <div style="width: 85px; float: left;">其他:</div>
                                <div style="float: left;">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 34%; padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="hide" style="height: 25px;" />&nbsp;可见
                                            </td>
                                            <td style="width: 33%; padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="refreshFlag" style="height: 25px;" />&nbsp;刷新
                                            </td>
                                            <td style="width: 33%; padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="addFlag" style="height: 25px;" />&nbsp;添加
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="updateFlag" style="height: 25px;" />&nbsp;修改 
                                            </td>
                                            <td style="padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="deleteFlag" style="height: 25px;" />&nbsp;删除
                                            </td>
                                            <td style="padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="submitFlag" style="height: 25px;" />&nbsp;提交
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="queryFlag" style="height: 25px;" />&nbsp;查询
                                            </td>
                                            <td style="padding-top: 5px;">
                                                <input class="easyui-switchbutton" checked id="keywordFlag" style="height: 25px;" />&nbsp;关键字
                                            </td>
                                            <td style="padding-top: 5px;"></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div id="tabs_2" title="调用方法" style="padding: 10px">
                        <div>
                            <div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“刷新”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="refreshFunc" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“添加”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="addFunc" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“修改”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="updateFunc" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“删除”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="deleteFunc" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“提交”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="submitFunc" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“查询”功能调用方法名:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="queryFunc" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs_3" title="按钮图标" style="padding: 10px">
                        <div>
                            <div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“刷新”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="refreshIcon" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“添加”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="addIcon" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“修改”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="updateIcon" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“删除”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="deleteIcon" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“提交”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="submitIcon" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“查询”按钮图标:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="queryIcon" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs_4" title="按钮文本" style="padding: 10px">
                        <div>
                            <div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“刷新”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="refreshCaption" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“添加”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="addCaption" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“修改”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="updateCaption" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“删除”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="deleteCaption" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“提交”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="submitCaption" />
                                </div>
                                <div style="padding-top: 5px;">
                                    <input class="easyui-textbox" label="“查询”按钮显示文本:" labelposition="top" style="width: 100%; height: 50px;"
                                        id="queryCaption" />
                                </div>
                            </div>
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
