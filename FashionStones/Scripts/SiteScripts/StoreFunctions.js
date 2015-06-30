//Инфо при наведении
function showd(pid) {
    document.getElementById('prop-' + pid).style.display = 'block';
};

function hided(pid) {
    document.getElementById('prop-' + pid).style.display = 'none';
}
function CallChangefunc(val) {
    $("#limit").val(val);
    $("#sub-form-limit").submit();
}
function getNameById(id) {
    return $("div[tr-id^='fName-" + id + "'] a.clickDetail").html();
}
function setName(name) {
    $("#popup-details-name").html(name);
}





$(document).ready(function () {
    $('.clickImgGallery').click(function (event) {
        event.preventDefault();
        var pId = $(this).attr("tr-id");
        var link = $(this).attr('href');
        var name = getNameById(pId);       
        $(function () {
            $('#exampleModalImg').empty();
            $('#exampleModalImg').prepend('<div class="box-modal_close arcticmodal-close"></div>' +
                '<img class="popup-gallery" src="' + link + '" />' +
                '<div class="arrow-left"><a href="#"></a></div>' +
                '<div class="arrow-right"><a href="#"></a></div>' +
                '<div id="popup-details" style="display: none">'+ pId + '</div>'+
                '<div id="popup-details-name" class="center">'+name+'</div>'
                );
            $('#exampleModalImg').arcticmodal();
        });
    });



    $('body').on('click', '#exampleModalImg .arrow-left', function (e) {
        e.preventDefault();
        var pId = $('#exampleModalImg #popup-details').html();
        var n = $('#goods td[td-class]').index($("td[td-class^='" + pId + "']"));
        var l = $('#goods td[td-class]').length;
        var indexNext = 0;
        var tdNext = 0;
        indexNext = n - 1;
        if (n == 0) {
            indexNext = l - 1;
        }
        tdNext = $('#goods td[td-class]')[indexNext];
       var nextId = $(tdNext).attr('td-class');   
       var image= $("#goods td[td-class^='" + nextId + "'] .clickImgGallery");
       var href = image.attr("href");
       $(".popup-gallery").attr("src", href);
       $("#popup-details").html(nextId);

       var name = getNameById(nextId);
       setName(name);

    });
    $('body').on('click', '#exampleModalImg .arrow-right', function (e) {
        e.preventDefault();
        var pId = $('#exampleModalImg #popup-details').html();
        var n = $('#goods td[td-class]').index($("td[td-class^='" + pId + "']"));
        var l = $('#goods td[td-class]').length;
        var indexNext = 0;
        indexNext = n + 1;
        if (n == l - 1) {
            indexNext = 0;
        }
        tdNext = $('#goods td[td-class]')[indexNext];
        var nextId = $(tdNext).attr('td-class');
        var image = $("#goods td[td-class^='" + nextId + "'] .clickImgGallery");
        var href = image.attr("href");
        $(".popup-gallery").attr("src", href);
        $("#popup-details").html(nextId);
        var name = getNameById(nextId);
        setName(name);
    });





    $('.clickDetail').click(function (e) {
        e.preventDefault();
        var link = $(this).attr("href");
        $.arcticmodal({
            type: 'ajax',
            url: link,
            afterClose: function (data, el) {
                var b = $('.magnifier, .cursorshade, .statusdiv, .tracker');
                b.remove();
            }
        });
    });
});