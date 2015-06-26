$(document).ready(function () {


    $('body').on('click', '.btn_count_down', function() {
        var pId = $(this).attr("tr-id");
        var pInp = $(".inp_count[tr-id='" + pId + "']");
        var pCount = parseInt(pInp.val());
        if (pCount != pCount) {
            pCount = 1;
        } else if (pCount > 1) {
            pCount = pCount - 1;
        }
        pInp.val(pCount);
    });
    $('body').on('click', '.btn_count_up', function() {
        var pId = $(this).attr("tr-id");
        var pInp = $(".inp_count[tr-id='" + pId + "']");
        var pCount = parseInt(pInp.val());
        if (pCount != pCount) {
            pCount = 1;
        } else {
            pCount = pCount + 1;
        }
        pInp.val(pCount);
    });

    $('body').on('keydown', '.inp_count', function(e) {
  //  $(".inp_count").keydown(function(e) {
        // Allow: backspace, delete, tab, escape, enter
        console.log("key_kod " + e.keyCode);
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
            (e.keyCode == 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
            (e.keyCode == 88 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });


    $('body').on('click', '.btn_addToCart', function(e) {
            e.preventDefault();
            var pId = $(this).attr("tr-id");
            var pCount = $(".inp_count[tr-id='" + pId + "']").val();
           if (pCount <= 0) {
               $(".inp_count[tr-id='" + pId + "']").val(1);
               pCount = 1;
           }
        var hrefAddToCard = "/ShoppingCart/AddToCard";
        $.ajax({
                type: "POST",
                url: hrefAddToCard,
                traditional: true,
                data: {
                    id: pId,
                    count: pCount,
                }
            }).done(function(json) {
                if (json.success === true) {
                    $('.cart-status').text(json.itemCount);
                    $.jGrowl("Товар добавлен в корзину!", { sticky: false, theme: 'red_theme', life: 2000 });
                    $(".inp_count[tr-id='" +json.item + "']").val(1);
                } else {
                    alert(json.errorMessage);
                }
            }).fail(function() {
                alert('Cannot do it at this time');
            });


        });

});


