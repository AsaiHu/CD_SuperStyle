var url_query = "/Api/Character/Query?adapter=" + none;

var url_get = "/Api/Character/Get";
var url_save = "/Api/Character/Save";
var url_delete = "/Api/Character/Delete";

$(function () {
    close_dialog();
    query_data();
});

function query_data() {
    var url = url_query + "&pageNumber=" + page_number + "&pageSize=" + page_size;
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
                $("#valName").textbox("setValue", data.Name);
                $("#valExtendMean").textbox("setValue", data.ExtendMean);
                $("#valRepresentVal").textbox("setValue", data.RepresentVal);
                $("#valSequence").textbox("setValue", data.Sequence);       

                editor1.html(data.Introduce);

                config_window("w", "修改文章信息", "icon-page-edit");
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
    if (!$('#valName').textbox("isValid")) return false;

    var valName = $('#valName').textbox("getValue");
    var valExtendMean = $('#valExtendMean').textbox("getValue");
    var valRepresentVal = $('#valRepresentVal').textbox("getValue");
    var valSequence = $('#valSequence').textbox("getValue");
    
    var valContent = document.getElementsByClassName('ke-edit-iframe')[0].contentWindow.document.getElementsByClassName('ke-content')[0].innerHTML;

    var entity = new Object();
    entity.ID = id;
    entity.Name = valName;
    entity.ExtendMean = valExtendMean;
    entity.RepresentVal = valRepresentVal;
    entity.Sequence = valSequence;
    entity.Introduce = valContent;
    var url = url_save;
    var data = String.toSerialize(entity);

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

    config_window("w", "新建文章信息", "icon-page-add");

    $("#valName").textbox("setValue", "");
    $("#valExtendMean").textbox("setValue", "");
    $("#valRepresentVal").textbox("setValue", "");
    $("#valSequence").textbox("setValue", "");
    editor1.html("");
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
