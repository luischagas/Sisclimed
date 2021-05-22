var estado;

var modalShowing = false;

ko.extenders.ValidaCadastro = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var current = target()

            if (newValue !== current) {
                //msgLogin("");
                target(newValue);
                target.notifySubscribers(newValue);

                if (convenioKO) {
                    convenioKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var convenioKO = new convenioData();

$(function () {
    ConvenioStart();
});


function ConvenioStart() {

    var datako = $("[data-bind*='convenioKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(convenioKO, datakoelement);
    }

    Loader('show');

    GetListConvenios();

    $('#btnCadastrarNovoConvenio').on('click', function () {

        $('#modalNovoCadastroConvenio').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoConvenio span').text("Salvar");
        $('h4').text('Novo Cadastro de Convênio');
    })

    $('#modalNovoCadastroConvenio').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroConvenio').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblConvenio', convenioKO.currentDisplayConvenioIdentity());
    });

    $('#btnLimparConvenio').on('click', function () {

        clearFields();

    })


    InitSeletctonic(['tblConvenio'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setCurrentDisplayConvenio($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(convenioKO.currentDisplayConvenio());
           }
       });

    $('#btnSalvarNovoConvenio').on('click', function () {
        estado ? AddUpdateConvenio('ADD') : AddUpdateConvenio('UPT', convenioKO.convenioId());
    })

    $('#modalNovoCadastroConvenio').on('shown.bs.modal', function () {
        $('#txtNomeConvenio').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(convenioKO.currentDisplayConvenio());
    });



}

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoConvenio').click();
        $('#btnSalvarNovoConvenio').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return convenioKO.nome().length > 0 && convenioKO.tipo().length > 0 
};

function convenioData() {
    this.nome = ko.observable("").extend({ ValidaCadastro: null });
    this.tipo = ko.observable("").extend({ ValidaCadastro: null });
    this.convenioId = ko.observable("");
    this.convenioList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplayConvenio = ko.observable(null);
    this.currentDisplayConvenioIdentity = ko.observable(null);

};


function setCurrentDisplayConvenio(id, name) {
    for (ct = 0; ct < convenioKO.convenioList().length; ct++) {
        var convenio = convenioKO.convenioList()[ct];
        if (convenio.convenioId == id) {
            convenioKO.currentDisplayConvenio(convenio);
            convenioKO.currentDisplayConvenioIdentity(name)
        }
    }
};

function convenio() {
    this.nome = ""
    this.tipo = ""
    this.convenioId = ""
}

function convenioRequest() {
    this.convenio = new convenio();
};

function GetListConvenios() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListConvenios',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListConvenios, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                convenioKO.convenioList(data.value);

                                if (convenioKO.currentDisplayConvenioIdentity()) {
                                    SetSelectonicFocus('#tblConvenio', convenioKO.currentDisplayConvenioIdentity());
                                }
                            } else { convenioKO.convenioList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListConvenios, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListConvenios, 15000);
        }
    }
}

function SetSelectonicFocus(table, selected) {

    var tbl = $(table + ' tbody');

    if (selected) {
        tbl.selectonic('select', $('#' + selected));
        tbl.selectonic('focus', $('#' + selected));
    }

};

function GetConvenio(convenioId) {

    var dataValue = new convenioRequest();

    dataValue.convenio.convenioId = convenioId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetConvenio',
        data: JSON.stringify(dataValue.convenio),
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
                if (data.value) {
                    if (data.value.length > 0) {
                        convenioKO.nome(data.value[0].nome);
                        convenioKO.tipo(data.value[0].tipo);
                        convenioKO.convenioId(data.value[0].convenioId);
                    }
                } else { Loader('hide'); }
            }
            Loader('hide');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError();
        }
    });
}


function showModalUPT(convenio, event) {

    var run = true;

    if (convenio) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            GetConvenio(convenio.convenioId);
            $('#btnSalvarNovoConvenio span').text('Alterar');
            $('h4').text('Alterar Cadastro do Convênio');
            $('#modalNovoCadastroConvenio').modal();
            estado = false;
        }
    }
}

function AddUpdateConvenio(acao, ConvenioID) {


    var dataValue = new convenioRequest();

    dataValue.convenio.nome = convenioKO.nome();
    dataValue.convenio.tipo = convenioKO.tipo();
    dataValue.convenio.convenioId = ConvenioID;

    $.ajax({
        type: 'POST',
        url: '../api/values/SetConvenio',
        data: JSON.stringify(dataValue),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        xhrFields: {
            withCredentials: true
        },
        beforeSend: function (xhr, opts) {
            xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
        },
        success: function (data, ajaxOptions, response) {
            var msg = '';

            if (data) {
                if (data.value) {
                    if (data.message == "OK") {
                        $('#modalNovoCadastroConvenio').modal('hide');
                        acao == 'ADD' ? msg = 'Convênio adicionado com sucesso!' : msg = 'Convênio Alterado com sucesso!';
                        modalShowing = false;
                        GetListConvenios();
                    } else {
                        msg = data.message;
                    }
                } else { msg = 'Erro ao adicionar Convênio!' }
            } else { msg = 'Erro ao adicionar Convênio!'; }

            AlertServerError(msg);

            Loader('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError('Erro ao adicionar Convênio!');
            Loader('hide');
        }
    });
}

function DeleteConvenio(data) {


    var dataValue = new convenioRequest();

    dataValue.convenio.convenioId = data.convenioId;

    alertify.confirm('Confirma a exclusão do convênio selecionado?', function (e) {
        if (e) {

            $.ajax({
                type: 'POST',
                url: '../api/values/DeleteConvenio',
                data: JSON.stringify(dataValue.convenio),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                success: function (data, ajaxOptions, response) {
                    var msg = '';

                    if (data) {
                        if (data.value) {
                            if (data.message == "OK") {
                                msg = 'Convênio excluído com sucesso!'
                                GetListConvenios();
                            } else { msg = data.message; }
                        } else { Loader('hide'); }
                    }

                    AlertServerError(msg);

                    Loader('hide');

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                }
            });
        }

    });
};

function clearFields() {

    convenioKO.nome("")
    convenioKO.tipo("")
   

}
