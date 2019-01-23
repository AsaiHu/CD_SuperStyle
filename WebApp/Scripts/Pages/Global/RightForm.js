var url_query_role = "/Api/Role/Query";
var url_query = "/Api/Right/Query";
var url_save = "/Api/Right/Save";

$(function() {
    close_dialog();
    query_data();
});

function query_data() {
    var url = url_query_role + "?pageNumber=" + page_number + "&pageSize=" + page_size;
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
    var url = url_query + "?roleId=" + id;

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
                $("#tt").tree('loadData', data);
                config_window("w", "设置角色权限", "icon-page-edit");
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
    var rights = new Array();
    var nodes = $('#tt').tree('getChecked');
    for (var i = 0; i < nodes.length; i++) {
        var right_module = new Object();
        right_module.RoleID = id;
        right_module.ModuleID = nodes[i].id;
        rights.push(right_module);
    }

    var url = url_save + "?roleID=" + id;
    var data = String.toSerialize(rights);

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

function reset() {
    id = "-1";
    treeChecked(false, "tt");
}

function open_dialog() {
    $('#w').window('open');
}

function close_dialog() {
    reset();
    $('#w').window('close');
}


function action_edit() {
    var row = $('#datagrid').datagrid('getSelected');
    if (row) {
        id = row.ID;
        set_data();
    }
}

function action_query() {
    query_data();
}