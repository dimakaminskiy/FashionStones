function setInfo(info) {
    $(".detailInfo").html(info);
}
function setImage(small,big) {
    $(".my-foto").attr("src", small).attr("data-large", big);
}
function setArticul(art) {
    $(".popup-art").html(art);
}
function setPrice(price) {
    $(".popup-price").html(price);
}
function setName(name) {
    $("#detail .upper").html(name);
}

function changeProduct(pId) {
    $("#detail").attr("tr-id", pId);
    $("#popup-option .btn_count_down").attr("tr-id", pId);
    $("#popup-option .inp_count").attr("tr-id", pId);
    $("#popup-option .btn_count_up").attr("tr-id", pId);
    $("#popup_btn button").attr("tr-id", pId);
}


$(document).ready(function () {
    // если отсутсвует zoomsl-3.0.min.js
    if (!$.fn.imagezoomsl) {
        $('.msg').show();
        return;
    } else $('.msg').hide();
    // инициализация плагина
    $('.my-foto').imagezoomsl({
        zoomrange: [3, 3]
    });

    $('.arrow-right a').click(function (e) {
        e.preventDefault();
        var id = $("#detail").attr("tr-id");
        var n = $('#goods td[td-class]').index($("td[td-class^='" + id + "']"));
        var l = $('#goods td[td-class]').length;
        var indexNext = 0;
        var tdNext = 0;
        indexNext = n + 1;
        if (n == l-1) {
            indexNext = 0;
        }
        tdNext = $('#goods td[td-class]')[indexNext];
        var nextId = $(tdNext).attr('td-class');
        var text = $("#prop-" + nextId).html();
        var preview = $(".preview[tr-id='preview-" + nextId + "']").attr("src");
        var img = $(".clickImgGallery[tr-id='" + nextId + "']").attr("href");
        var fName = $("div[tr-id='fName-" + nextId + "'] a").html();
        var arrWords = fName.split(/\s+/);
        var art = arrWords[0];
        var name = arrWords.slice(1, arrWords.length).join(" ");
        var price = $(".price[tr-id='price-" + nextId + "']").html();
        setArticul(art);
        setName(name);
        setPrice(price);
        changeProduct(nextId);
        setImage(preview, img);
        setInfo(text);


    });

    $('.arrow-left a').click(function (e) {
        e.preventDefault();
        var id = $("#detail").attr("tr-id");
        var n = $('#goods td[td-class]').index($("td[td-class^='" + id + "']"));
        var l = $('#goods td[td-class]').length;
        var indexNext = 0;
        var tdNext = 0;
        indexNext = n - 1;
        if (n == 0) {
            indexNext = l-1;
        }
        tdNext = $('#goods td[td-class]')[indexNext];
        var nextId = $(tdNext).attr('td-class');
        var text = $("#prop-" + nextId).html();
        var preview = $(".preview[tr-id='preview-" + nextId + "']").attr("src");
        var img = $(".clickImgGallery[tr-id='" + nextId + "']").attr("href");
        var fName = $("div[tr-id='fName-" + nextId + "'] a").html();
        var arrWords = fName.split(/\s+/);
        var art = arrWords[0];
        var name = arrWords.slice(1, arrWords.length).join(" ");
        var price = $(".price[tr-id='price-" + nextId + "']").html();
        //var options = $(".option[tr-id='option-" + nextId + "']").html();
        setArticul(art);
        setName(name);
        setPrice(price);
        changeProduct(nextId);
        setImage(preview, img);
        setInfo(text);


    });
});


