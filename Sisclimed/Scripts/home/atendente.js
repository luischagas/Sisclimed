﻿var estado;

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

                if (atendenteKO) {
                    atendenteKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var atendenteKO = new atendenteData();

$(function () {
    atendenteStart();
});


function atendenteStart() {

    var datako = $("[data-bind*='atendenteKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(atendenteKO, datakoelement);
    }

    Loader('show');

    GetListAtendente();

    $('#btnCadastrarNovoAtendente').on('click', function () {

        $('#modalNovoCadastroAtendente').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoAtendente span').text("Salvar");
        $('h4').text('Novo Cadastro de Atendente');
    })

    $('#modalNovoCadastroAtendente').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroAtendente').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblAtendente', atendenteKO.currentDisplayAtendenteIdentity());
    });

    $('#btnLimparAtendente').on('click', function () {

        clearFields();

    })


    InitSeletctonic(['tblAtendente'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setCurrentDisplayAtendente($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(atendenteKO.currentDisplayAtendente());
           }
       });

    $('#btnSalvarNovoAtendente').on('click', function () {
        estado ? AddUpdateAtendente('ADD') : AddUpdateAtendente('UPT', atendenteKO.atendenteId());
    })

    $('#modalNovoCadastroAtendente').on('shown.bs.modal', function () {
        $('#txtNome_Atendente').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(atendenteKO.currentDisplayAtendente());
    });


    $("#txtDataNasc_Atendente").mask("99/99/9999");

    $("#txtCPF").mask("999.999.999-99");

    $('#txtNumero_Atendente').bind('keydown', soNums);

    var dateToday = new Date();
    $(".DataNasc").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior',
        currentText: 'Agora',
        closeText: 'Fechar',
        changeMonth: true,
        changeYear: true,
        yearRange: '1945:'+(new Date).getFullYear(),         
        showButtonPanel: true,
      
    });




}

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoAtendente').click();
        $('#btnSalvarNovoAtendente').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return atendenteKO.nome().length > 0 && atendenteKO.dataNasc().length > 0 && $('#rua').length > 0 && $('#bairro').length > 0 && $('#uf').length > 0 && $('#cidade').length > 0 && atendenteKO.cpf().length > 0 && atendenteKO.email().length > 0 && (atendenteKO.telefone().length > 0 || atendenteKO.celular().length > 0)
};

function atendenteData() {
    this.nome = ko.observable("").extend({ ValidaCadastro: null });
    this.dataNasc = ko.observable("").extend({ ValidaCadastro: null });
    this.endereco = ko.observable("").extend({ ValidaCadastro: null });
    this.numero = ko.observable("");
    this.complemento = ko.observable("");
    this.bairro = ko.observable("").extend({ ValidaCadastro: null });
    this.estado = ko.observable("").extend({ ValidaCadastro: null });
    this.cidade = ko.observable("").extend({ ValidaCadastro: null });
    this.cpf = ko.observable("").extend({ ValidaCadastro: null });
    this.email = ko.observable("").extend({ ValidaCadastro: null });
    this.telefone = ko.observable("").extend({ ValidaCadastro: null });
    this.celular = ko.observable("").extend({ ValidaCadastro: null });
    this.cep = ko.observable("");
    this.atendenteId = ko.observable("");
    this.password = ko.observable("");
    this.atendenteList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplayAtendente = ko.observable(null);
    this.currentDisplayAtendenteIdentity = ko.observable(null);

};


function setCurrentDisplayAtendente(id, name) {
    for (ct = 0; ct < atendenteKO.atendenteList().length; ct++) {
        var atendente = atendenteKO.atendenteList()[ct];
        if (atendente.atendenteId == id) {
            atendenteKO.currentDisplayAtendente(atendente);
            atendenteKO.currentDisplayAtendenteIdentity(name)
        }
    }
};

function atendente() {
    this.nome = ""
    this.dataNasc = ""
    this.Endereco = ""
    this.numero = ""
    this.complemento = ""
    this.bairro = ""
    this.estado = ""
    this.cidade = ""
    this.cpf = ""
    this.email = ""
    this.telefone = ""
    this.celular = ""
    this.cep = ""
    this.password = ""
    this.atendenteId = ""
}

function atendenteRequest() {
    this.atendente = new atendente();
};

function GetListAtendente() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListAtendentes',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListAtendente, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                atendenteKO.atendenteList(data.value);

                                if (atendenteKO.currentDisplayAtendenteIdentity()) {
                                    SetSelectonicFocus('#tblAtendente', atendenteKO.currentDisplayAtendenteIdentity());
                                }
                            } else { atendenteKO.atendenteList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListAtendente, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListAtendente, 15000);
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

