$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    $("#tabs").tabs();

    $("#Hardware").attr("checked",false);
    $('.pprice').css("display", "none");

   
    $('#txtPrice').numeric();
    
    var ischecked = $('#chchardware:checked').val();
    if (ischecked == "true" || ischecked == "True") {
        $('.pprice').css("display", "block");
    }

    $('#chchardware').click(function () {
        var stat = $('#chchardware:checked').val();
        if (stat == "True" || stat == "true") {
            $('.pprice').css("display", "block");
            $('#txtHardWarePrice').addClass("required");
            $('#txtHardWarePrice').numeric();
        }
        else {
            $('.pprice').css("display", "none");
            $('#txtHardWarePrice').removeClass("required");
        }
    });


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#ProductGroupId").attr("disabled", "disabled");
        $("#ProductGroupId").empty().append($("<option></option>").val("").html("Ürün Grubunu Seçiniz..."));
    }

    $("#Language").change(function () {

        var val = $("#Language option:selected").val();
        if (val == "") { $("#ProductGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductGroupId").empty().append($("<option></option>").val("").html("Ürün Grubunu Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#ProductGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }
             
      
    });

    
   
   
});
