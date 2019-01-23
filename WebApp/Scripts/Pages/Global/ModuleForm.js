var url_query = "/Api/Module/Query?Adapter=" + treegrid + "|" + combotree;
var url_get = "/Api/Module/Get";
var url_save = "/Api/Module/Save";
var url_delete = "/Api/Module/Delete";

$(function () {
    close_dialog();
    query_data();
});

function query_data() {
    var url = url_query;

    $.ajax({
        url: url,
        cache: false,
        data: null,
        type: "POST",
        dataType: 'json',
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (json) {
            var code = json.code;
            if (code == success_code) {
                var data = json.data;
                var module_data2 = data.ModuleData2;
                var datasource = { rows: module_data2 };
                $("#treegrid").treegrid('loadData', datasource);

                var module_data3 = data.ModuleData3;
                $("#upperModule").combotree("loadData", module_data3);
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
                $("#moduleName").textbox("setValue", data.DisplayName);
                $("#upperModule").combotree("setValue", data.UpperModuleID);
                $("#moduleSrc").textbox("setValue", data.Src);
                $("#moduleUrl").textbox("setValue", data.ImageUrl);
                $("#moduleSeq").textbox("setValue", data.Sequence);
                if (!data.Hide) {
                    $('#hide').switchbutton("check");
                    set_ctrl_enabled(!data.Hide);

                    if (data.RefreshFlag)
                        $('#refreshFlag').switchbutton("check");
                    else
                        $('#refreshFlag').switchbutton("uncheck");
                    if (data.AddFlag)
                        $('#addFlag').switchbutton("check");
                    else
                        $('#addFlag').switchbutton("uncheck");
                    if (data.UpdateFlag)
                        $('#updateFlag').switchbutton("check");
                    else
                        $('#updateFlag').switchbutton("uncheck");
                    if (data.DeleteFlag)
                        $('#deleteFlag').switchbutton("check");
                    else
                        $('#deleteFlag').switchbutton("uncheck");
                    if (data.SubmitFlag)
                        $('#submitFlag').switchbutton("check");
                    else
                        $('#submitFlag').switchbutton("uncheck");
                    if (data.QueryFlag)
                        $('#queryFlag').switchbutton("check");
                    else
                        $('#queryFlag').switchbutton("uncheck");
                    if (data.KeywordFlag)
                        $('#keywordFlag').switchbutton("check");
                    else
                        $('#keywordFlag').switchbutton("uncheck");

                    $('#refreshFunc').textbox("setValue", data.RefreshFunc);
                    $('#addFunc').textbox("setValue", data.AddFunc);
                    $('#updateFunc').textbox("setValue", data.UpdateFunc);
                    $('#deleteFunc').textbox("setValue", data.DeleteFunc);
                    $('#submitFunc').textbox("setValue", data.SubmitFunc);
                    $('#queryFunc').textbox("setValue", data.QueryFunc);

                    $('#refreshIcon').textbox("setValue", data.RefreshIcon);
                    $('#addIcon').textbox("setValue", data.AddIcon);
                    $('#updateIcon').textbox("setValue", data.UpdateIcon);
                    $('#deleteIcon').textbox("setValue", data.DeleteIcon);
                    $('#submitIcon').textbox("setValue", data.SubmitIcon);
                    $('#queryIcon').textbox("setValue", data.QueryIcon);

                    $('#refreshCaption').textbox("setValue", data.RefreshCaption);
                    $('#addCaption').textbox("setValue", data.AddCaption);
                    $('#updateCaption').textbox("setValue", data.UpdateCaption);
                    $('#deleteCaption').textbox("setValue", data.DeleteCaption);
                    $('#submitCaption').textbox("setValue", data.SubmitCaption);
                    $('#queryCaption').textbox("setValue", data.QueryCaption);
                }
                else {
                    $('#hide').switchbutton("uncheck");
                    set_ctrl_enabled(!data.Hide);
                }
                config_window("w", "修改模块信息", "icon-page-edit");
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
    if (!$("#moduleName").textbox("isValid") || !$("#upperModule").textbox("isValid")
            || !$("#moduleUrl").textbox("isValid") || !$("#moduleSeq").textbox("isValid")) return false;

    var moduleName = $('#moduleName').textbox("getValue");
    var upperModule = $('#upperModule').combotree("getValue");
    if (upperModule == "") upperModule = null;
    var moduleSrc = $('#moduleSrc').textbox("getValue");
    var moduleUrl = $('#moduleUrl').textbox("getValue");
    var moduleSeq = $('#moduleSeq').textbox("getValue");
    var moduleDesc = $('#moduleDesc').textbox("getValue");
    var moduleNotes = $('#moduleNotes').textbox("getValue");
    var hide = $('#hide').switchbutton('options').checked;
    var refreshFlag = $('#refreshFlag').switchbutton('options').checked;
    var addFlag = $('#addFlag').switchbutton('options').checked;
    var updateFlag = $('#updateFlag').switchbutton('options').checked;
    var deleteFlag = $('#deleteFlag').switchbutton('options').checked;
    var submitFlag = $('#submitFlag').switchbutton('options').checked;
    var queryFlag = $('#queryFlag').switchbutton('options').checked;
    var keywordFlag = $('#keywordFlag').switchbutton('options').checked;

    var refreshFunc = $('#refreshFunc').textbox('getValue');
    var addFunc = $('#addFunc').textbox('getValue');
    var updateFunc = $('#updateFunc').textbox('getValue');
    var deleteFunc = $('#deleteFunc').textbox('getValue');
    var submitFunc = $('#submitFunc').textbox('getValue');
    var queryFunc = $('#queryFunc').textbox('getValue');

    var refreshIcon = $('#refreshIcon').textbox('getValue');
    var addIcon = $('#addIcon').textbox('getValue');
    var updateIcon = $('#updateIcon').textbox('getValue');
    var deleteIcon = $('#deleteIcon').textbox('getValue');
    var submitIcon = $('#submitIcon').textbox('getValue');
    var queryIcon = $('#queryIcon').textbox('getValue');

    var refreshCaption = $('#refreshCaption').textbox('getValue');
    var addCaption = $('#addCaption').textbox('getValue');
    var updateCaption = $('#updateCaption').textbox('getValue');
    var deleteCaption = $('#deleteCaption').textbox('getValue');
    var submitCaption = $('#submitCaption').textbox('getValue');
    var queryCaption = $('#queryCaption').textbox('getValue');

    var module = new Object();
    module.ID = id;
    module.DisplayName = moduleName;
    module.UpperModuleID = upperModule;
    module.Src = moduleSrc;
    module.ImageUrl = moduleUrl;
    module.Sequence = moduleSeq;
    module.Description = moduleDesc;
    module.Notes = moduleNotes;
    module.Hide = hide;
    module.RefreshFlag = refreshFlag;
    module.AddFlag = addFlag;
    module.UpdateFlag = updateFlag;
    module.DeleteFlag = deleteFlag;
    module.SubmitFlag = submitFlag;
    module.QueryFlag = queryFlag;
    module.KeywordFlag = keywordFlag;

    module.RefreshFunc = refreshFunc;
    module.AddFunc = addFunc;
    module.UpdateFunc = updateFunc;
    module.DeleteFunc = deleteFunc;
    module.SubmitFunc = submitFunc;
    module.QueryFunc = queryFunc;

    module.RefreshIcon = refreshIcon;
    module.AddIcon = addIcon;
    module.UpdateIcon = updateIcon;
    module.DeleteIcon = deleteIcon;
    module.SubmitIcon = submitIcon;
    module.QueryIcon = queryIcon;

    module.RefreshCaption = refreshCaption;
    module.AddCaption = addCaption;
    module.UpdateCaption = updateCaption;
    module.DeleteCaption = deleteCaption;
    module.SubmitCaption = submitCaption;
    module.QueryCaption = queryCaption;

    var url = url_save;
    var data = String.toSerialize(module);

    $.ajax({
        type: 'POST',
        dataType: 'json',
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

function set_ctrl_enabled(value) {
    if (!value) {
        $('#refreshFlag').switchbutton("uncheck");
        $('#addFlag').switchbutton("uncheck");
        $('#updateFlag').switchbutton("uncheck");
        $('#deleteFlag').switchbutton("uncheck");
        $('#submitFlag').switchbutton("uncheck");
        $('#queryFlag').switchbutton("uncheck");
        $('#keywordFlag').switchbutton("uncheck");

        $('#refreshFlag').switchbutton({ disabled: "disabled" });
        $('#addFlag').switchbutton({ disabled: "disabled" });
        $('#updateFlag').switchbutton({ disabled: "disabled" });
        $('#deleteFlag').switchbutton({ disabled: "disabled" });
        $('#submitFlag').switchbutton({ disabled: "disabled" });
        $('#queryFlag').switchbutton({ disabled: "disabled" });
        $('#keywordFlag').switchbutton({ disabled: "disabled" });
    }
    else {
        $('#refreshFlag').switchbutton({ disabled: "" });
        $('#addFlag').switchbutton({ disabled: "" });
        $('#updateFlag').switchbutton({ disabled: "" });
        $('#deleteFlag').switchbutton({ disabled: "" });
        $('#submitFlag').switchbutton({ disabled: "" });
        $('#queryFlag').switchbutton({ disabled: "" });
        //$('#keywordFlag').switchbutton({ disabled: "" });
    }
}

function set_refresh_enabled(value) {
    if (!value) {
        $('#refreshFunc').textbox({ disabled: "disabled" });
        $('#refreshIcon').textbox({ disabled: "disabled" });
        $('#refreshCaption').textbox({ disabled: "disabled" });

        if ($('#refreshFunc').textbox("getValue") == "")
            $('#refreshFunc').textbox("setValue", "action_refresh();");
        if ($('#refreshIcon').textbox("getValue") == "")
            $('#refreshIcon').textbox("setValue", "icon-reload");
        if ($('#refreshCaption').textbox("getValue") == "")
            $('#refreshCaption').textbox("setValue", "刷新");
    }
    else {
        $('#refreshFunc').textbox({ disabled: "" });
        $('#refreshIcon').textbox({ disabled: "" });
        $('#refreshCaption').textbox({ disabled: "" });

        if ($('#refreshFunc').textbox("getValue") == "")
            $('#refreshFunc').textbox("setValue", "action_refresh();");
        if ($('#refreshIcon').textbox("getValue") == "")
            $('#refreshIcon').textbox("setValue", "icon-reload");
        if ($('#refreshCaption').textbox("getValue") == "")
            $('#refreshCaption').textbox("setValue", "刷新");
    }
}

function set_add_enabled(value) {
    if (!value) {
        $('#addFunc').textbox({ disabled: "disabled" });
        $('#addIcon').textbox({ disabled: "disabled" });
        $('#addCaption').textbox({ disabled: "disabled" });

        if ($('#addFunc').textbox("getValue") == "")
            $('#addFunc').textbox("setValue", "action_add();");
        if ($('#addIcon').textbox("getValue") == "")
            $('#addIcon').textbox("setValue", "icon-page-add");
        if ($('#addCaption').textbox("getValue") == "")
            $('#addCaption').textbox("setValue", "添加");

    }
    else {
        $('#addFunc').textbox({ disabled: "" });
        $('#addIcon').textbox({ disabled: "" });
        $('#addCaption').textbox({ disabled: "" });

        if ($('#addFunc').textbox("getValue") == "")
            $('#addFunc').textbox("setValue", "action_add();");
        if ($('#addIcon').textbox("getValue") == "")
            $('#addIcon').textbox("setValue", "icon-page-add");
        if ($('#addCaption').textbox("getValue") == "")
            $('#addCaption').textbox("setValue", "添加");
    }
}

function set_edit_enabled(value) {
    if (!value) {
        $('#updateFunc').textbox({ disabled: "disabled" });
        $('#updateIcon').textbox({ disabled: "disabled" });
        $('#updateCaption').textbox({ disabled: "disabled" });

        if ($('#updateFunc').textbox("getValue") == "")
            $('#updateFunc').textbox("setValue", "action_edit();");
        if ($('#updateIcon').textbox("getValue") == "")
            $('#updateIcon').textbox("setValue", "icon-page-edit");
        if ($('#updateCaption').textbox("getValue") == "")
            $('#updateCaption').textbox("setValue", "修改");
    }
    else {
        $('#updateFunc').textbox({ disabled: "" });
        $('#updateIcon').textbox({ disabled: "" });
        $('#updateCaption').textbox({ disabled: "" });

        if ($('#updateFunc').textbox("getValue") == "")
            $('#updateFunc').textbox("setValue", "action_edit();");
        if ($('#updateIcon').textbox("getValue") == "")
            $('#updateIcon').textbox("setValue", "icon-page-edit");
        if ($('#updateCaption').textbox("getValue") == "")
            $('#updateCaption').textbox("setValue", "修改");
    }
}

function set_delete_enabled(value) {
    if (!value) {
        $('#deleteFunc').textbox({ disabled: "disabled" });
        $('#deleteIcon').textbox({ disabled: "disabled" });
        $('#deleteCaption').textbox({ disabled: "disabled" });

        if ($('#deleteFunc').textbox("getValue") == "")
            $('#deleteFunc').textbox("setValue", "action_delete();");
        if ($('#deleteIcon').textbox("getValue") == "")
            $('#deleteIcon').textbox("setValue", "icon-page-delete");
        if ($('#deleteCaption').textbox("getValue") == "")
            $('#deleteCaption').textbox("setValue", "删除");
    }
    else {
        $('#deleteFunc').textbox({ disabled: "" });
        $('#deleteIcon').textbox({ disabled: "" });
        $('#deleteCaption').textbox({ disabled: "" });

        if ($('#deleteFunc').textbox("getValue") == "")
            $('#deleteFunc').textbox("setValue", "action_delete();");
        if ($('#deleteIcon').textbox("getValue") == "")
            $('#deleteIcon').textbox("setValue", "icon-page-delete");
        if ($('#deleteCaption').textbox("getValue") == "")
            $('#deleteCaption').textbox("setValue", "删除");
    }
}

function set_submit_enabled(value) {
    if (!value) {
        $('#submitFunc').textbox({ disabled: "disabled" });
        $('#submitIcon').textbox({ disabled: "disabled" });
        $('#submitCaption').textbox({ disabled: "disabled" });

        if ($('#submitFunc').textbox("getValue") == "")
            $('#submitFunc').textbox("setValue", "action_submit();");
        if ($('#submitIcon').textbox("getValue") == "")
            $('#submitIcon').textbox("setValue", "icon-ok");
        if ($('#submitCaption').textbox("getValue") == "")
            $('#submitCaption').textbox("setValue", "提交");
    }
    else {
        $('#submitFunc').textbox({ disabled: "" });
        $('#submitIcon').textbox({ disabled: "" });
        $('#submitCaption').textbox({ disabled: "" });

        if ($('#deleteFunc').textbox("getValue") == "")
            $('#submitFunc').textbox("setValue", "action_submit();");
        if ($('#submitIcon').textbox("getValue") == "")
            $('#submitIcon').textbox("setValue", "icon-ok");
        if ($('#submitCaption').textbox("getValue") == "")
            $('#submitCaption').textbox("setValue", "提交");
    }
}

function set_query_enabled(value) {
    if (!value) {
        $('#keywordFlag').switchbutton("uncheck");
        $('#keywordFlag').switchbutton({ disabled: "disabled" });

        $('#queryFunc').textbox({ disabled: "disabled" });
        $('#queryIcon').textbox({ disabled: "disabled" });
        $('#queryCaption').textbox({ disabled: "disabled" });

        if ($('#queryFunc').textbox("getValue") == "")
            $('#queryFunc').textbox("setValue", "action_query();");
        if ($('#queryIcon').textbox("getValue") == "")
            $('#queryIcon').textbox("setValue", "icon-search");
        if ($('#queryCaption').textbox("getValue") == "")
            $('#queryCaption').textbox("setValue", "查询");

    }
    else {
        $('#keywordFlag').switchbutton({ disabled: "" });

        $('#queryFunc').textbox({ disabled: "" });
        $('#queryIcon').textbox({ disabled: "" });
        $('#queryCaption').textbox({ disabled: "" });

        if ($('#queryFunc').textbox("getValue") == "")
            $('#queryFunc').textbox("setValue", "action_query();");
        if ($('#queryIcon').textbox("getValue") == "")
            $('#queryIcon').textbox("setValue", "icon-search");
        if ($('#queryCaption').textbox("getValue") == "")
            $('#queryCaption').textbox("setValue", "查询");

    }
}

function reset() {
    id = "-1";
    config_window("w", "创建新模块", "icon-page-add");

    $("#moduleName").textbox("setValue", "");
    $("#upperModule").textbox("setValue", "");
    $("#moduleSrc").textbox("setValue", "");
    $("#moduleUrl").textbox("setValue", "");
    $("#moduleSeq").textbox("setValue", "");
    $("#moduleDesc").textbox("setValue", "");
    $("#moduleNotes").textbox("setValue", "");
    $('#hide').switchbutton("uncheck");
    $("#tabs").tabs("select", 0);
    set_ctrl_enabled(false);
    set_refresh_enabled(false);
    set_add_enabled(false);
    set_edit_enabled(false);
    set_delete_enabled(false);
    set_submit_enabled(false);
    set_query_enabled(false);

    $('#hide').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_ctrl_enabled(checked);
        }
    });

    $('#refreshFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_refresh_enabled(checked);
        }
    });

    $('#addFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_add_enabled(checked);
        }
    });

    $('#updateFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_edit_enabled(checked);
        }
    });

    $('#deleteFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_delete_enabled(checked);
        }
    });

    $('#submitFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_submit_enabled(checked);
        }
    });

    $('#queryFlag').switchbutton({
        checked: false,
        onChange: function (checked) {
            set_query_enabled(checked);
        }
    });
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
    var row = $('#treegrid').treegrid('getSelected');
    if (row) {
        id = row.id;
        set_data();
    }
}

function action_delete() {
    var row = $('#treegrid').treegrid('getSelected');
    if (row) {
        id = row.id;
        delete_data();
    }
}