    var oldValue = 0;
    function setItemPrice(pId, price) {
        $(".item-total-price[tr-id='" + pId + "']").html(price);
    }
    function setCountItem(pId, count) {
        $(".inp_count[tr-id='" + pId + "']").val(count);
    }
    function getCountItem(pId) {
        return $(".inp_count[tr-id='" + pId + "']").val();
    }
    function setTotolPrice(price) {
        $("#cart-total").html(price);
    }
    function setCartCount(count) {
        $(".cart-status").html(count);
    } 
    function editCartProduct(pId, count) {
        var href = "/ShoppingCart/EditCartProduct";
        $.ajax({
            type: "POST",
            url: href,
            traditional: true,
            data: {
                id: pId,
                count: count
            }
        }).done(function (data) {
            if (data.success === true) {
                setItemPrice(pId, data.itemPrice);
                setCountItem(pId, data.itemCount);
                setTotolPrice(data.itemToralPrice);
                setCartCount(data.itemTotalCount);
            } else {
                alert(data.errorMessage);
            }
        }).fail(function () {
            alert('Ошибка соединения, обновите страницу');
        });
    }


$(document).ready(function () {
    jQuery('.inp_count').on('input propertychange paste', function () {
        var pCount = parseInt($(this).val());
        var pId = $(this).attr("tr-id");
    if (pCount > 1) {
       editCartProduct(pId, pCount);
    } else {
        $(this).val(1);
        editCartProduct(pId, 1);
    }
  });
    $(".CleanLink").click(function (e) {
        e.preventDefault();
        var theHREF = $(this).attr("href");
        $.post(theHREF, null, function (json) {
            if (json.success) {
                $(".content_in").html("<h3>Просмотр корзины </h3> <p class=\"update\"> Ваша корзина пуста </p>");
                $('.cart-status').text('0');
            }
        });

    });
    $(".btn_count_down").click(function () {
      var pId = $(this).attr("tr-id");
      var pCount = parseInt(getCountItem(pId));
      var oldValue = pCount;
      if (pCount != pCount) {
          pCount = 1;
      } else if (pCount > 1) {
          pCount = pCount - 1;
      }
      if (oldValue != pCount) {
          editCartProduct(pId, pCount);
       }
   });
    $(".btn_count_up").click(function () {

      var pId = $(this).attr("tr-id");
      var pInp = $(".inp_count[tr-id='" + pId + "']");
      var pCount = parseInt(pInp.val());
      var oldValue = pCount;
      if (pCount != pCount) {
          pCount = 1;
      } else {
          pCount = pCount + 1;
      }
      if (oldValue != pCount) {
          editCartProduct(pId, pCount);
      }
  });
    $(".inp_count").keydown(function(e) {
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
    $(".RemoveLink").click(function (e) {
        e.preventDefault();
        var link = $(this).attr("href");
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            $.post(link, "", function(data) {
                if (data.success) {

                    if (data.itemTotalCount == 0) {
                        $('.content_in').html("<h3>Просмотр корзины </h3> <p class=\"update\"> Ваша корзина пуста </p>");
                    } else {
                    $('#row-' + data.item).fadeOut('slow');
                    setTotolPrice(data.itemToralPrice);  
                    }
                    console.log(data);
                    setCartCount(data.itemTotalCount);
                } else {
                    alert(data.errorMessage);
                }
                //$('#cart-total').text(data.itemToralPrice);
                ////  $('#update-message').text(data.Message).fadeIn(1000).fadeOut(2000);
                //$('#cart-status').text('Товаров в корзине (' + data.CartCount + ')');
            });

        }
    });

});
