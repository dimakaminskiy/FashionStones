       function blockPage() {
           $.reject({
               reject: {
                   msie: true
               },
               display: ['firefox', 'chrome', 'opera', 'safari', 'msie'],
               close: false,
               imagePath: "/Content/jReject/images/",
               // Background color for overlay  
               overlayBgColor: '#000',
               // Background transparency (0-1)  
               overlayOpacity: 1.0,

               // Fade in time on open ('slow','medium','fast' or integer in ms)  
               fadeInTime: 'fast',
               // Fade out time on close ('slow','medium','fast' or integer in ms)  
               fadeOutTime: 'fast',
               header: 'Ваш браузер устарел!',
               paragraph1: 'Вы пользуетесь устаревшим браузером, который не поддерживает современные веб-стандарты и представляет угрозу вашей безопасности.',
               paragraph2: 'Пожалуйста, установите современный браузер. Щёлкните по любой иконке, чтобы перейти на страницу загрузки браузера, загрузеите и установите его, после чего зайдите на сайт через него.'
           });
       }




$(document).ready(function () {
           var oldIE = false;
           if ($('html').is('.ie6, .ie7, .ie8, .ie9')) {
               oldIE = true;
           }
           if (oldIE) {
               $('html').css("overflow", "hidden");
               blockPage();
               return false;
           }

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