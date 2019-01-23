var url_query = "/Api/Config/Query";
var url_get = "/Api/Config/Get";
var url_save = "/Api/Config/Save";
var url_delete = "/Api/Config/Delete";
var ModuleID;

$(function () {

    close_dialog();
    
    query_data();
});

function query_data() {
    var url = url_query +  "?pageNumber=" + page_number + "&pageSize=" + page_size;
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
                if (data.TitleFlag)
                    $('#titleFlag').switchbutton("check");
                else
                    $('#titleFlag').switchbutton("uncheck");
                if (data.ShortTitleFlag)
                    $('#shortTitleFlag').switchbutton("check");
                else
                    $('#shortTitleFlag').switchbutton("uncheck");
                if (data.DescriptionFlag)
                    $('#descriptionFlag').switchbutton("check");
                else
                    $('#descriptionFlag').switchbutton("uncheck");
                if (data.CutPathFlag)
                    $('#cutSrcFlag').switchbutton("check");
                else
                    $('#cutSrcFlag').switchbutton("uncheck");
                if (data.KeywordsFlag)
                    $('#keywordsFlag').switchbutton("check");
                else
                    $('#keywordsFlag').switchbutton("uncheck");
                if (data.PublishTimeFlag)
                    $('#publishTimeFlag').switchbutton("check");
                else
                    $('#publishTimeFlag').switchbutton("uncheck");
                if (data.AuthorFlag)
                    $('#authorFlag').switchbutton("check");
                else
                    $('#authorFlag').switchbutton("uncheck");
                if (data.PicPathFlag)
                    $('#imgUploadFlag').switchbutton("check");
                else
                    $('#imgUploadFlag').switchbutton("uncheck");
                if (data.ArticleContentFlag)
                    $('#articleContentFlag').switchbutton("check");
                else
                    $('#articleContentFlag').switchbutton("uncheck");
                if (data.PublishFlag)
                    $('#publishFlag').switchbutton("check");
                else
                    $('#publishFlag').switchbutton("uncheck");

                //$('#moduleId').textbox("setValue", data.ModuleID);
                ModuleID = data.ModuleID;
                $('#titleCaption').textbox("setValue", data.TitleCaption);
                $('#shortTitleCaption').textbox("setValue", data.ShortTitleCaption);
                $('#descriptionCaption').textbox("setValue", data.DescriptionCaption);
                $('#keywordsCaption').textbox("setValue", data.KeywordsCaption);
                $('#publishTimeCaption').textbox("setValue", data.PublishTimeCaption);
                $('#authorCaption').textbox("setValue", data.AuthorCaption);
                $('#articleContentCaption').textbox("setValue", data.ArticleContentCaption);

                config_window("w", "修改配置信息", "icon-page-edit");
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
   
    var titleFlag = $('#titleFlag').switchbutton('options').checked;
    var shortTitleFlag = $('#shortTitleFlag').switchbutton('options').checked;
    var descriptionFlag = $('#descriptionFlag').switchbutton('options').checked;
    var cutSrcFlag = $('#cutSrcFlag').switchbutton('options').checked;
    var keywordsFlag = $('#keywordsFlag').switchbutton('options').checked;
    var publishTimeFlag = $('#publishTimeFlag').switchbutton('options').checked;
    var authorFlag = $('#authorFlag').switchbutton('options').checked;
    var imgUploadFlag = $('#imgUploadFlag').switchbutton('options').checked;
    var articleContentFlag = $('#articleContentFlag').switchbutton('options').checked;
    var publishFlag = $('#publishFlag').switchbutton('options').checked;
    var titleCaption = $('#titleCaption').textbox('getValue');
    var shortTitleCaption = $('#shortTitleCaption').textbox('getValue');
    var descriptionCaption = $('#descriptionCaption').textbox('getValue');
    var keywordsCaption = $('#keywordsCaption').textbox('getValue');
    var publishTimeCaption = $('#publishTimeCaption').textbox('getValue');
    var authorCaption = $('#authorCaption').textbox('getValue');
    var articleContentCaption = $('#articleContentCaption').textbox('getValue');
    //var moduleId = $('#moduleId').textbox('getValue');

    var config = new Object();
    config.ID = id;
    config.ModuleID =parseInt(ModuleID);
    config.TitleFlag = titleFlag;
    config.ShortTitleFlag = shortTitleFlag;
    config.DescriptionFlag = descriptionFlag;
    config.CutPathFlag = cutSrcFlag;
    config.KeywordsFlag = keywordsFlag;
    config.PublishTimeFlag = publishTimeFlag;
    config.AuthorFlag = authorFlag;
    config.PicPathFlag = imgUploadFlag;
    config.ArticleContentFlag = articleContentFlag;
    config.PublishFlag = publishFlag;
    config.TitleCaption = titleCaption;
    config.ShortTitleCaption = shortTitleCaption;
    config.DescriptionCaption = descriptionCaption;
    config.KeywordsCaption = keywordsCaption;
    config.AuthorCaption = authorCaption;
    config.PublishTimeCaption = publishTimeCaption;
    config.ArticleContentCaption = articleContentCaption;
    
    var url = url_save;
    var data = String.toSerialize(config);

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

    config_window("w", "新建配置信息", "icon-page-add");

    $('#titleFlag').switchbutton("uncheck");
    $('#shortTitleFlag').switchbutton("uncheck");
    $('#DescriptionFlag').switchbutton("uncheck");
    $('#cutSrcFlag').switchbutton("uncheck");
    $('#keywordsFlag').switchbutton("uncheck");
    $('#publishTimeFlag').switchbutton("uncheck");
    $('#authorFlag').switchbutton("uncheck");
    $('#imgUploadFlag').switchbutton("uncheck");
    $('#articleContentFlag').switchbutton("uncheck");
    $('#publishFlag').switchbutton("uncheck");
    $("#titleCaption").textbox("setValue", "");
    $("#shortTitleCaption").textbox("setValue", "");
    $("#descriptionCaption").textbox("setValue", "");
    $("#keywordsCaption").textbox("setValue", "");
    $("#publishTimeCaption").textbox("setValue", "");
    $("#authorCaption").textbox("setValue", "");
    $("#articleContentCaption").textbox("setValue", "");
   // $('#moduleId').textbox("setValue", "");
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

