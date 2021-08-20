﻿
/* These are jshint settings as described in http://www.jshint.com/docs/
/* jshint unused: false */

/*************** Sound functions ***********************/
// Sound functions. Module Pattern from http://learn.jquery.com/code-organization/concepts/
var Sound = (function () {
    "use strict";
    // All external dependencies should be part of options
    var _options = {
        // Selector to error sound audio element
        error: null,
        // Selector to success sound audio element
        success: null
    };
    //Inserting values for each from Sound.init(), other wise insert the default values in _options.
    var init = function (options) {
        _options = $.extend(_options, options);
    };

    //Error sound played two times after completion of first.
    var error = function () {
        $(_options.error).one('ended', function (e) {
            this.play();
        })[0].play();
    };

    //success sound played only once.
    var success = function () {
        $(_options.success)[0].play();
    };

    // public API
    return {
        init: init,
        error: error,
        success: success
    };
})();


/*************** Progress bar functions ***********************/
// Functions to update the progress bar
var Progress = (function () {
    "use strict";
    var _options = {
        //id of the progress bar
        bar: '#progressCartons',
        //id of the span to show recived carton count
        receivedCount: '#receivedCount'
    };

    // will take default values if not passed.
    var init = function (options) {
        _options = $.extend(_options, options);
    };

    //Update function takes increment(i.e -1 in case of remove carton and 1 in case addition of carton.)
    var update = function (increment) {
        var $bar = $(_options.bar + 'div.progress-bar');
        var val = parseInt($bar.attr('aria-valuenow')) + increment;
        var maxval = parseInt($bar.attr('aria-valuemax'));
        var pct = Math.round(val * 100 / maxval);
        $bar.attr('aria-valuenow', val)
            .css('width', pct + '%')
            .find('span').text(pct);
        var count = parseInt($(_options.receivedCount).text());
        $(_options.receivedCount).text(count + increment);
    };

    //Public API
    return {
        init: init,
        update: update
    };
})();




