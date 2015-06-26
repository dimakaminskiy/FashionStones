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


$(document).ready(function () {
    $('.clickImgGallery').click(function (event) {
        event.preventDefault();
        var link = $(this).attr('href');
        $(function () {
            $('#exampleModalImg').empty();
            $('#exampleModalImg').prepend('<div class="box-modal_close arcticmodal-close"></div><img src="' + link + '" />');
            $('#exampleModalImg').arcticmodal();
        });
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