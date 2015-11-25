; (function ($) {
    $.fn.mvcBreadCrumbs = function (options) {
        if (!this) {
            return this;
        }
        $that = $(this);

        //options
        var defaults = {
            url: '', // url for get current crumb
            id: '', // id current crumb
            classes: '', // classes for ul element
            storeKey: 'breadcrumb', // key for sessionStore
            onError: undefined // callback for fire error event
        };
        options = options ? options : {};
        options = $.extend({}, defaults, options);

        //init
        $that = $that.append('<ul class="breadcrumb ' + options.classes + '">').find('ul');

        //get only from sessionstorage
        CrumbsProcess($that, options, null);

        //load new data
        GetPart($that, options);

        return this;
    };

    // get data from server using id current crumb
    function GetPart($that, options) {
        $.ajax({
            url: options.url,
            method: 'POST',
            data: { id: options.id },
            success: function (data, textStatus, jqXHR) {
                if (textStatus === 'success') {
                    CrumbsProcess($that, options, data);
                } else {
                    if (options.onError !== undefined) options.onError(textStatus, jqXHR, data);
                }
            }
        })
        .fail(function (jqXHR, textStatus) {
            if (options.onError !== undefined) options.onError(textStatus, jqXHR);
        })
    };

    // join data from sessionStorage and new data
    function CrumbsProcess($that, options, newData) {
        var data = [];
        try {
            data = JSON.parse(sessionStorage.getItem(options.storeKey));
            if (!Array.isArray(data)) data = [];
        } catch (e) {
            data = [];
        }

        if (Array.isArray(newData)){
            newData.forEach(function (element, index, array) {
                for (var i = 0; i < data.length; i++) {
                    if ((element.hash === data[i].hash) || // url is equal
                        (element.level !== -1) && (element.level === data[i].level) || // level is present and equal
                        (element.head === true)) { // head of queue
                        data = data.slice(0, i);
                        break;
                    }
                };
                data.push(element);
            });
        }

        //store to sessionStorage (only if first element is head) and generate ui
        if ((data.length > 0) && (data[0].head === true)) sessionStorage.setItem(options.storeKey, JSON.stringify(data));
        Draw($that, data);
    };

    // generate ui
    function Draw($that, data) {
        $that.find('li').remove();
        data.forEach(function (element, index, array) {
            if (element.link === true) $that.append('<li><a href="' + element.url + '">' + element.label + '</a></li>');
            else $that.append('<li>' + element.label + '</li>');
        })
    }
})(jQuery);