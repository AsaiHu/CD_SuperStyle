<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dynamic.ascx.cs" Inherits="WebApp.Controls.Dynamic" %>
<script type="text/javascript">
    function <%=this.ClientID%>_bindData() {
        var url = "/Api/Web/SearchArticle?pageNumber=1&pageSize=4&classCode=001";
        $.ajax({
            type: 'POST',
            url: url,
            data: null,
            async: false,
            beforeSend: null,
            cache: false,
            dataType:'json',
            success: function (res) {
                var code = res.code;
                if (code == 0) {
                    var data = res.data;
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].PicPath != null && data[i].PicPath != "") {
                            $("#<%=this.ClientID %>_img_dwdt").attr("src", data[i].PicPath);
                            $("#<%=this.ClientID %>_btn_dwdt").attr("href", "Article.aspx?id=" + data[i].ID);
                            break;
                        }
                    }
                    var html = "";
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].Title.length > 17)
                            data[i].Title = data[i].Title.substring(0, 17);
                        html += '<div style="width: 250px; height: 30px; border-bottom: 1px dashed gray;"><span style="display: block; float: left; line-height: 30px; width: 100%;"><a href="Article.aspx?id=' + data[i].ID + '" target="_blank"><span style="font-family:宋体;">>></span>&nbsp;' + (data[i].Title.length > 15 ? data[i].Title.substring(0, 15) : data[i].Title) + '</a></span> </div>';
                    }
                    $("#<%=this.ClientID %>_div_dwdt").html(html);
                   
                }
                else {
                    var message = res.message;
                    alert(message);
                }
            },
            error: function (e) {
            },
            complete: function () {
            }
        });
    }

</script>
<div style="width: 250px; height: 415px; background-color: white; margin-top: 20px; margin-left: 20px; padding: 15px; float: left;">
    <div class="head">东吴动态<a href="javascript:void(0);" onclick="dropUrl('Trends.html');"><span style="width: 60px; display: block; height: 25px; font-size: 13pt; background-color: #fd0002; line-height: 25px; color: white; float: right; text-align: center;">more></span></a></div>

    <div style="width: 250px; height: 250px;">
        <a id="<%=this.ClientID %>_btn_dwdt"><img src="../../Images/web/dwjt.png" style="width: 248px; height: 248px; border: 1px solid #f2f2f2;" id="<%=this.ClientID %>_img_dwdt" /></a>
    </div>
    <div style="width: 250px; height: 130px;">
        <div style="font-size: 10pt; width: 250px; height: 168px;" id="<%=this.ClientID %>_div_dwdt">
            <div style="width: 250px; height: 30px; border-bottom: 1px dashed gray;">
                <span style="display: block; float: left; line-height: 30px; width: 100%;">>>&nbsp;第55期东吴讲堂讲座预告</span>
            </div>
            <div style="width: 250px; height: 30px; line-height: 30px; border-bottom: 1px dashed gray;">
                <span style="display: block; float: left; width: 100%;">>>&nbsp;"红楼梦"文学奖首奖获得者阎连科与</span>
            </div>
            <div style="width: 250px; height: 30px; line-height: 30px; border-bottom: 1px dashed gray;">
                <span style="display: block; float: left; width: 100%;">>>&nbsp;"红楼梦"文学奖首奖获得者阎连科与</span>
            </div>
            <div style="width: 250px; height: 30px; line-height: 30px; border-bottom: 1px dashed gray;">
                <span style="display: block; float: left; width: 100%;">>>&nbsp;"红楼梦"文学奖首奖获得者阎连科与</span>
            </div>

        </div>
    </div>

</div>
