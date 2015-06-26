$(document).ready(function () {
    var recordToDelete;
    var theHREF;
    $('.deleteLink').click(function (e) {
        recordToDelete = $(this).attr("rowId");
        e.preventDefault();
        theHREF = $(this).attr("href");
        $('#dialog-confirm').dialog('open');
    });
    $('#dialog-confirm').dialog({
        autoOpen: false,
        height: 280,
        modal: true,
        resizable: false,
        buttons: {
            'Продолжить': function () {
                $.post(theHREF, null, function (json) {
                    if (json.success) {
                        mainTable = $('#row-' + recordToDelete).parent();
                        var count = mainTable.find('tr').length;
                        if (count < 3)
                        {
                            $("#table-order").remove();
                            $("#table").append("<p>Нет заказов</p>");
                        }
                        else {
                            $('#row-' + recordToDelete).fadeOut('slow').remove();
                        }
                    }
                    else {
                        var warrning = $("#div-dialog-warning  p").html();
                        $("#div-dialog-warning  p").html(warrning + json.errorMessage);
                        $("#div-dialog-warning").dialog({
                            resizable: false,
                            width: 500,
                            height: 280,
                            modal: true,
                            buttons: {
                                "Ок": function () {

                                    $(this).dialog("close");
                                }
                            },
                            close: function () {
                                $("#div-dialog-warning  p").html(warrning);

                            }
                        }).parent().addClass("ui-state-error");
                    }
                },
       'json');
                $(this).dialog('close');
            },
            'Отмена': function () {
                $(this).dialog('close');
            }
        }
    });
});


