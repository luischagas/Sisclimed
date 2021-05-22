ko.extenders.ValidaLogin = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var current = target()

            if (newValue !== current) {
                loginKO.message("");
                target(newValue);
                target.notifySubscribers(newValue);

                if (loginKO) {
                    loginKO.validLogin(loginKO.username().length > 0 && loginKO.password().length > 0);
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};

var PKEY = null;
var logging = false;
var loginKO = new loginData();

$(function () {
    var logged = getAuthorization();

    var datako = $("[data-bind*='loginKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(loginKO, datakoelement);
    }

    $('#btnEntrar').click(function () {
        loginKO.message("");

        if (PKEY) {
            Login(PKEY, 'LGN');
        } else {
            GetKeysAndLogin('LGN');
        }
    });






});

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && loginKO.validLogin()) {
        $('#btnEntrar').click();
    }
    return true;
};

function loginData() {
    this.username = ko.observable("").extend({ ValidaLogin: null });
    this.password = ko.observable("").extend({ ValidaLogin: null });
    this.validLogin = ko.observable(false);
    this.message = ko.observable("");        
};



function GetKeysAndLogin(TYPE) {
    if (loginKO.validLogin()) {

        $('#divLoaderLogin').show();

        $.ajax({
            type: 'POST',
            url: '../api/values/CriarSessao',            
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            xhrFields: {
                withCredentials: true
            },
            beforeSend: function (xhr, opts) {                
                 xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));                
            },
            success: function (data, ajaxOptions, response) {
                if (data) {                    
                        if (data.code == 1) {
                            if (response.status == 200) {
                                PKEY = new setAuthorization(data, true);
                                PKEY.TYPE = TYPE;
                            }
                        }                    
                }               
            },
            error: function (xhr, ajaxOptions, thrownError) {
                loginKO.message("Erro ao realizar login");
                logging = false;
                $('#divLoaderLogin').hide();
                $('#txtUsername').focus().select();                
            }
        }).done(function () {
            if (PKEY) {
                Login(PKEY);
            } else {
                loginKO.message("Erro de acesso ao realizar login");
            }
        });
    }
};

function Login(PKEY) {
    if (!logging) {

        logging = true;

        $('#divLoaderLogin').show();

        var words = '', credentials;

        if (PKEY.TYPE == 'LGN') { words = base64_encode(loginKO.username()) + '\\' + base64_encode(loginKO.password()); }

        credentials = '\\' + PKEY.SID + '\\' + base64_encode(words);
        
        $.ajax({
            type: 'POST',
            url: '../api/values/Login',            
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            xhrFields: {
                withCredentials: true
            },
            beforeSend: function (xhr, opts) {
                xhr.setRequestHeader("Authorization", "Basic " + base64_encode('LGN') + credentials);
            },
            success: function (data, ajaxOptions, response) {
                if (base64_decode(data) == "AuthLGN") {
                    window.location = "../Home/Atendimento";
                } else {
                    loginKO.message("Erro ao realizar login");
                    logging = false;
                    $('#divLoaderLogin').hide();
                    $('#txtUsername').focus().select();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                switch (xhr.responseText) {
                    default:
                        loginKO.message("Nome de usuário ou senha inválidos");
                        break;
                   
                }
                logging = false;
                $('#divLoaderLogin').hide();
                $('#txtUsername').focus().select();
            }
        }).done(function () {
            logging = false;
        });        
    }
};

function setAuthorization(data, alive) {
    try {         
        this.SID = data.value

        if (Modernizr.localstorage) {
            localStorage.PKEY = base64_encode(JSON.stringify(this));
        } else {
            setCookie('PKEY', base64_encode(JSON.stringify(this)), (alive) ? 1440 : null);
        }

        setCookie('SID', base64_encode(this.SID), (alive) ? 1440 : null);
    } catch (ex) {
        PKEY = null;
    }
};

function LogoutRequest(token) {
    this.token = token;
};


function Logout() {
    getSID = getCookie('SID');

    if (getSID) {
        if (getSID.length > 0) {

            $.ajax({
                type: 'POST',
                url: '../api/values/Logout',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(new LogoutRequest(base64_decode(getSID))),
                dataType: "json",
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                success: function (data, ajaxOptions, response) {                  
                },
                error: function (xhr, ajaxOptions, thrownError) {                   
                }
            })
        }
    }

    removeCookie("SID");

    if (Modernizr.localstorage) {
        localStorage.removeItem("PKEY");
    } else {
        removeCookie("PKEY");
    }
    sessionStorage.removeItem('Ramal');
    goToIndex();
};

function getAuthorization() {
    try {
        var getSID, ckPKEY;

        getSID = getCookie('SID');

        if (getSID) {

            getSID = base64_decode(getSID);

            if (Modernizr.localstorage) {
                ckPKEY = localStorage.PKEY;
                if (ckPKEY) {
                    getPKEY = JSON.parse(base64_decode(ckPKEY));
                } else { return false; }
            } else {
                ckPKEY = getCookie('PKEY');
                if (ckPKEY) {
                    getPKEY = JSON.parse(base64_decode(ckPKEY));
                } else { return false; }
            }

            if (getPKEY.SID == getSID) {
               return true;
            } else {
               return false;
            }
        } else {
            return false;
        }
    } catch (ex) {
        return false;
    }
};

