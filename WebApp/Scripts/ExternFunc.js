/*对象序列化为字符串*/
String.toSerialize = function (obj) {
    var transferCharForJavascript = function (s) {
        var newStr = s.replace(
        /[\x26\x27\x3C\x3E\x0D\x0A\x22\x2C\x5C\x00]/g,
        function (c) {
            ascii = c.charCodeAt(0)
            return '\\u00' + (ascii < 16 ? '0' + ascii.toString(16) : ascii.toString(16))
        }
        );
        return newStr;
    };
    if (obj == null) {
        return null;
    }
    else if (obj.constructor == Array) {
        var builder = [];
        builder.push("[");
        for (var index in obj) {
            if (typeof obj[index] == "function") continue;
            if (index > 0) builder.push(",");
            builder.push(String.toSerialize(obj[index]));
        }
        builder.push("]");
        return builder.join("");
    }
    else if (obj.constructor == Object) {
        var builder = [];
        builder.push("{");
        var index = 0;
        for (var key in obj) {
            if (typeof obj[key] == "function") continue;
            if (index > 0) builder.push(",");
            builder.push(key + ":" + String.toSerialize(obj[key]));
            index++;
        }
        builder.push("}");
        return builder.join("");
    }
    else if (obj.constructor == Boolean) {
        return obj.toString();
    }
    else if (obj.constructor == Number) {
        return obj.toString();
    }
    else if (obj.constructor == String) {
        return '"' + transferCharForJavascript(obj) + '"';
    }
    else if (obj.constructor == Date) {
        return '{"__DataType":"Date","__thisue":' + obj.getTime() - (new Date(1970, 0, 1, 0, 0, 0)).getTime() + '}';
    }
    else if (this.toString != undefined) {
        return String.toSerialize(obj);
    }
}

