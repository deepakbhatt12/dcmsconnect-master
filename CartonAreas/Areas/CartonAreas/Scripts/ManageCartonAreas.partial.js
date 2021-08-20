﻿/// <reference1 path="../../../Scripts/jquery-1.6.2-vsdoc.js" />

$(document).ready(function () {
    //$('button.mca-unassign').button({ text: false, icons: { primary: 'ui-icon-close' } });
    //$('button.mca-assign').button({ text: false, icons: { primary: 'ui-icon-pencil' } });

    $('#btnAplyForFilter').button();
    // The dialog must be passed $tr (current row which has been clicked) in the custom option currentRow
    $('#divEditDialog').dialog({
        autoOpen: false,
        width: 'auto',
        modal: true,
        closeOnEscape: true,
        open: function (event, ui) {
            // Copy the values in the current to the dialog's input boxes
            var $tr = $(this).dialog('option', 'currentRow');
            var vwh = $('span.mca-vwh', $tr).text().trim();

            $('#lblSku em, span.sku-display', this).text($('span.mca-sku', $tr).text().trim());
            $('#lblAssignedVwh em', this).text(vwh);

            var capacity = $('span.mca-maxassignedcartons', $tr).text().trim();
            $('#lblMaxAssignedCarton em', this).text(capacity);
            $('#tbMaxAssignedCarton').val(capacity);

            $(this).dialog({ title: 'Assign SKU to Location #' + $tr.attr('data-location-id') });
            $(this).find('#tbMaxAssignedCarton').removeClass('input-validation-error');
            $("#displayCartonCount span", this).text($('span.mca-cartoncount', $tr).text());
            $('#tbSku').val($tr.attr('data-upc-code'));
            $('#tbAssignedVwh').val(vwh);

            $('#hfCurrentLocationId', this).val($tr.attr('data-location-id'));

            // Clear the validation error classes. If the user closed the dialog after receiving validation errors, this code will clean up all that.
            $('div[data-valmsg-summary]', this).removeClass('validation-summary-errors').addClass('validation-summary-valid');
            $('input,select', this).removeClass('input-validation-error');
        },
        buttons: [
            {
                id: 'btnUpdate',
                text: 'Update',
                icons: { primary: "ui-icon-disk" },
                click: function (event, ui) {
                    var $form = $('form', this);
                    if (!$form.valid()) {
                        // Do nothing if the form is invalid
                        return false;
                    }

                    // Make the ajax call to update the SKU assignment
                    $('#imgAjaxLoader', this).show();
                    $.ajax($form.attr('action'), {
                        type: 'POST',
                        data: $form.serializeArray(),
                        context: this
                    }).done(function (data, textStatus, jqXHR) {
                        // Update the location count matrix
                        $('#divupdatefilter').html(data);
                        // Success. Update the SKU assignemnt in the current row
                        var $row = $(this).dialog('option', 'currentRow').addClass('ui-state-active');
                        $('span.mca-sku', $row).text($('span.sku-display', this).text());
                        $('span.mca-maxassignedcartons', $row).text($('#tbMaxAssignedCarton', this).val());
                        $('span.mca-vwh', $row).text($('#tbAssignedVwh', this).val());
                        //$('button.mca-unassign', $row).button('enable');
                        $('button.mca-unassign', $row).prop('disabled', false).removeClass('ui-state-disabled');
                        $(this).dialog('close');
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        alert(jqXHR.responseText);
                    }).always(function () {
                        $(this).dialog('option', 'currentRow').removeClass('ui-state-highlight');
                        $('#imgAjaxLoader', this).hide();
                    });
                }
            },
            {
                text: 'Cancel',
                click: function (event, ui) {
                    var $tr = $(this).dialog('option', 'currentRow').removeClass('ui-state-highlight');
                    $(this).dialog('close');
                }
            }
        ]
    });

    $('#divLocationList').on('click', 'button.mca-unassign', function (e) {
        // Unassign SKU
        var $tr = $(this).closest('tr').addClass('ui-state-highlight');
        var locationId = $tr.attr('data-location-id');
        if (!confirm("Assigned SKU from location " + locationId + " will be removed. Are you sure?")) {
            $tr.removeClass('ui-state-highlight');
            return false;
        }

        $.ajax($(this).attr('data-unassign-url'), {
            type: 'POST',
            context: { row: $tr, button: $(this) }
        }).done(function (data, textStatus, jqXHR) {
            $('#divupdatefilter').html(data);
            this.row.addClass('ui-state-active')
                .find('span.mca-sku,span.mca-maxassignedcartons,span.mca-vwh')
                .empty();
            //this.button.button('disable');
            this.button.prop('disabled', true).addClass('ui-state-disabled');
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        }).always(function () {
            this.row.removeClass('ui-state-highlight');
        });
    }).on('click', 'button.mca-assign', function (e) {
        var $tr = $(this).closest('tr').addClass('ui-state-highlight');
        $('#divEditDialog')
            .dialog('option', 'currentRow', $tr)
            .dialog('open');
    });
});

/*
$Id$ 
$Revision$
$URL$
$Header$
$Author$
$Date$
*/