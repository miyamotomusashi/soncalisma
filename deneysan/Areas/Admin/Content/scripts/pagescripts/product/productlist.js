$(function () {
    var id = 0;
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/urunlistesi/" + lang;
    });

    $("#GroupList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        id = $("#GroupList option:selected").val();
        window.location.href = "/yonetim/urunlistesi/" + lang+"/"+id;
    });
    SortOrderByCategory(id, "/Product/SortRecords");
});


function RemoveTechnic(id) {
    
    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btntechnic_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/Product/RemoveTechnic',
        data: '{id:"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function () {

            $("#btntechnic_" + id).attr("src", "/Areas/Admin/Content/images/icons/technic.png");
            $("#divtechnical").remove();
            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });
        }
    });
}

function RemoveTraining(id) {

    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btnmanual_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/Product/RemoveTraining',
        data: '{id:"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function () {

            $("#btnmanual_" + id).attr("src", "/Areas/Admin/Content/images/icons/usermanual.png");
            $("#divtraining").remove();
            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });
        }
    });
}



function RemoveExperimental(id) {

    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btnexerimental_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/Product/RemoveExperimental',
        data: '{id:"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function () {

            $("#btnexerimental_" + id).attr("src", "/Areas/Admin/Content/images/icons/experimental.png");
            $("#divexperimental").remove();
            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });
        }
    });
}


function RemoveVideo(id) {

    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btnvideo_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/Product/RemoveVideo',
        data: '{id:"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function () {

            $("#btnvideo_" + id).attr("src", "/Areas/Admin/Content/images/icons/video.png");
            $("#divvideo").remove();
            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });
        }
    });
}