function UUID(len, radix) {
    var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
    var uuid = [], i;
    radix = radix || chars.length;

    if (len) {
        // Compact form
        for (i = 0; i < len; i++) uuid[i] = chars[0 | Math.random() * radix];
    } else {
        // rfc4122, version 4 form
        var r;

        // rfc4122 requires these characters
        uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
        uuid[14] = '4';

        // Fill in random data.  At i==19 set the high bits of clock sequence as
        // per rfc4122, sec. 4.1.5
        for (i = 0; i < 36; i++) {
            if (!uuid[i]) {
                r = 0 | Math.random() * 16;
                uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
            }
        }
    }

    return uuid.join('');
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function contains(arr, obj) {
    var i = arr.length;
    while (i--) {
        if (arr[i] === obj) {
            return true;
        }
    }
    return false;
}


function date2string(d) {
    var y = d.getFullYear();
    var m = d.getMonth() + 1;
    m < 10 ? m = "0" + m : "";
    var d = d.getDate();
    d < 10 ? d = "0" + d : "";
    return y.toString() + "-" + m.toString() + "-" + d.toString();
}

function message_error(content) {
    messager_fade('提示信息', "<div style='width:100%;height:20px;line-height:20px;text-align:center;'><div style='width:50px;height:20px;background-image:url(/Scripts/easyui.1.5.1/themes/icons/error.gif);background-repeat:no-repeat;float:left;background-position:25px 1px;'></div><div style='float:left;'>" + content + "</div></div>");
}

function message_warning(content) {
    messager_fade('提示信息', "<div style='width:100%;height:20px;line-height:20px;text-align:center;'><div style='width:50px;height:20px;background-image:url(/Scripts/easyui.1.5.1/themes/icons/warning.gif);background-repeat:no-repeat;float:left;background-position:25px 1px;'></div><div style='float:left;'>" + content + "</div></div>");
}

function message_information(content) {
    messager_fade('提示信息', "<div style='width:100%;height:20px;line-height:20px;text-align:center;'><div style='width:50px;height:20px;background-image:url(/Scripts/easyui.1.5.1/themes/icons/information.gif);background-repeat:no-repeat;float:left;background-position:25px 1px;'></div><div style='float:left;'>" + content + "</div></div>");
}

function alert_warning(content) {
    $.messager.alert('提示信息', content, 'warning');
}

function alert_error(content) {
    $.messager.alert('提示信息', content, 'error');
}

function alert_information(content) {
    $.messager.alert('提示信息', content, 'information');
}

var border_color_required = "1px solid red";

/*用户登录信息动态验证*/
function login_dynamic_validate(obj) {
    $("#" + obj.id).css("border-bottom", "1px solid #ccc");
    $("#" + obj.id).attr("title", "");

    if ($("#" + obj.id).val() == "") {
        $("#" + obj.id).css("border-bottom", border_color_required);
        $("#" + obj.id).attr("title", "不能为空");
    }
}

/*用户登录验证*/
function login_validate() {
    //show_loadbar();
    $("#userId").css("border-bottom", "1px solid #ccc");
    $("#userId").attr("title", "");

    $("#userPwd").css("border-bottom", "1px solid #ccc");
    $("#userPwd").attr("title", "");

    //$("#verifyCode").css("border-bottom", "1px solid #ccc");
    //$("#verifyCode").attr("title", "");

    if ($("#userId").val() == "") {
        $("#userId").css("border-color", border_color_required);
        $("#userId").attr("title", "不能为空");
        alert_error("“登录名”不能为空！");
        //hide_loadbar();
        return false;
    }
    if ($("#userPwd").val() == "") {
        //alert("对不起！密码不能为空，请填写！");
        $("#userPwd").css("border-color", border_color_required);
        $("#userPwd").attr("title", "不能为空");
        alert_error("“登录密码”不能为空！");
        //hide_loadbar();
        return false;
    }
    //if ($("#verifyCode").val() == "") {
    //    //alert("对不起！验证码不能为空，请填写！");
    //    $("#verifyCode").css("border-color", border_color_required);
    //    $("#verifyCode").attr("title", "不能为空");
    //    alert_error("“验证码”不能为空！");
    //    //hide_loadbar();
    //    return false;
    //}
    //var cookie = new CookieClass();
    //if (cookie.getCookie("ValidateCode") != $("#verifyCode").val()) {
    //    $("#verifyCode").css("background-color", background_color_required);
    //    //$("#verifyCode").css("border-color", border_color_required);
    //    $("#verifyCode").attr("title", "验证码错误");
    //    alert_error("“验证码”错误！");
    //    //hide_loadbar();
    //    return false;
    //}
    return true;
}

/*打开修改密码对话框*/
function openPwd() {
    $('#w').window('open');
}
/*关闭修改密码对话框*/
function closePwd() {
    $("#userPwd").textbox("setValue", "");
    $("#confirmPwd").textbox("setValue", "");
    $('#w').window('close');
}
/*修改密码*/
function submitPwd() {
    if (!$("#userPwd").textbox("isValid") || !$("#confirmPwd").textbox("isValid")) return false;

    var userPwd = $('#userPwd').textbox("getValue");
    var cookie = new CookieClass();
    var userId = cookie.getCookieByKey("soochow_admin", "userid");

    var Users = new Object();
    Users.ID = parseInt(userId);
    Users.Password = userPwd;

    var data = String.toSerialize(Users);

    $.ajax({
        type: 'POST',
        url: "/Api/Security/ChangePwd",
        data: data,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        cache: false,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            closePwd();
        
         
            var code = res.code;
            var message = res.data;

            if (code == 0) {
                $.messager.alert('提示信息', message, 'info');
            }
            else {
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
/*用户注销*/
function logOut() {
    $.messager.confirm('提示信息', "确定要退出本次登录吗？", function (r) {
        if (r) {
            $.ajax({
                type: 'POST',
                url: "/Api/Security/Logout",
                data: null,
                beforeSend: function (XHR) {
                    loading();
                    XHR.setRequestHeader('Ticket', ticket);
                },
                success: function (res) {
                    var json = JSON.parse(res);
                    var code = json.code;
                    var message = json.data;

                    if (code == 0) {
                        window.location.href = "/Pages/Login.html";
                    }
                    else {
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
/*退出系统*/
function exit() {
    window.close();
}
/*显示时间*/
function timePrint() {
    var week; var date;
    var today = new Date();
    var year = today.getYear() + 1900;
    var month = today.getMonth() + 1;
    var day = today.getDate();
    var ss = today.getDay();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    date = year + "年" + month + "月" + day + "日 "
    if (ss == 0) week = "星期日"
    if (ss == 1) week = "星期一"
    if (ss == 2) week = "星期二"
    if (ss == 3) week = "星期三"
    if (ss == 4) week = "星期四"
    if (ss == 5) week = "星期五"
    if (ss == 6) week = "星期六"
    if (minutes <= 9)
        minutes = "0" + minutes
    if (seconds <= 9)
        seconds = "0" + seconds
    myclock = date + week + "&nbsp;" + hours + ":" + minutes + ":" + seconds;

    $("#liveclock").html(myclock);
}

function action_refresh() {
    window.location.reload();
}

function action_bind_enum(enumName, id) {

    var url = "/Api/Enum/Query?enumType=" + enumName;
    $.ajax({
        type: 'POST',
        url: url,
        data: null,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            var json = JSON.parse(res);
            var code = json.code;
            if (code == success_code) {
                var data = json.data;
                if (data != null) {
                    $("#" + id).combobox('loadData', data);
                }
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

var categoryId = 0;
function action_bind_category(id) {
    $.ajax({
        type: 'POST',
        url: "/Api/MagzineCategory/Query?Adapter=" + datalist,
        data: null,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            var json = JSON.parse(res);
            var code = json.code;

            if (code == success_code) {
                var data = json.data;
                $("#" + id).datalist('loadData', data);
                $("#" + id).datalist('selectRow', 0);
                if (data.length <= 1 && $("#wrap").length > 0) {
                    $('#wrap').layout('hidden', "west");
                    if ($("#center").length > 0) {
                        $('#center').css("border-width", "0px");
                    }
                    else {
                        $('#center').css("border-width", "1px");
                    }
                }
                else if ($("#wrap").length > 0)
                    $('#wrap').layout('show', 'west');
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



var classId = 0;
function action_bind_classes(id) {
    alert(1);
    $.ajax({
        type: 'POST',
        url: "/Api/Class/Query?Adapter=" + tree,
        data: null,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            var json = JSON.parse(res);
            var code = json.code;

            if (code == success_code) {
                var data = json.data;
                $("#" + id).tree('loadData', data);
                $("#" + id).tree('selectRow', 0);
                if (data.length <= 1 && $("#wrap").length > 0) {
                    $('#wrap').layout('hidden', "west");
                    if ($("#center").length > 0) {
                        $('#center').css("border-width", "0px");
                    }
                    else {
                        $('#center').css("border-width", "1px");
                    }
                }
                else if ($("#wrap").length > 0)
                    $('#wrap').layout('show', 'west');
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

function action_open_file(fileupload_id) {
    $("#" + fileupload_id).click();
}

function action_upload_image(fileupload_id,src_id,img_id) {
    var file = document.getElementById(fileupload_id).files[0];
    var formData = new FormData();
    formData.append("action", "UploadVMKImagePath");
    formData.append("file", file); //加入文件对象
    // 调用apicontroller后台action方法，将form数据传递给后台处理。contentType必须设置为false, 否则chrome和firefox不兼容
    $.ajax({
        url: "/Api/File/Upload",
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        dataType:'json',
        processData: false,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            var code = res.code;
            if (code == 0) {
                $("#" + src_id).val(res.data);
                $("#" + img_id).attr("src", res.data);
            }
            else {
                var message = json.message;
                $.messager.alert('提示信息', message, 'info');
            }
        },
        error: function (res) {
            $.messager.alert('提示信息', '上传失败', 'info');
        },
        complete: function () {
            loaded();
        }
    });
}

function action_upload_file(fileupload_id, src_id) {
    var file = document.getElementById(fileupload_id).files[0];
    var formData = new FormData();
    formData.append("action", "UploadFilePath");
    formData.append("file", file); //加入文件对象
    // 调用apicontroller后台action方法，将form数据传递给后台处理。contentType必须设置为false, 否则chrome和firefox不兼容
    $.ajax({
        url: "/Api/File/Upload",
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        dataType: 'json',
        processData: false,
        beforeSend: function (XHR) {
            loading();
            XHR.setRequestHeader('Ticket', ticket);
        },
        success: function (res) {
            var code = res.code;
            if (code == 0) {
                $("#" + src_id).textbox("setValue",res.data);
            }
            else {
                var message = json.message;
                $.messager.alert('提示信息', message, 'info');
            }
        },
        error: function (res) {
            $.messager.alert('提示信息', '上传失败', 'info');
        },
        complete: function () {
            loaded();
        }
    });
}