/*************** Tabs functions ***********************/
// Functios to work with tabs. Tabs can be referenced by pallet id. A tab can be activated or created.
// Tab content is automatically loaded when a tab becomes active, or it can be manually loaded using html()
// Initial set of tabs with empty content are created during init().
var Tabs = (function () {
    "use strict";
    var _options = {
        // Selector for tab container
        tabContainer: '', //'#palletTabs',
        // Selector to container containing tab content
        contentContainer: '',  //'#palletTabContent',        
        // The URL which is responsible for returning pallet html
        // The URL should contain a placeholder ~ which will be replaced by the pallet id
        loadUrl: '',
        // Array of pallet ids for which tabs should be initially created
        pallets: []
    };

    // This name is used to store the pallet id corresponding to the tab. Can be anything
    var _attrPalletId = "data-palletid";

    var init = function (options) {
        _options = $.extend(_options, options);


        var $li;
        $.each(_options.pallets, function (i, val) {
            var x = Tabs.create(val);
            if (i === 0) {
                $li = x;
            }
        });

        // Remove the tab when the close icon is clicked
        $(_options.tabContainer).on('click', '.glyphicon-remove-sign', function (e) {
            var $li = $(this).closest('li');
            if ($li.is('.active')) {
                // If the visible tab is being removed, first make something else visible
                // Try to show the next tab
                var $nextli = $li.next();
                if ($nextli.length === 0) {
                    // If no next, then show first
                    $nextli = $('li:first', e.delegateTarget);
                }
                $('a', $nextli).tab('show');
            }
            // Remove content first
            var contentSelector = $('a', $li).attr('href');  // e.g. #tab2
            $(contentSelector).remove();
            // Now remove tab
            $li.remove();

        }).on('shown.bs.tab', function (e) {
            _load();
        }).find('li:first a').tab('show');
    };

    // jquery object representing the div of the content which is currently active
    var activeContent = function () {
        return $('> div.active.tab-pane', _options.contentContainer);
    };

    // Returns the pallet id corresponding to the active tab
    var activePalletId = function () {
        return $('> li.active', _options.tabContainer).attr(_attrPalletId);
    };

    // Creates a new tab. Does not ensure whether the tab for this pallet already exists.
    // Returns jquery object corresponding to the newly created li
    var create = function (palletId) {
        // Insert content div
        var htmlId = 'Pallet_' + (palletId || 'NOPALLET');
        $('<div></div>').attr('id', htmlId).addClass('tab-pane')
            .appendTo(_options.contentContainer);

        // Insert the li and return it
        var $a = $('<a></a>').attr({
            href: '#' + htmlId,
            role: 'tab',
            "data-toggle": 'tab',
            title: 'Pallet ' + (palletId || '??')
        }).text(palletId || 'NOPALLET');
        if (palletId) {
            $a.append(' <span title="Remove Pallet" class="glyphicon glyphicon-remove-sign text-info"></span>');
        }

        return $('<li></li>').attr(_attrPalletId, palletId)
            .append($a).appendTo(_options.tabContainer);
    };

    // Activates the tab for the passed pallet id. Creates a new tab if necessary
    var show = function (palletId) {
        var x = $('> li[' + _attrPalletId + '="' + palletId + '"] > a', _options.tabContainer).tab('show');
        if (x.length > 0) {
            // We were able to activate the pallet
            return;
        }
        // No such pallet. Create a new tab at the end of the tabs and make it active
        Tabs.create(palletId).find('a').tab('show');
    };

    // Updates the content html of the active tab
    var html = function (html) {
        //var $pane = $('div.active.tab-pane', _options.contentContainer);
        activeContent().html(html);
    };

    // Updates the html of active pallet
    var _load = function () {
        //alert('Loading');
        Tabs.html('Loading...');
        // Important to pass cache:false to ensure that the pallet contents are requeried after a carton is received
        return $.ajax(_options.loadUrl.replace('~', Tabs.activePalletId()), {cache: false}).then(function (data, textStatus, jqXHR) {
            Tabs.html(data);
        }, function (jqXHR, textStatus, errorThrown) {
            Tabs.html(jqXHR.responseText);
        });
    };
    return {
        init: init,
        create: create,
        activePalletId: activePalletId,
        html: html,
        show: show,
        activeContent: activeContent,
        load: _load
    };
})();



