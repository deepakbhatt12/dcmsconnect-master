﻿///#source 1 1 /Areas/DcmsLite/Scripts/Receive.partial.js
$(document).ready(function () {
    $('button[data-icon]').each(function () {
        $(this).button({ icons: { primary: $(this).attr('data-icon') } });
    }).click(function (e) {
        $('#divReceivingDialog').dialog("option", "currentForm", $(e.target).closest('form')).dialog('open');
        return false;
    });

    $('#divReceivingDialog').dialog({
        title: 'Receive Cartons',
        autoOpen: false,
        width: 'auto',
        modal: true,
        closeOnEscape: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();
        },
        buttons: [
        {
            text: 'OK',
            click: function (event, ui) {
                var $form = $(this).dialog('option', 'currentForm');
                $(event.target).button({ disabled: true });
                $('#btnCancel').button({ disabled: true });
                $form.submit();
            }
        },
        {
            text: 'Cancel',
            id: 'btnCancel',
            click: function (event, ui) {
                $('#divReceivingDialog').dialog('close');
            }
        }
        ]
    });
});
///#source 1 1 /Areas/DcmsLite/Scripts/layout.js
$(document).ready(function () {
    $('#btnSearch').button({ icons: { primary: 'ui-icon-search' } }).on('click', function (e) {
        var $form = $(this).closest('form');
        $.ajax({
            url: $form.attr('action'),
            data: $form.serializeArray(),
            type: 'GET',
            cache: false,
            statusCode: {
                200: function (data, textStatus, jqXHR) {
                    //alert('200 ' + data);
                    window.location.href = data;
                    return true;
                },
                203: function (data, textStatus, jqXHR) {
                    $('#layoutError').text(data).show();
                }
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        });
        return false;
    });
});

