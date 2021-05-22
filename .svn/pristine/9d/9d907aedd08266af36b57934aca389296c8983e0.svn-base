function setCookie(c_name, value, exMinutes) {
    var exdate = new Date();
    exdate.setMinutes(exdate.getMinutes() + exMinutes);

    var c_value = escape(value) + "; path=/" + ((exMinutes == null) ? "" : "; expires=" + exdate.toGMTString());

    document.cookie = c_name + "=" + c_value;
};

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
};

function removeCookie(cookieName) {
    setCookie(cookieName, "", -1);
};