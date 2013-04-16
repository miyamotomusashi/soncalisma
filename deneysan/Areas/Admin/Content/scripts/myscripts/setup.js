$(function () {
    var screenheight = screen.height;
    $("#page").css("min-height", screenheight-200);

});

$(document).ready(function () {
    smallBrowserSize(jQuery(window).width());
});


$(window).resize(function () {
    smallBrowserSize(jQuery(window).width());
});

function smallBrowserSize(e) {
    if (e <= 960) { $("#logo").hide(); } else { $("#logo").show(); }
}

