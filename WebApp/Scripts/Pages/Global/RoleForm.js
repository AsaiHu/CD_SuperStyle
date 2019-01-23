var url_query = "/Api/Role/Query";
var url_get = "/Api/Role/Get";
var url_save = "/Api/Role/Save";
var url_delete = "/Api/Role/Delete";

$(function() {
    close_dialog();
    query_data();
});

function query_data() {
    var url = url_query + "?pageNumber=" + page_number + "&pageSize=" + page_size;
    var data = "keyword=" + $("#input_keyword").val();

    $.ajax({
        type: 'POST',
        dataType: 'json',
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
        data: null,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (json) {
            var code = json.code;
            if (code == success_code) {
                var data = json.data;
                $("#roleCode").textbox("setValue", data.RoleCode);
                $("#roleName").textbox("setValue", data.RoleName);
                $("#roleDesc").textbox("setValue", data.Description);
                config_window("w", "修改角色信息", "icon-page-edit");
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
    if (!$("#roleCode").textbox("isValid") || !$("#roleName").textbox("isValid")) return false;

    var roleCode = $('#roleCode').textbox("getValue");
    var roleName = $('#roleName').textbox("getValue");
    var roleDesc = $('#roleDesc').textbox("getValue");


    var role = new Object();
    role.ID = id;
    role.RoleCode = roleCode;
    role.RoleName = roleName;
    role.Description = roleDesc;
    var url = url_save;
    var data = String.toSerialize(role);

    $.ajax({
        type: 'POST',
        url: url,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        cache: false,
        data: data,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function(res) {
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

function delete_data() {
    $.messager.confirm('提示信息', "确定删除吗？", function(r) {
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

    config_window("w", "创建新角色", "icon-page-add");

    $("#roleCode").textbox("setValue", "");
    $("#roleName").textbox("setValue", "");
    $("#roleDesc").textbox("setValue", "");
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