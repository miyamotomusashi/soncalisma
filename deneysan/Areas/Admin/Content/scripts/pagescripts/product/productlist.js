$(function () {

    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/urunlistesi/" + lang;
    });

    $("#GroupList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        var id = $("#GroupList option:selected").val();
        window.location.href = "/yonetim/urunlistesi/" + lang+"/"+id;
    });

});
