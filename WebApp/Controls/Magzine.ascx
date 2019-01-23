<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Magzine.ascx.cs" Inherits="WebApp.Controls.Magzine" %>
<script type="text/javascript">
    function <%=this.ClientID%>_bindData() {
        var url = "/Api/Web/GetNewMagzineCategory";
        $.ajax({
            type: 'POST',
            url: url,
            data: null,
            beforeSend: null,
            cache: false,
            dataType: 'json',
            success: function (res) {
                var code = res.code;
                if (code == 0) {
                    var data = res.data;
                    var html = "";
                    if (data.length > 0) {
                        $("#<%=this.ClientID %>_img_xqk").attr("src", data[0].CategoryPicPath);
                        $("#<%=this.ClientID %>_title_zxqk").html("<a href='Period.aspx?ID=" + data[0].ID + "'style='color:gray;' >" + data[0].CategoryYear + "年 " + data[0].CategoryName + " 总第" + data[0].CategoryTotalCount + "期</div>");
                        $("#<%=this.ClientID %>_btn_xqk").attr("href", "ArticleCategory.aspx?categoryID=" + data[0].ID);
                    }
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
<div style="float: left; width: 250px; height: 395px; background-color: white; margin-left: 20px; padding: 15px; margin-top: 20px;float:left;">
    <div class="head">最新期刊&nbsp;&nbsp;<span style="color: red; font-size: 13pt;">New</span> <a href="Category.aspx" ><span style="width: 60px; display: block; height: 25px; font-size: 13pt; background-color: #fd0002; line-height: 25px; color: white; float: right; text-align: center;">more></span></a></div>
    <div style="width: 100%; height: 30px; color: gray; line-height: 30px" id="<%=this.ClientID %>_title_zxqk">2017年第5期 总第42期</div>

    <div style="width: 240px; height: 325px; margin-left: auto; margin-right: auto;">
        <a id="<%=this.ClientID %>_btn_xqk" target="_blank"><img src="../../Images/web/dwxs.png" id="<%=this.ClientID %>_img_xqk" style="width:240px;height:320px;" /></a>
    </div>
</div>
