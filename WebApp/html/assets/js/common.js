var contactUrl = "../Api/Web/GetContact";

$(function () {
    $("#head_container").load("./hb.html #head");
    $("#footer_container").load("./hb.html #footer");
    bindFooterContact();
})

function bindFooterContact() {
    $.ajax({
        type: 'POST',
        data: null,
        dataType: 'json',
        url: contactUrl,
        success: function (json) {
            var data = json.data;
            document.getElementsByClassName("footer_telephone")[0].innerText = data.Telephone;
            document.getElementsByClassName("footer_mobilephone")[0].innerText = data.MobilePhone;
            document.getElementsByClassName("footer_fax")[0].innerText = data.Fax;
        }
    })
}