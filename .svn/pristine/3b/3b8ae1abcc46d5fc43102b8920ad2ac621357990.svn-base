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

                if (exameKO) {
                    exameKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var exameKO = new exameData();

$(function () {
    ExameStart();
});


function ExameStart() {

    var datako = $("[data-bind*='exameKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(exameKO, datakoelement);
    }

    Loader('show');

    GetListExames();

    $('#btnCadastrarNovoExame').on('click', function () {

        $('#modalNovoCadastroExame').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoExame span').text("Salvar");
        $('h4').text('Novo Cadastro de Exame');
    })

    $('#modalNovoCadastroExame').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroExame').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblExame', exameKO.currentDisplayExameIdentity());
    });

    $('#btnLimparExame').on('click', function () {

        clearFields();

    })


    InitSeletctonic(['tblExame'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setCurrentDisplayExame($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(exameKO.currentDisplayExame());
           }
       });

    $('#btnSalvarNovoExame').on('click', function () {
        estado ? AddUpdateExame('ADD') : AddUpdateExame('UPT', exameKO.exameId());
    })

    $('#modalNovoCadastroExame').on('shown.bs.modal', function () {
        $('#txtNomeExame').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(exameKO.currentDisplayExame());
    });


    $('#txtValorExame').mask('000.000.000.000.000,00', { reverse: true });

}

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoExame').click();
        $('#btnSalvarNovoExame').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return exameKO.nome().length > 0 && exameKO.valor().length > 0 
};

function exameData() {
    this.nome = ko.observable("").extend({ ValidaCadastro: null });
    this.valor = ko.observable("").extend({ ValidaCadastro: null });
    this.exameId = ko.observable("");
    this.exameList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplayExame = ko.observable(null);
    this.currentDisplayExameIdentity = ko.observable(null);

};


function setCurrentDisplayExame(id, name) {
    for (ct = 0; ct < exameKO.exameList().length; ct++) {
        var exame = exameKO.exameList()[ct];
        if (exame.exameId == id) {
            exameKO.currentDisplayExame(exame);
            exameKO.currentDisplayExameIdentity(name)
        }
    }
};

function exame() {
    this.nome = ""
    this.valor = ""
    this.exameId = ""
}

function exameRequest() {
    this.exame = new exame();
};

function GetListExames() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListExames',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListExames, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                exameKO.exameList(data.value);

                                if (exameKO.currentDisplayExameIdentity()) {
                                    SetSelectonicFocus('#tblExame', exameKO.currentDisplayExameIdentity());
                                }
                            } else { exameKO.exameList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListExames, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListExames, 15000);
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

function GetExame(exameId) {

    var dataValue = new exameRequest();

    dataValue.exame.exameId = exameId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetExame',
        data: JSON.stringify(dataValue.exame),
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
                        exameKO.nome(data.value[0].nome);
                        exameKO.valor(data.value[0].valor);
                        exameKO.exameId(data.value[0].exameId);
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


function showModalUPT(exame, event) {

    var run = true;

    if (exame) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            GetExame(exame.exameId);
            $('#btnSalvarNovoExame span').text('Alterar');
            $('h4').text('Alterar Cadastro do Exame');
            $('#modalNovoCadastroExame').modal();
            estado = false;
        }
    }
}

function AddUpdateExame(acao, exameId) {


    var dataValue = new exameRequest();

    dataValue.exame.nome = exameKO.nome();
    dataValue.exame.valor = exameKO.valor().replace(/[R$]+/g, "");
    dataValue.exame.exameId = exameId;

    $.ajax({
        type: 'POST',
        url: '../api/values/SetExame',
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
                        $('#modalNovoCadastroExame').modal('hide');
                        acao == 'ADD' ? msg = 'Exame adicionado com sucesso!' : msg = 'Exame Alterado com sucesso!';
                        modalShowing = false;
                        GetListExames();
                    } else {
                        msg = data.message;
                    }
                } else { msg = 'Erro ao adicionar Exame!' }
            } else { msg = 'Erro ao adicionar Exame!'; }

            AlertServerError(msg);

            Loader('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError('Erro ao adicionar Exame!');
            Loader('hide');
        }
    });
}

function DeleteExame(data) {


    var dataValue = new exameRequest();

    dataValue.exame.exameId = data.exameId;

    alertify.confirm('Confirma a exclusão do Exame selecionado?', function (e) {
        if (e) {

            $.ajax({
                type: 'POST',
                url: '../api/values/DeleteExame',
                data: JSON.stringify(dataValue.exame),
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
                                msg = 'Exame excluído com sucesso!'
                                GetListExames();
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

    exameKO.nome("")
    exameKO.valor("")
   

}
