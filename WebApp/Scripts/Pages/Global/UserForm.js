var url_query = "/Api/User/Query";
var url_role_query = "/Api/Role/Query?Adapter=" + datalist;
var url_get = "/Api/User/Get";
var url_save = "/Api/User/Save";
var url_delete = "/Api/User/Delete";

$(function () {
    close_dialog();
    query_data();
});

function query_data() {
    var url = url_query + "?pageNumber=" + page_number + "&pageSize=" + page_size;
    var data = "keyword=" + $("#input_keyword").val();

    $.ajax({
        type: 'POST',
        dataType:'json',
        url: url,
        data: data,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (json) {
            var code = json.code;

            if (code == success_code) {
                
                var data = json.data;
                var total_count = json.total_count;

                var datasource = { rows: data };
                $("#datagrid").datagrid('loadData', datasource);
                $("#pagination").pagination({
                    total: total_count,
                    pageSize: page_size
                });

                query_role_data();
            }
            else {
                var message = json.message;
                $.messager.alert('提示信息', message, 'error');
            }

        },
        error: function (e) {
        },
        complete: function () {
            loaded();
        }
    });
}

function query_role_data() {
    var url = url_role_query;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: url,
        data: null,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (json) {
            var code = json.code;

            if (code == success_code) {
                var data = json.data;
                $("#userRoles").datalist('loadData', data);
            }
            else {
                var message = json.message;
                $.messager.alert('提示信息', message, 'error');
            }
        },
        error: function (e) {
        },
        complete: function () {
            loaded();
        }
    });
}

function set_data() {
    var url = url_get + "?id=" + id;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: url,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (json) {
            var code = json.code;
            if (code == success_code) {
                var data = json.data;

                var user = data.User;
                var roles = data.Roles;

                $("#userId").textbox("setValue", user.UserID);
                $("#userName").textbox("setValue", user.UserName);
                $("#userPwd").passwordbox("setValue", user.Password);
                $("#confirmPwd").passwordbox("setValue", user.Password);
                $("#userTel").textbox("setValue", user.Telephone);
                $("#userEmail").textbox("setValue", user.Email);
                if (user.IsLocked == 1)
                    $('#userLocked').switchbutton("check");
                else
                    $('#userLocked').switchbutton("uncheck");
                if (user.IsChangePwd == 1)
                    $('#changePwd').switchbutton("check");
                else
                    $('#changePwd').switchbutton("uncheck");

                var rows = $('#userRoles').datalist('getRows');
                for (var i = 0; i < rows.length; i++) {
                    for (var n = 0; n < roles.length; n++) {
                        if (rows[i].id == roles[n].RoleID) {
                            $('#userRoles').datalist("checkRow", i);
                        }
                    }
                }
                config_window("w", "修改用户信息", "icon-page-edit");
                open_dialog();
            }
            else {
                var message = json.message;
                $.messager.alert('提示信息', message, 'error');
            }
        },
        error: function (e) {
        },
        complete: function () {
            loaded();
        }
    });
}

function save_data() {
    if (!$("#userId").textbox("isValid") || !$("#userName").textbox("isValid") || !$("#userPwd").textbox("isValid") || !$("#confirmPwd").textbox("isValid")) return false;

    var userId = $('#userId').textbox("getValue");
    var userName = $('#userName').textbox("getValue");
    var userPwd = $('#userPwd').textbox("getValue");
    var userTel = $('#userTel').textbox("getValue");
    var userEmail = $('#userEmail').textbox("getValue");
    var userLocked = $('#userLocked').switchbutton('options').checked ? 1 : 0;
    var changePwd = $('#changePwd').switchbutton('options').checked ? 1 : 0;

    var user = new Object();
    user.ID = id;
    user.UserID = userId;
    user.UserName = userName;
    user.Password = userPwd;
    user.Telephone = userTel;
    user.Email = userEmail;
    user.IsLocked = userLocked;
    user.IsChangePwd = changePwd;

    var roles = new Array();
    var rows = $('#userRoles').datalist('getChecked');
    for (var i = 0; i < rows.length; i++) {
        var role = new Object();
        role.RoleID = rows[i].id;
        role.UserID = id;
        roles.push(role);
    }

    user.Roles = roles;

    var url = url_save;
    var data = String.toSerialize(user);

    $.ajax({
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        cache: false,
        url: url,
        data: data,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            close_dialog();
            var code = res.code;
            if (code == success_code) {
                query_data();
            }
            if (code == success_code) {
                var message = res.data;
                $.messager.alert('提示信息', message, 'info');
            }
            else {
                var message = res.message;
                $.messager.alert('提示信息', message, 'error');
            }
        },
        error: function (e) {
        },
        complete: function () {
            loaded();
        }
    });
}

function delete_data() {
    $.messager.confirm('提示信息', "确定删除吗？", function (r) {
        if (r) {
            var url = url_delete + "?id=" + id;
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: url,
                data: null,
                beforeSend: function (XHR) {
                    loading();
                    XHR.setRequestHeader('Ticket', ticket);
                },
                success: function (json) {
                    var code = json.code;
                    if (code == success_code)
                        query_data();

                    if (code == success_code) {
                        var message = json.data;
                        $.messager.alert('提示信息', message, 'info');
                    }
                    else {
                        var message = json.message;
                        $.messager.alert('提示信息', message, 'error');
                    }

                },
                error: function (e) {
                },
                complete: function () {
                    loaded();
                }
            });
        }
    });

}


function reset() {
    id = "-1";

    config_window("w", "创建新用户", "icon-page-add");

    $("#userId").textbox("setValue", "");
    $("#userName").textbox("setValue", "");
    $("#userPwd").passwordbox("setValue", "");
    $("#confirmPwd").passwordbox("setValue", "");
    $("#userTel").textbox("setValue", "");
    $("#userEmail").textbox("setValue", "");
    $('#userLocked').switchbutton("uncheck");
    $('#changePwd').switchbutton("uncheck");
    $('#userRoles').datalist("uncheckAll");
    $("#tabs").tabs("select", 0);
}

function open_dialog() {
    $('#w').window('open');
}

function close_dialog() {
    $('#w').window('close');
}

function action_add() {
    reset();
    open_dialog();
}

function action_edit() {
    reset();
    var row = $('#datagrid').datagrid('getSelected');
    if (row) {
        id = row.ID;
        set_data();
    }
}

function action_delete() {
    var row = $('#datagrid').datagrid('getSelected');
    if (row) {
        id = row.ID;
        delete_data();
    }
}

function action_query() {
    query_data();
}
