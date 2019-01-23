<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="WebApp.Controls.Pagination" %>
<script type="text/javascript">


    function bindPagination(totalCount, pageIndex, pageSize) {
        if (totalCount <= 0) {
            $("#pagination").css("display", "none");
        }
        else {
            var pageCount = totalCount / pageSize;
            if (totalCount % pageSize != 0)
                pageCount += 1;

            var html = "";
            for (var i = 1; i <= pageCount; i++) {
                if (i == pageIndex) {
                    html += "<a href='javascript:go(" + i + ");' class='pageNumClick' >" + i + "</a>";
                } else {
                    html += "<a href='javascript:go(" + i + ");' class='pageNum' >" + i + "</a>";
                }
                //$("#pagination").css("width", (150 + i * 20) + "px");
            }

            $("#pages").html(html);
        }
    }

</script>
<div style="width: 640px; height: 20px; text-align: center; font-size: 11pt;">
    <div style="margin-left: auto; margin-right: auto; height: 20px; text-align: center;" id="pagination">
        <table cellpadding="0" cellspacing="0" style="margin-left: auto; margin-right: auto;">
            <tr>
                <td style="width: 80px; text-align: center;"><span id="last"><a href="javascript:last();">上一页</a></span></td>
                <td style="text-align: center;"><span id="pages"></span></td>
                <td style="width: 80px; text-align: center;">
                    <span id="next"><a href="javascript:next();">下一页</a></span>

                </td>
            </tr>
        </table>
    </div>
</div>

