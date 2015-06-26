$(document).ready(function () {

    //Регистрация смена
    $('.block_register').click(function () {
        $('.hide_block_register').toggle('explode', 700);
        $('.show_block_register').toggle('explode', 700);
    });

    //-------------Стрелочка вверх-------------------------/

    $("#top-link").hide();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#top-link').fadeIn();
        } else {
            $('#top-link').fadeOut();
        }
    });
    $('#top-link a').click(function () {
        $('body,html').animate(
                { scrollTop: 0 }, 800); return false;
    });

    $("#FS").hide();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
            $('#FS').fadeIn();
        } else {
            $('#FS').fadeOut();
        }
    });
    //-----------------------Меню основное-------------------------/
    $('#head table td').hover(function () {
        $(this).css({ 'background': 'linear-gradient(to top, #000000, #414141, #000000)' });
    },
            function () {
                $(this).css({ 'background': ' url("/Content/images/bg_white (1).png")' });
            }
    );
    //----------------------------------------------------------/
    //Свернуть/Развернуть окно

    $('.show_block').click(function () {
        var hide = $(this).next();
        hide.toggle('explode', 500);
    });

    $('.show_block_sortable').click(function() {
        var hide = $(this).next();
        hide.toggle('explode', 500);
    });
});