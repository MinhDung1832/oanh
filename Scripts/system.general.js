
function Round(num, dp) {
    return Number(Number(num).toFixed(dp));
}
function ValidateNum($this) {
    $this.value = $this.value.replace(/[^0-9]/g, '');
}
function ValidateFloatNum($this) {
    $this.value = $this.value.replace(/[^0-9\.]/g, '');
}
function ParseJsonDateString(jsonDate) {
    let shortDate = null;
    if (jsonDate) {
        let regex = /-?\d+/;
        let matches = regex.exec(jsonDate);
        let dt = new Date(parseInt(matches[0]));
        let month = dt.getMonth() + 1;
        let monthString = month > 9 ? month : '0' + month;
        let day = dt.getDate();
        let dayString = day > 9 ? day : '0' + day;
        let year = dt.getFullYear();
        shortDate = dayString + '/' + monthString + '/' + year;
    }
    return shortDate;
}
function ParseJsonDate(jsonDate) {
    let offset = new Date().getTimezoneOffset() * 60000;
    let parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);
    if (parts[2] == undefined) parts[2] = 0;
    if (parts[3] == undefined) parts[3] = 0;
    return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
}
function AES(val1, val2) {
    let ret; $.ajax({ async: false, url: '/common/encrypt', type: 'post', data: JSON.stringify({ val1: val1, val2: val2 }), contentType: 'application/json, charset=utf-8', success: (resp) => { ret = resp } }); return ret;
}
function isValid(value) {
    return (value !== undefined && value !== null);
}
function isValidUser(val1, val2) {
    let ret; $.ajax({ async: false, url: '/common/validuser', type: 'post', data: JSON.stringify({ val1: val1, val2: val2 }), contentType: 'application/json, charset=utf-8', success: (resp) => { ret = resp } }); return ret;
}
function js_MenuMinimize() {
    $('[data-toggle="minimize"]').click();
}
function js_AutoScroll(anchor) {
    let target = jQuery(anchor);
    target = target.length && target || jQuery('[name=' + anchor.slice(1) + ']');
    if (target.length) {
        let targetOffset = target.offset().top - 49;
        jQuery('html, body').animate({ scrollTop: targetOffset }, 600);
        return false;
    }
}
function js_Loading(isLoading, loadingType, closeEffect) {
    switch (loadingType) {
        case 0:
            $('.loading-0').remove();
            if (isLoading) $('BODY').append('<div class="loading-0"></div>');
            break;
        case 1:
            if (isLoading) {
                $('.loading-1-wrapper').remove();
                $('BODY').removeClass('loaded-1');
                $('BODY').append(
                    '<div class="loading-1-wrapper">' +
                    '<div class="loading-1"></div>' +
                    '<div class="loading-1-section section-left"></div>' +
                    '<div class="loading-1-section section-right"></div>' +
                    '</div>');
            } else {
                if (closeEffect) {
                    $('BODY').addClass('loaded-1');
                    setTimeout(function () { $('.loading-1-wrapper').remove(); }, 500);
                } else {
                    $('.loading-1-wrapper').remove();
                }
            }
            break;
        case 2:
            $('.loading-2-background').remove();
            $('.loading-2-wrapper').remove();
            if (isLoading) {
                $('BODY').append(
                    '<div class="loading-2-background"></div>' +
                    '<div class="loading-2-wrapper">' +
                    '<div class="spinner-grow text-primary" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-secondary" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-success" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-danger" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-warning" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-info" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-light" role="status"><span class="sr-only">Loading...</span></div>' +
                    '<div class="spinner-grow text-dark" role="status"><span class="sr-only">Loading...</span></div>' +
                    '</div>'
                );
                $('.loading-2-background').fadeIn('slow');
                $('.loading-2-wrapper').fadeIn('slow');
            }
            break;
    }
}