<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="WebApp.Pages.MainForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>网站信息发布系统</title>
    <link rel="stylesheet" type="text/css" href="/Styles/default.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/easyui.1.5.1/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/easyui.1.5.1/themes/icon.css" />

    <script type="text/javascript" src="/Scripts/easyui.1.5.1/jquery-1.7.2.min.js"></script>

    <script type="text/javascript" src="/Scripts/easyui.1.5.1/jquery.easyui.min.js"></script>

    <script type="text/javascript" src="/Scripts/easyui.1.5.1/locale/easyui-lang-zh_CN.js"></script>

    <script type="text/javascript" src="/Scripts/ExternFunc.js"></script>

    <script type="text/javascript" src="/Scripts/EasyUI.js"></script>
    <script type="text/javascript" src="/Scripts/ExternFunc.js"></script>
    <script src="/Scripts/Cookie.js" type="text/javascript"></script>
    <script type="text/javascript" src='/Scripts/Pages/MainForm.js'> </script>
    <script type="text/javascript" language="javascript">
        var success_code = "<%=(int)DAL.Enum_StatusCode.Success %>";
        var success_name = "<%=DAL.Util.GetDisplayName(DAL.Enum_StatusCode.Success) %>";
        var fail_code = "<%=(int)DAL.Enum_StatusCode.Fail %>";
        var fail_name = "<%=DAL.Util.GetDisplayName(DAL.Enum_StatusCode.Fail) %>";
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden" fit="true" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px; width: 100%; background: white; text-align: center;">
            <img src="/images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2; -moz-opacity: 0.7; opacity: .70; z-index: 20000;">
        <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
            <img src="/images/loading.gif" align="absmiddle" alt="" />
            正在加载中,请稍候...
        </div>
    </div>

    <div region="north" split="false" border="false" style="overflow: hidden; font-family: Verdana, 微软雅黑,黑体;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="qy_top2bg">
            <tr>
                <td style="width: 350px; height: 50px; text-align: left; "><img alt="" src="/Images/logo_top.png" style="height:36px;padding-left:15px;"/></td>
                <td style="text-align: right; float: right;">
                    <table style="margin-top: 5px;" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <a href="javascript:void(0);" target="_parent" style="width: 100px; height: 40px; display: block; background-image: url(/images/qyxx_r3_c21.png); background-repeat: no-repeat; background-position: center center;"></a>
                            </td>
                            <td>
                                <a href="javascript:void(0);" onclick="openPwd();" style="width: 100px; height: 40px; display: block; background-image: url(/images/qyxx_r3_c22.png); background-repeat: no-repeat; background-position: center center;"></a>
                            </td>
                            <td>
                                <a href="javascript:void(0);" target="_parent" onclick="logOut();" style="width: 100px; height: 40px; display: block; background-image: url(/images/qyxx_r3_c23.png); background-repeat: no-repeat; background-position: center center;"></a>
                            </td>
                            <td>
                                <a href="javascript:void(0);" target="_parent" onclick="exit();" style="width: 100px; height: 40px; display: block; background-image: url(/images/qyxx_r3_c24.png); background-repeat: no-repeat; background-position: center center;"></a>
                            </td>
                            <td>
                                <a href="javascript:void(0);" target="_parent" style="width: 100px; height: 40px; display: block; background-image: url(/images/qyxx_r3_c25.png); background-repeat: no-repeat; background-position: center center;"></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #e6eef8; height: 30px; border-top: 1px solid #95B8E7;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="27" align="left" valign="middle" style="width: 880px">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="width: 25px; text-align: right; vertical-align: middle;">
                                            <img src="/images/340.gif" width="16" height="16" alt="" />
                                        </td>
                                        <td class="qy_topk2px">当前用户：<span style="color: Red;"><%=Utilities.Security.CurrentUser.UserID %></span>&nbsp;&nbsp;&nbsp;<span style="color: Blue;"><%=Utilities.Security.CurrentUser.UserName %></span>
                                        </td>
                                        <td style="padding-left: 5px; padding-right: 5px;">
                                            <div class='datagrid-btn-separator'></div>
                                        </td>
                                        <td>
                                            <table width="266" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="21" align="left" valign="middle">
                                                        <img src="/images/320.gif" width="16" height="16" alt="" />
                                                    </td>
                                                    <td align="left" valign="middle" class="qy_date">今天是：<span id="liveclock"></span>
                                                        <script language="javascript" type="text/javascript">
                                                            setInterval("timePrint();", 1000);
                                                        </script>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" valign="middle" class="qy_menurighbg"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div region="south" split="false" style="height: 25px; background: #D2E0F2;">
        <div class="footer">
            苏州威博世网络科技有限公司-版权所有
        </div>
    </div>
    <div region="west" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用" style="padding: 20px; overflow: hidden; color: red;">
                <form id="form1" runat="server">
                    <asp:HiddenField ID="hidKeyVerify" runat="server" />
                </form>
            </div>
        </div>
    </div>
    <!--修改密码窗口-->
    <div id="w" class="easyui-window" title="修改密码" collapsible="false" minimizable="false"
        maximizable="false" icon="icon-save" style="padding: 5px; background: #fafafa;"
        data-options="
        width: 300,
        modal: true,
        shadow: true,
        closed: true,
        height: 160,
        resizable: false">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <div>
                    <input class="easyui-passwordbox" label="新密码:" labelposition="left" style="width: 92%; height: 25px;"
                        id="userPwd" data-options="required:true,validType:'length[6,12]'">
                </div>
                <div style="padding-top: 10px;">
                    <input class="easyui-passwordbox" label="确认密码:" labelposition="left" style="width: 92%; height: 25px;"
                        id="confirmPwd" validtype="confirmPass['#userPwd']" data-options="required:true">
                </div>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0);" onclick="submitPwd();">确定</a> <a class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0);"
                    onclick="closePwd();">取消</a>
            </div>
        </div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="tabupdate">
            刷新
        </div>
        <div class="menu-sep">
        </div>
        <div id="close">
            关闭
        </div>
        <div id="closeall">
            全部关闭
        </div>
        <div id="closeother">
            除此之外全部关闭
        </div>
        <div class="menu-sep">
        </div>
        <div id="closeright">
            当前页右侧全部关闭
        </div>
        <div id="closeleft">
            当前页左侧全部关闭
        </div>
        <%--<div class="menu-sep">
        </div>
       <div id="exit">
            退出</div>--%>
    </div>
</body>
</html>