/*************** Scan functions ***********************/
// Monitors the enter key in the text area. When pressed, it starts a timer and acts on the user scans
// Errors are displayed in an associated popover.
var HandleScan = (function () {
    "use strict";
    var _timer;

    // Number of milliseconds to go before the timer expires
    var _ticker;

    // jquery object representing the text area. Set during init.
    var _$tb;

    var _options = {
        // Selector to the text area
        textarea: '', //'#scanArea textarea',
        // Selector of a button which will cause immediate handling of the scans.
        // It should have an ajax loading image which will be made visible while ajax calls are in progress
        // It should also have a <span class='badge'></span> which will display the countdown timer
        // Its next sibling will be shown when errors have been encountered.
        button: '', //'#scanArea button',
        // URL to invoke for receiving cartons
        cartonUrl: '',  //$('#tbScan').data('carton-url'),
        // This function is passed the pallet id and the cartons to receive. It should return a a name value array containing all parameters needed
        // by the function which will receive cartons
        cartonPostdata: function (palletId, cartons) {
            // This is an example function. Not useful. Caller must pass it to init
            //return [
            //    { name: 'processId', value: 123 },
            //    { name: 'cartons', value: cartons },
            //    { name: 'palletId', value: palletId },
            //    { name: 'dispos', value: 'something' }
            //];
        },
        delay: 3000   // Number of milliseconds delay after enter is pressed
    };

    // Returns true if there was an existing timer which was cleared
    var _clearTimer = function () {
        if (!_timer) {
            return false;
        }
        clearInterval(_timer);
        _timer = null;
        $('span.badge', _options.button).addClass('hidden');
        return true;
    };

    // Starts the timer with the specified delay. delay can be 0.
    var _startTimer = function (delay) {
        _clearTimer();
        _ticker = delay;
        $('span.badge', _options.button).removeClass('hidden').text('');
        _timer = setInterval(function () {
            _ticker -= 1000;
            $('span.badge', _options.button).text(_ticker / 1000);
            if (_ticker <= 0) {
                _act();  // Calling our private function
            }
        }, Math.min(delay, 1000));
    };

    // Displays the ajax loader image and hides the error message.
    // Disables go button and text area
    var _preAction = function () {
        _clearTimer();
        //alert(_options.button);
        $(_options.button).prop('disabled', true)
            .find('img').removeClass('hidden')
            .end()
            .next('button').addClass('hidden');
        
        _$tb.prop('disabled', true).popover('hide');
    };

    // Hides the ajax loader image
    var _postAction = function () {
        $(_options.button).prop('disabled', false).find('img').addClass('hidden');
        _$tb.prop('disabled', false).focus();
    };

    // Shows error popover
    var _showError = function (text) {
        Sound.error();
        _$tb.attr('data-content', text)
            .popover('show');

        $(_options.button).next('button').removeClass('hidden');
    };

    //Hides error popover
    //var _hideError = function () {
    //    $(_options.button).next('button').addClass('hidden');
    //    $(_options.textarea).popover('hide');
    //};
    
    // Returns whether the input in the text area is worth acting upon
    // If it returns -1 then error popup has already been displayed and no further action should be taken.
    // If it returns 0, you should act immediately. 1 means act whenver you want
    var _canAct = function () {
        var tokens = _tokens();
        if (tokens.length === 0) {
            // Text box is empty. Nothing to do
            return -1;
        }
        if (_isPallet(tokens[tokens.length - 1])) {
            // Act on pallet scan immediately
            return 0; 
        }
        if (!Tabs.activePalletId()) {
            _showError('Please scan a pallet first');
            // If there is no active pallet, we must require a pallet scan
            return -1;
        }
        return 1;
    };

    var init = function (options) {
        _options = $.extend(_options, options);
        _$tb = $(_options.textarea);
        _$tb.on('keypress', function (e) {
            _clearTimer();
            _$tb.popover('hide');
            if (e.which !== 13) {
                // We are interested only when enter key is pressed
                return;
            }
            Sound.success();
            // Disallow carton scan if there is no active pallet
            switch (_canAct()) {
                case -1:
                    // No action
                    return;

                case 0:
                    // Act now
                    //_act();
                    _startTimer(0);
                    // Ignore key press
                    return false;

                default:
                    // Act later
                    _startTimer(_options.delay);
                    return;
            }
        }).popover({
            trigger: 'manual',
            html: true,
            // The title is added here with custom cross button for removeing poopover
            title: '<strong class="text-danger"><span class="text-danger glyphicon glyphicon-warning-sign"></span> Error Message</strong><a class="close text-danger" href="#">&times;</a>',
            placement: 'auto',
            container: 'body'
        }).on('shown.bs.popover', function () {
            // Select text area text after popover is displayed
            _$tb.select().focus();
        });

        // Hide the popover when the X in the title is clicked
        $(document).on('click', '.close', function (e) {
            //hiding the error message popover and at the same doing empty textarea with focus.
            _$tb.popover('hide').focus();
        });
        //Shows the popover again after closing the popover on click of icon next to go button.
        $(_options.button).on('click', function (e) {
            if (_canAct() >= 0) {
                //_act();
                _startTimer(0);
            }
        }).next('button').on('click', function (e) {
            _$tb.popover('show');
        });
    };

    var _tokens = function () {
        return $.grep($(_options.textarea).val().toUpperCase().split('\n'), function (txt, i) {
            // Ignore empty lines
            return txt.trim().toUpperCase() !== '';
        });
    };

    // TODO: Make this logic more robust
    var _isPallet = function (str) {
        return str.toUpperCase().indexOf('P') === 0;
    };

    // Called to immediately act on the text in text area
    var _act = function () {
        _clearTimer();

        var tokens = _tokens();
        if (tokens.length === 0) {
            // Text box is empty. Nothing to do
            return;
        }

        var def = $.Deferred(_preAction);
        var chain = def;
        //var lastscan = tokens[tokens.length - 1];

        // The value in textbox after all ajax has completed
        var tbVal = [];

        var palletId;
        var cartons;
        if (_isPallet(tokens[tokens.length - 1])) {
            palletId = tokens[tokens.length - 1];
            cartons = tokens.slice(0, tokens.length - 1);
        } else {
            cartons = tokens;
        }

        if (cartons.length > 0) {
            // Some cartons were scanned before this pallet
            chain = chain.then(_receiveCartons.bind(undefined, cartons)).done(function (cartonsRejected) {
                // Appending one array to another http://stackoverflow.com/questions/1374126/how-to-extend-an-existing-javascript-array-with-another-array
                tbVal.push.apply(tbVal, cartonsRejected);
            }).fail(function (cartonsRejected, palletId) {
                tbVal.push.apply(tbVal, cartonsRejected);
                if (palletId) {
                    tbVal.push(palletId);
                }
            }.bind(undefined, cartons, palletId));
        }

        if (palletId) {
            chain = chain.then(function (palletId) {
                Tabs.show(palletId);
            }.bind(undefined, palletId));
        }

        //if (_isPallet(lastscan)) {
        //    // First process the cartons before this pallet
        //    if (tokens.length > 1) {
        //        // Some cartons were scanned before this pallet
        //        chain = chain.then(_receiveCartons.bind(undefined, tokens.slice(0, tokens.length - 1))).done(function (cartons) {
        //            tbVal.push(cartons);
        //        });
        //    }
        //    chain = chain.then(_changePallet.bind(undefined, lastscan)).fail(function (palletId) {
        //        tbVal.push(palletId);
        //    }.bind(undefined, lastscan));
        //} else {
        //    chain = chain.then(_receiveCartons.bind(undefined, tokens)).done(function (cartons) {
        //        // Appending one array to another http://stackoverflow.com/questions/1374126/how-to-extend-an-existing-javascript-array-with-another-array
        //        tbVal.push.apply(tbVal, cartons);
        //        alert(tbVal.length);
        //    });
        //}
        chain = chain.always(_postAction).always(function () {
            _$tb.val(tbVal.join('\n'))
        });

        def.resolve();  // Initiate the function chain
    };

    // Returns ajax object so that we can chain functions to be executed after ajax call is complete
    // Should be private
    var _receiveCartons = function (cartons) {
        return $.post(_options.cartonUrl, _options.cartonPostdata(Tabs.activePalletId(), cartons)).then(function (data, textStatus, jqXHR) {
            // Success
            var cartons = [];
            if (data && data.length > 0) {
                var $ul = $('<ul class="list-group"></ul>');

                $.each(data, function (i, elem) {
                    $('<li class="list-group-item list-group-item-warning"></li>').text(elem.cartonId + ': ' + elem.message).appendTo($ul);
                    cartons.push(elem.cartonId);
                });
                _showError($ul[0].outerHTML);
            } else {
                // Clear the text box
                _$tb.val('');
            }
            Tabs.load();
            //this.count === -1 (i.e removing carton from pallet)
            Progress.update(this.count);
            return cartons;
        }.bind({
            count: cartons.length
        }), function (jqXHR, textStatus, errorThrown) {
            switch (jqXHR.status) {
                case 500:
                    // Some exception thrown by action
                    _showError(jqXHR.responseText);
                    break;

                default:
                    // Action was not found
                    _showError('Error ' + jqXHR.status + ': ' + errorThrown);
                    break;
            }

        });
    };

    // Expects this.palletId
    // Returns ajax object to enable further chaining
    // Should be private
    //var _changePallet = function (palletId) {
    //    Tabs.show(palletId);
    //};

    return {
        init: init
    };
})();