function GetAtendente(atendenteId) {

    var dataValue = new atendenteRequest();

    dataValue.atendente.atendenteId = atendenteId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetAtendente',
        data: JSON.stringify(dataValue.atendente),
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
                        atendenteKO.nome(data.value[0].nome);
                        atendenteKO.dataNasc(moment(data.value[0].dataNasc).format('DD/MM/YYYY'));
                        atendenteKO.endereco(data.value[0].endereco);
                        atendenteKO.numero(data.value[0].numero);
                        atendenteKO.complemento(data.value[0].complemento);
                        atendenteKO.bairro(data.value[0].bairro);
                        atendenteKO.estado(data.value[0].estado);
                        atendenteKO.cidade(data.value[0].cidade);
                        atendenteKO.cpf(data.value[0].cpf);
                        atendenteKO.email(data.value[0].email);
                        atendenteKO.celular(data.value[0].celular);
                        atendenteKO.telefone(data.value[0].telefone);
                        atendenteKO.cep(data.value[0].cep);
                        atendenteKO.atendenteId(data.value[0].atendenteId);
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


function showModalUPT(atendente, event) {

    var run = true;

    if (atendente) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            GetAtendente(atendente.atendenteId);
            $('#btnSalvarNovoAtendente span').text('Alterar');
            $('h4').text('Alterar Cadastro do Atendente');
            $('#modalNovoCadastroAtendente').modal();
            estado = false;
        }
    }
}

function randomPassword(length) {
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var pass = "";
    for (var x = 0; x < length; x++) {
        var i = Math.floor(Math.random() * chars.length);
        pass += chars.charAt(i);
    }
    return pass;
}


function AddUpdateAtendente(acao, atendenteID) {


    var dataValue = new atendenteRequest();

    atendenteKO.endereco($('#rua').val());
    atendenteKO.bairro($('#bairro').val());
    atendenteKO.estado($('#uf').val());
    atendenteKO.cidade($('#cidade').val());

    var parts = atendenteKO.dataNasc().split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));


    dataValue.atendente.nome = atendenteKO.nome();
    dataValue.atendente.dataNasc = dt;
    dataValue.atendente.endereco = atendenteKO.endereco();
    dataValue.atendente.numero = atendenteKO.numero();
    dataValue.atendente.complemento = atendenteKO.complemento();
    dataValue.atendente.bairro = atendenteKO.bairro();
    dataValue.atendente.estado = atendenteKO.estado();
    dataValue.atendente.cidade = atendenteKO.cidade();
    dataValue.atendente.cpf = atendenteKO.cpf().replace(/[\(\)\.\s-]+/g, '');
    dataValue.atendente.email = atendenteKO.email();
    dataValue.atendente.celular = atendenteKO.celular().replace(/[\(\)\.\s-]+/g, '');
    dataValue.atendente.telefone = atendenteKO.telefone().replace(/[\(\)\.\s-]+/g, '');
    dataValue.atendente.cep = atendenteKO.cep().replace(/[\(\)\.\s-]+/g, '');
    dataValue.atendente.password = base64_encode(randomPassword(6));
    dataValue.atendente.atendenteId = atendenteID;

    $.ajax({
        type: 'POST',
        url: '../api/values/SetAtendente',
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
                        $('#modalNovoCadastroAtendente').modal('hide');
                        if (acao == 'ADD') {
                            if (atendenteKO.email() != '') {
                                msg = 'Atendente adicionado com sucesso! <br /> Senha Provisória: <b>' + base64_decode(dataValue.atendente.password) + '</b><br /> A mesma foi enviada para seu e-mail seguida das instruções necessárias.'
                            } else {
                                msg = 'Atendente adicionado com sucesso!'
                            }
                        } else {
                            msg = 'Atendente Alterado com sucesso!';
                        }

                        modalShowing = false;
                        GetListAtendente();
                    } else {
                        msg = data.message;
                    }
                } else { msg = 'Erro ao adicionar Atendente!' }
            } else { msg = 'Erro ao adicionar Atendente!'; }

            AlertServerError(msg);

            Loader('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError('Erro ao adicionar Paciente!');
            Loader('hide');
        }
    });
}

function clearFields() {

    atendenteKO.nome("")
    atendenteKO.dataNasc("")
    atendenteKO.endereco("")
    atendenteKO.numero("")
    atendenteKO.complemento("")
    atendenteKO.bairro("")
    atendenteKO.estado("")
    atendenteKO.cidade("")
    atendenteKO.cpf("")
    atendenteKO.email("")
    atendenteKO.celular("")
    atendenteKO.telefone("")
    atendenteKO.cep("")
    $('#txtCPF').prop('placeholder', '');
    $('#txtEmail').prop('placeholder', '');
    $('#lblCPF').css('color', '')
    $('#lblEmail').css('color', '')
    $('#cep').prop('placeholder', '');
    $('#formAtendente').trigger('reset');

}


function DeleteAtendente(data) {


    var dataValue = new atendenteRequest();

    dataValue.atendente.atendenteId = data.atendenteId

    alertify.confirm('Confirma a exclusão do atendente selecionado?', function (e) {
        if (e) {

            $.ajax({
                type: 'POST',
                url: '../api/values/DeleteAtendente',
                data: JSON.stringify(dataValue.atendente),
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
                                msg = 'Atendente excluído com sucesso!'
                                GetListAtendente();
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