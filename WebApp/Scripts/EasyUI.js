function format_state(value) {
    if (value == success_code) {
        return success_name;
    }
    else {
        return fail_name;
    }
}

function format_check(value) {
    if (value == "1") {
        return '<div class="icon-ok">&nbsp;</div>';
    }
    else if (value == "0") {
        return '<div class="icon-cancel">&nbsp;</div>';
    }
    else {
        return '';
    }
}

function format_date(value, row, index) {
    try {
        if (value.length > 10)
            return value.substr(0, 10);
    } catch (e) { }
}

function format_money(value, row, index) {
    if (value != null)
        return value.toFixed(2);
}

function formart_public(value, row, index) {
    if (value)
        return '已发布';
    else
        return '未发布';
}

function open_selector(id, title, width, height) {
    $("#" + id).dialog({
        title: '&nbsp;' + title,
        width: width,
        height: height,
        iconCls: 'icon-folder-explore',
        modal: true
    });
    $('#' + id).dialog('open');
}

function config_dialog(id, title, icon) {
    $("#" + id).dialog({
        title: title,
        iconCls: icon,
        modal: true
    });
}

function config_window(id, title, icon) {
    $("#" + id).window({
        title: title,
        iconCls: icon,
        modal: true
    });
}

function messager_show(title, content) {
    $.messager.show({
        title: title,
        msg: content,
        timeout: 1000,
        height: 30,
        showType: 'show'
    });
}

function messager_slide(title, content) {
    $.messager.show({
        title: title,
        msg: content,
        timeout: 500,
        height: 30,
        showType: 'slide'
    });
}

function messager_fade(title, content) {
    $.messager.show({
        title: title,
        msg: content,
        timeout: 1000,
        height: 30,
        showType: 'fade'
    });
}
function messager_progress(title, content) {
    var win = $.messager.progress({
        title: title,
        msg: content
    });
    setTimeout(function () {
        $.messager.progress('close');
    }, 5000)
}

$.extend($.fn.validatebox.defaults.rules, {
    confirmPass: {
        validator: function (value, param) {
            var pass = $(param[0]).passwordbox('getValue');
            return value == pass;
        },
        message: '两次输入的密码不一致'
    }
})

function loading() {
    //if ($("#loading-mask").length <= 0) {
    //    $("<div id=\"loading-mask\" style=\"position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2;-moz-opacity: 0.8;opacity: .80; z-index: 20000\"><div id=\"pageloading\" style=\"position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;\"><img src=\"../Images/loading.gif\" align=\"absmiddle\" alt=\"\" />正在加载中,请稍候...</div></div>").appendTo("body");
    //}
    //else {
    $('#loading-mask').show();
    //}
}
function loaded() {
    $('#loading-mask').hide();
}

//全选反选
//参数:selected:传入this,表示当前点击的组件
//treeMenu:要操作的tree的id；如：id="userTree"
function treeChecked(selected, treeMenu) {
    var roots = $('#' + treeMenu).tree('getRoots'); //返回tree的所有根节点数组
    if (selected) {
        for (var i = 0; i < roots.length; i++) {
            var node = $('#' + treeMenu).tree('find', roots[i].id); //查找节点
            $('#' + treeMenu).tree('check', node.target); //将得到的节点选中
        }
    } else {
        for (var i = 0; i < roots.length; i++) {
            var node = $('#' + treeMenu).tree('find', roots[i].id);
            $('#' + treeMenu).tree('uncheck', node.target);
        }
    }
}

/**  
 * layout方法扩展  
 * @param {Object} jq  
 * @param {Object} region  
 */
$.extend($.fn.layout.methods, {
    /**  
* 面板是否存在和可见  
 * @param {Object} jq  
 * @param {Object} params  
 */
    isVisible: function (jq, params) {
        var panels = $.data(jq[0], 'layout').panels;
        var pp = panels[params];
        if (!pp) {
            return false;
        }
        if (pp.length) {
            return pp.panel('panel').is(':visible');
        } else {
            return false;
        }
    },
    /**  
     * 隐藏除某个region，center除外。  
     * @param {Object} jq  
     * @param {Object} params  
     */
    hidden: function (jq, params) {
        return jq.each(function () {
            var opts = $.data(this, 'layout').options;
            var panels = $.data(this, 'layout').panels;
            if (!opts.regionState) {
                opts.regionState = {};
            }
            var region = params;
            function hide(dom, region, doResize) {
                var first = region.substring(0, 1);
                var others = region.substring(1);
                var expand = 'expand' + first.toUpperCase() + others;
                if (panels[expand]) {
                    if ($(dom).layout('isVisible', expand)) {
                        opts.regionState[region] = 1;
                        panels[expand].panel('close');
                    } else if ($(dom).layout('isVisible', region)) {
                        opts.regionState[region] = 0;
                        panels[region].panel('close');
                    }
                } else {
                    panels[region].panel('close');
                }
                if (doResize) {
                    $(dom).layout('resize');
                }
            };
            if (region.toLowerCase() == 'all') {
                hide(this, 'east', false);
                hide(this, 'north', false);
                hide(this, 'west', false);
                hide(this, 'south', true);
            } else {
                hide(this, region, true);
            }
        });
    },
    /**  
     * 显示某个region，center除外。  
     * @param {Object} jq  
     * @param {Object} params  
     */
    show: function (jq, params) {
        return jq.each(function () {
            var opts = $.data(this, 'layout').options;
            var panels = $.data(this, 'layout').panels;
            var region = params;

            function show(dom, region, doResize) {
                var first = region.substring(0, 1);
                var others = region.substring(1);
                var expand = 'expand' + first.toUpperCase() + others;
                if (panels[expand]) {
                    if (!$(dom).layout('isVisible', expand)) {
                        if (!$(dom).layout('isVisible', region)) {
                            if (opts.regionState[region] == 1) {
                                panels[expand].panel('open');
                            } else {
                                panels[region].panel('open');
                            }
                        }
                    }
                } else {
                    panels[region].panel('open');
                }
                if (doResize) {
                    $(dom).layout('resize');
                }
            };
            if (region.toLowerCase() == 'all') {
                show(this, 'east', false);
                show(this, 'north', false);
                show(this, 'west', false);
                show(this, 'south', true);
            } else {
                show(this, region, true);
            }
        });
    }
});