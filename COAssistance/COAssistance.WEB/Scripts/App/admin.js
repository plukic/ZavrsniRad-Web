var AHelper = {
    loader: '.loader-block',
    jqxhr: '',
    formbKey: '#formb',
    formsKey: '#forms',
    formeKey: '#forme',
    targetBlockKey: '#dataTarget',
    modalBlockId: '#modal-block',
    modalContainerId: '#modal-container',
    notificationDuration: 5000,
    timer: '',

    showLoader: function (display) {
        display ? $(this.loader).show('slow') : $(this.loader).hide('slow');
    },

    loadData: function (url, target) {
        this.showLoader(true);
        jqxhr = $.get(url);

        jqxhr.done(function (data) {
            $(target || AHelper.targetBlockKey).html(data);
            AHelper.showLoader(false);
        });

        jqxhr.fail(function (response) {
            if (response.responseJSON.RedirectUrl) {
                window.location.href = response.responseJSON.RedirectUrl;
            }
            AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
            AHelper.showLoader(false);
        });
    },

    getData: function (url, data) {
        $.ajax({
            url: url,
            type: 'GET',
            data: data,
            cache: false,
            success: function (data) {
                return data;
            },
            fail: function (response) {
                if (response.responseJSON.RedirectUrl) {
                    window.location.href = response.responseJSON.RedirectUrl;
                }
                AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
                AHelper.showLoader(false);
            }
        });
    },

    simulateModal: function (url, target) {
        if (url.length > 0) {
            var anchor = $('<a />', { class: 'modal-link hidden', href: url });
            $(target || AHelper.targetBlockKey).append(anchor);
            anchor[0].click();
        }
    },

    triggerAction: function (url, target) {
        clearTimeout(this.timer);
        this.showLoader(true);
        var postData = this.appendAntiforgeryToken();

        jqxhr = $.ajax({
            url: url,
            type: 'POST',
            data: postData,
            cache: false,
            success: function (response) {           
                AHelper.displayNotification(response.Text, response.Type);
                $(AHelper.modalContainerId).modal('hide');

                AHelper.loadData(response.RedirectUrl, target);
                AHelper.showLoader(false);
            },
            fail: function (response) {
                if (response.responseJSON.RedirectUrl) {
                    window.location.href = response.responseJSON.RedirectUrl;
                }
                AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
                AHelper.showLoader(false);
            }
        });        
    },

    generatePopup: function (message, url) {
        $('#popup').find('p').text(message);
        $('#popup').find('a').attr('href', url);
        $('#popup').show('slow');

        this.timer = setTimeout(function () {
            $('#popup').hide('slow');
        }, AHelper.notificationDuration);

        var element = $('<li />')
            .append($('<a />', { href: url, text: message })
                .prepend($('<i />', { class: 'fa fa-car text-error margin-r-5' })));

        $('#notificationsMenu').append(element);
    },

    displayNotification: function (message, type) {
        $('#notify').removeClass().addClass('text-' + type);
        $('#notify').text(message).show('slow');

        this.timer = setTimeout(function () {
            $('#notify').hide('slow');
        }, AHelper.notificationDuration);
    },

    postData: function (isCompleted, avoidValidation, targetId) {
        var form = $(AHelper.formbKey);

        if (!avoidValidation) {
            form.validate();

            if (!form.valid()) {
                return;
            }
        }

        form.find('#status').val(isCompleted);

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            cache: false,
            beforeSend: function () {
                form.find('.btn-danger')
                    .prop('disabled', true)
                    .html('<i class="fa fa-life-ring fa-spin"></i>');
            },
            success: function (response) {
                if (response.Type === 'success') {
                    console.log(response);
                    $(AHelper.modalContainerId).modal('hide');

                    AHelper.displayNotification(response.Text, response.Type);

                    if (response.RedirectUrl) {
                        AHelper.loadData(response.RedirectUrl, targetId);
                    }
                } else {
                    $(AHelper.modalBlockId).html(response);
                }
            },
            fail: function (response) {
                if (response.responseJSON.RedirectUrl) {
                    window.location.href = response.responseJSON.RedirectUrl;
                }
                AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
                AHelper.showLoader(false);
            }
        });

        return false;
    },

    postDataWithFile: function (isCompleted) {
        var form = $(AHelper.formeKey);

        form.validate();

        if (!form.valid()) {
            return;
        }

        var formData = new FormData($(form)[0]);

        formData = this.appendAntiforgeryToken(formData);
        form.find('#status').val(isCompleted);

        jqxhr = $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: formData,
            cache: false,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.Type === 'success') {
                    $(AHelper.modalContainerId).modal('hide');

                    AHelper.displayNotification(response.Text, response.Type);

                    if (response.RedirectUrl) {
                        AHelper.loadData(response.RedirectUrl, targetId);
                    }
                } else {
                    $(AHelper.modalBlockId).html(response);
                }
            },
            fail: function (response) {
                if (response.responseJSON.RedirectUrl) {
                    window.location.href = response.responseJSON.RedirectUrl;
                }
                AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
                AHelper.showLoader(false);
            }
        });

        return false;
    },

    searchData: function (form, target) {
        form = $(form || AHelper.formsKey);

        jqxhr = $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            cache: false
        });

        jqxhr.done(function (data) {
            $(target || AHelper.targetBlockKey).html(data);
        });

        jqxhr.fail(function (response) {
            if (response.responseJSON.RedirectUrl) {
                window.location.href = response.responseJSON.RedirectUrl;
            }
            AHelper.displayNotification(response.responseJSON.Text, response.responseJSON.Type);
            AHelper.showLoader(false);
        });
    },

    appendAntiforgeryToken: function (data) {
        data = data || {};
        var token = $('input[name=__RequestVerificationToken]');
        if (token.length > 0) {
            data.__RequestVerificationToken = token.val();
        }
        return data;
    }
};