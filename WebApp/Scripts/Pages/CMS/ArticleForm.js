var url_query = "/Api/Article/Query?adapter=" + none;

var url_get = "/Api/Article/Get";
var url_save = "/Api/Article/Save";
var url_delete = "/Api/Article/Delete";

$(function () {
    close_dialog();
    load_config();
    get_class();
    query_data();
});

function query_data() {
    var url = url_query + "&classCode=" + ClassCode + "&pageNumber=" + page_number + "&pageSize=" + page_size;
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
                $("#valTitle").textbox("setValue", data.Title);
                $("#valShortTitle").textbox("setValue", data.ShortTitle);
                $("#valDescription").textbox("setValue", data.Description);
                $("#valAnthor").textbox("setValue", data.Author);
                $("#valContent").val(data.ArticleContent);
                $("#valCutSrc").textbox("setValue", data.CutPath);
                $("#valKeywords").textbox("setValue", data.Keywords);
                $("#valPublishTime").datetimebox("setValue", data.PublishTime);

                editor1.html(data.ArticleContent);
                $("#src").val(data.PicPath);
                $("#img").attr("src", data.PicPath);

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
    if (!$('#valTitle').textbox("isValid")) return false;

    var valTitle = $('#valTitle').textbox("getValue");
    var valShortTitle = $('#valShortTitle').textbox("getValue");
    var valDescription = $('#valDescription').textbox("getValue");
    var valAnthor = $('#valAnthor').textbox("getValue");
    var valCutSrc = $('#valCutSrc').textbox("getValue");
    var valKeywords = $('#valKeywords').textbox("getValue");
    var valPublishTime = $('#valPublishTime').datebox("getValue");
    var valDescription = $('#valDescription').textbox("getValue");
    var valContent = $("#valContent").val();
    var IsPublished = $("#chkIsPublished").attr("checked") == "checked";
    var src = $("#src").val();

    var entity = new Object();
    entity.ID = id;
    entity.ClassID = classId;
    entity.Title = valTitle;
    entity.ShortTitle = valShortTitle;
    entity.Author = valAnthor;
    entity.Description = valDescription;
    entity.PicPath = src;
    entity.CutPath = valCutSrc;
    entity.ArticleContent = valContent;
    entity.IsPublished = IsPublished;
    entity.PublishTime = valPublishTime;
    entity.Keywords = valKeywords;
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

    $("#valTitle").textbox("setValue", "");
    $("#valShortTitle").textbox("setValue", "");
    $("#valDescription").textbox("setValue", "");
    $("#valAnthor").textbox("setValue", "");
    $("#valContent").val("");
    $("#valArticleSource").textbox("setValue", "");
    $("#valKeywords").textbox("setValue", "");
    $("#valPublishTime").datetimebox("setValue", "");
    $("#src").val("");
    $("#valCutSrc").val("");
    $("#img").attr("src", "../../Images/web/default.png");
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

//根据配置信息生成提交表格
function load_config() {
    var moduleID = getQueryString("ModuleID");
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "../../Api/Web/GetConfig?ModuleID=" + moduleID,
        data: null,
        beforeSend: null,
        success: function (json) {
            var code = json.code;
            if (code == 0) {
                var configData = json.data;
                if (!configData.TitleFlag) {
                    $("#boxTitle").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.TitleCaption != null) {
                    $("#boxTitle label").html(configData.TitleCaption)
                }
                if (!configData.ShortTitleFlag) {
                    $("#boxShortTitle").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.ShortTitleCaption != null) {
                    $("#boxShortTitle label").html(configData.ShortTitleCaption)
                }
                if (!configData.DescriptionFlag) {
                    $("#boxDescription").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.DescriptionCaption != null) {
                    $("#boxDescription label").html(configData.DescriptionCaption)
                }
                if (!configData.CutSrcFlag) {
                    $("#boxCutSrc").css({ "visibility": "hidden", "height": "0" });
                }
                if (!configData.KeywordsFlag) {
                    $("#boxKeywords").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.KeywordsCaption != null) {
                    $("#boxKeywords label").html(configData.KeywordsCaption)
                }
                if (!configData.PublishTimeFlag) {
                    $("#boxPublishTime").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.PublishTimeCaption != null) {
                    $("#boxPublishTime label").html(configData.PublishTimeCaption)
                }
                if (!configData.AuthorFlag) {
                    $("#boxAnthor").css({ "visibility": "hidden", "height": "0" });
                } else if (configData.AuthorCaption != null) {
                    $("#boxAnthor label").html(configData.AuthorCaption)
                }
                if (!configData.PicPathFlag) {
                    $("#boxImg").css({ "visibility": "hidden", "height": "0" });
                    $("#boxWindowRight").css({ "width": "0", "height": "0" });
                    $("#boxWindowLeft").css("width", "100%");
                }

                if (!configData.PublishFlag) {
                    $("#boxPublished").css("visibility", "hidden");
                }
                if (!configData.ArticleContentFlag) {
                    // $("#tabs").css({ "visibility": "hidden", "height": "0" });
                    $("#tabs").attr("style", "width: 0; height: 0; visibility: hidden;");

                } else if (configData.ArticleContentCaption != null) {
                    $("#tabs .tabs-title").html(configData.ArticleContentCaption)
                }

                //if (configData.Width != null) { $(".panel-noscroll .window").width("200"); }
                //if (configData.Height != null) { $("#w").height=configData.Height; }
            }
        }
    });
}