/*************** Printing functions ***********************/
// expects e.data.url
// e.delegateTarget should be the modal dialog
// fills printers within the first select within the dialog
function LoadPrinters(e) {
    // Populate the printer drop down when shown first time
    var $ddlPrinters = $('select', e.delegateTarget);
    $.get(e.data.url).then(function (printers, textStatus, jqXHR) {
        // Success. We have got the list of printers
        //var selected = jqXHR.getResponseHeader("Selected");
        $.each(printers, function (i, printer) {
            var x = $('<option></option>').val(printer.Name).text(printer.Name + ' : ' + printer.Description);
            if (printer.Name === this.selected) {
                x.attr('selected', 'selected');
            }
            this.ddl.append(x);
        }.bind({
            ddl: this.ddl,
            selected: jqXHR.getResponseHeader("Selected")
        }));
    }.bind({
        ddl: $ddlPrinters
    }), function (jqXHR, textStatus, errorThrown) {
        // Some error
        var x = $('<option></option>').val('')
            .html('<span class="bg-danger">Could not retrieve printer list: ' + textStatus + ' ' + jqXHR.status + '</span>');
        this.append(x);
    }.bind(this));
}

// Called when the print buton is clicked.
// e.delegateTarget should be the print dialog.
// e.data must contain the url and postdata
// The value of the selected printer will be stuffed in the second element of postdata
// The value of carton id will be stuffed in the first value of postdata. It is expected that the text of a span with class cartonId is the cartonId.
function OnPrint(e) {
    // this is the jquery object for print dialog
    function DisplayPrintMessage(text, cssClass) {
        $('div.alert', this).removeClass()
            .addClass('alert')
            .addClass(cssClass)
            .html(text);
    }

    var $dlg = $(e.delegateTarget);
    var printer = $('select', $dlg).val();
    if (!printer) {
        DisplayPrintMessage.call($dlg, 'Please Select a printer', 'alert-warning');
        return;
    }
    // Stuff the selected printer in the second value
    e.data.postdata[0].value = $('span.cartonId', $dlg).text();
    e.data.postdata[1].value = printer;
    $.post(e.data.url, e.data.postdata).then(function (data, textStatus, jqXHR) {
        // Success
        DisplayPrintMessage.call(this.dlg, jqXHR.responseText, jqXHR.status === 203 ? 'alert-warning' : 'alert-success');
    }.bind({ dlg: e.delegateTarget }), function (jqXHR, textStatus, errorThrown) {
        // error
        Sound.error();
        DisplayPrintMessage.call(this.dlg, jqXHR.responseText, 'alert-danger');
    }.bind({ dlg: e.delegateTarget }));
}



/*************** Carton Removing functions ***********************/
// Called when the remove ok buton is clicked.
// e.delegateTarget should be the remove dialog.
// e.data must contain the url and postdata
// The value of carton id will be stuffed in the first value of postdata. It is expected that the text of a span with class cartonId is the cartonId.
function OnRemove(e) {
    var $dlg = $(e.delegateTarget);
    // Stuff the cartonId in the first value
    e.data.postdata[0].value = $('span.cartonId', $dlg).text();
    $.post(e.data.url, e.data.postdata).then(function (data, textStatus, jqXHR) {
        // success
        $dlg.modal('hide');
        Tabs.html(data);
        Progress.update(-1);
    }.bind({ dlg: e.delegateTarget }), function (jqXHR, textStatus, errorThrown) {
        // error
        Sound.error();
        alert('Error: ' + jqXHR.responseText);
    }.bind({ dlg: e.delegateTarget }));
}
