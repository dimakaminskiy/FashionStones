var fileIndex = null;
var jcrop_api,
    boundx,
    boundy,
    xsize,
    ysize;
var IsFirstImage = true;
$().ready(function () {
    fileIndex = new FileIndex();
    fileIndex.init();
});


function exists(selector) {
    return ($(selector).length > 0);
};

var maxSize = 10 * 1024 * 1024;//8 мб
var minSize = 5 * 1024;

function FileIndex() {
    _this = this;
    this.ajaxFileUpload = "/Admin/PHoto/UploadPreImage";
    this.init = function () {
        $('#UploadImage').fineUploader({
        request:
            {
                endpoint: _this.ajaxFileUpload
            },
            
        validation:
            {
                allowedExtensions: ['jpeg', 'jpg', 'gif', 'png', 'JPEG', "JPG"],
                sizeLimit: maxSize,
                itemLimit: 1,
                minSizeLimit: minSize,
            },
        multiple: false,
        dragAndDrop:
            {
                disableDefaultDropzone: true //отключаем дроп-зону
            },
        messages:
            { //русифицируем некоторые сообщения и кнопки
                typeError: "{file}: неверный тип файла. Принимаются только файлы форматов: {extensions}."
            },
        text:
            {
                uploadButton: 'Выберите фото',
                failUpload: 'Не закачан!',
                cancelButton: 'Отмена'
            },
           
        
        })
            .on('error', function (event, id, name, reason) {
                //do something
            })
        .on('complete', function (event, id, name, responseJson)
        {
            if (responseJson.success) { // все хорошо ))
               
                if (IsFirstImage) {
                $("#crop-avatar-target").attr('src', responseJson.fileName);
                $("#crop-avatar-target-preview").attr('src', responseJson.fileName);
                var img = $('#crop-avatar-target');
                $('#avatar-crop-box').removeClass('hidden');
                initAvatarCrop(img);


                    //$("#crop-avatar-target-preview").addClass('hidden');
                }

                
                if (IsFirstImage == false) {

                    jcrop_api.setImage(responseJson.fileName, function () {
                        $("#crop-avatar-target-preview").removeAttr("style");
                        $("#crop-avatar-target-preview").attr('src', responseJson.fileName);
                        $("#crop-avatar-target").attr("src", responseJson.fileName);
                        var bounds = jcrop_api.getBounds();
                        $("#crop-avatar-target").attr('width', bounds[0]);
                        $("#crop-avatar-target").attr('height', bounds[1]);

                        boundx = bounds[0];
                        boundy = bounds[1];
                        jcrop_api.animateTo([0, 0, 640, 800]);
                        jcrop_api.focus();

                    });
                }


                //var i = new Image();
                //i.src = responseJson.fileName;
                //i.onload = function() {
                //   /// alert(i.width);
                //    $('crop-avatar-target-preview').attr("width", w).attr("height", h);
                //}

                

                 
               
            
            
           



                
                if (IsFirstImage) IsFirstImage = false;

                // удалим сообщение о успешной загрузке файла
                setTimeout(function () {
                    $('.qq-upload-list li:last-child').remove();
                }, 2000);
            }
        });
    };
}


function initAvatarCrop(img) {
    img.Jcrop({
        onChange: updatePreviewPane,
        onSelect: updatePreviewPane,
       // aspectRatio: xsize / ysize,
        aspectRatio: 200/250,
        minSize: [640, 800],
        maxSize:[640,800]
    }, function () {
        var bounds = this.getBounds();
        boundx = bounds[0];
        boundy = bounds[1];

        jcrop_api = this;
        jcrop_api.animateTo([0,0, 640, 800]);
        jcrop_api.setOptions({ allowSelect: true });
        jcrop_api.setOptions({ allowMove: true });
        jcrop_api.setOptions({ allowResize: true });
        jcrop_api.setOptions({ aspectRatio:  200/250 });

        var pcnt = $('#preview-pane .preview-container');
        xsize = pcnt.width();
        ysize = pcnt.height();

        $('#preview-pane').appendTo(jcrop_api.ui.holder);

        jcrop_api.focus();
    });
}

function updatePreviewPane(c) {
    if (parseInt(c.w) > 0) {
        var rx = xsize / c.w;
        var ry = ysize / c.h;

        $('#preview-pane .preview-container img').css({
            width: Math.round(rx * boundx) + 'px',
            height: Math.round(ry * boundy) + 'px',
            marginLeft: '-' + Math.round(rx * c.x) + 'px',
            marginTop: '-' + Math.round(ry * c.y) + 'px'
        });
    }
}






function saveImage() {
    var img = $('#preview-pane .preview-container img');
    //$('#avatar-crop-box button').addClass('disabled');
    var n = $("#Caption").val();

    $.ajax({
        type: "POST",
        url: "/Admin/PHoto/Save",
        traditional: true,
        data: {
            w: img.css('width'),
            h: img.css('height'),
            l: img.css('marginLeft'),
            t: img.css('marginTop'),
            fileName: img.attr('src'),
            caption: n
        }
    }).done(function (data) {
        if (data.success === true) {
            window.location.replace("/Admin/PHoto/");
        }
        else {
            alert(data.errorMessage);
        }
    }).fail(function (e) {
        alert('Cannot upload at this time');
    });
}













function saveAvatar() {
    var img = $('#preview-pane .preview-container img');
    //$('#avatar-crop-box button').addClass('disabled');

    $.ajax({
        type: "POST",
        url: "/Admin/PHoto/Save",
        traditional: true,
        data: {
            w: img.css('width'),
            h: img.css('height'),
            l: img.css('marginLeft'),
            t: img.css('marginTop'),
            fileName: img.attr('src')
        }
    }).done(function (data) {
        if (data.success === true) {
            jQuery(".my_photo").attr("src", data.avatarFileLocation + "?" + Math.random());// меняем аватарку 
            jQuery('#message').append('<div class="message">'+data.message+'</div>'); // добавляем сообщение
            jQuery('.message').toggle(1000).delay(1000).effect('blind', 2500); // ефекты
            jQuery('.message').removeClass('message').addClass('noMess');
            $('#modal_close').click(); // закрываем окно


            jQuery("#avatar-crop-box").addClass("hidden");
            jQuery("#avatar-result").addClass("hidden");


            var h =
                '<div> <img src="" id="crop-avatar-target" alt="Uploaded image" />' +
                    '<div id="preview-pane">' +
                    '<div class="preview-container">' +
                    '<img src="" class="jcrop-preview" id="crop-avatar-target-preview" alt="Preview" />' +
                    '</div></div></div>' +
                    '<div style="position: absolute; top: 450px; left: 940px;">' +
                    '<button class="btn btn-default" onclick="saveAvatar()">Установить аватарку</button>'
                    + '</div>';


            jQuery(".jc-demo-box").html(h);

            IsFirstImage = true;
//   $('#avatar-result').removeClass('hidden');
            //   $('#avatar-crop-box').addClass('hidden'); // ToDo - Remove if you want to keep the upload box
            //   $("#UploadImage").remove();
        }
        else
        {
            jQuery('#message').append('<div class="message">' + data.errorMessage + '</div>'); // добавляем сообщение
            jQuery('.errorMessage').toggle(1000).delay(1000).effect('blind', 2500); // ефекты
            jQuery('.errorMessage').removeClass('errorMessage').addClass('errorNoMess');
            $('#modal_close').click(); // закрываем окно
            jQuery("#avatar-crop-box").addClass("hidden");
            jQuery("#avatar-result").addClass("hidden");
        }
    }).fail(function (e) {
        alert('Cannot upload avatar at this time');
    });
}




