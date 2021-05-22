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

                if (pacienteKO) {
                    pacienteKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var pacienteKO = new pacienteData();

$(function () {
    pacienteStart();
});


function pacienteStart() {

    var datako = $("[data-bind*='pacienteKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(pacienteKO, datakoelement);
    }

    Loader('show');

   GetListPaciente();

    $('#btnCadastrarNovoPaciente').on('click', function () {

        $('#modalNovoCadastroPaciente').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoPaciente span').text("Salvar");
        $('h4').text('Novo Cadastro de Paciente');
    })

    $('#modalNovoCadastroPaciente').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroPaciente').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblPaciente', pacienteKO.currentDisplaypacienteIdentity());
    });

    $('#btnLimparPaciente').on('click', function () {

        clearFields();

    })

    GetCombo('CONVENIO', $('.slcConvenio'));


    InitSeletctonic(['tblPaciente'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setCurrentDisplaypaciente($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(pacienteKO.currentDisplaypaciente());
           }
       });

    $('#btnSalvarNovoPaciente').on('click', function () {
        estado ? AddUpdatepaciente('ADD') : AddUpdatepaciente('UPT', pacienteKO.pacienteId());
    })

    $('#modalNovoCadastroPaciente').on('shown.bs.modal', function () {
        $('#txtNome_Paciente').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(pacienteKO.currentDisplaypaciente());
    });


    $("#txtDataNasc_Paciente").mask("99/99/9999");

    $("#txtCPF").mask("999.999.999-99");

    $('#txtNumero_Paciente').bind('keydown', soNums);

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
        yearRange: '1945:' + (new Date).getFullYear(),
        showButtonPanel: true,

    });

}

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoPaciente').click();
        $('#btnSalvarNovoPaciente').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return pacienteKO.nome().length > 0 && pacienteKO.dataNasc().length > 0 && $('#rua').length > 0 && $('#bairro').length > 0 && $('#uf').length > 0 && $('#cidade').length > 0 && pacienteKO.cpf().length > 0 && pacienteKO.estadoCivil().length > 0 && (pacienteKO.telefone().length > 0 || pacienteKO.celular().length > 0)
};

function pacienteData() {
    this.nome = ko.observable("").extend({ ValidaCadastro: null });
    this.dataNasc = ko.observable("").extend({ ValidaCadastro: null });
    this.endereco = ko.observable("").extend({ ValidaCadastro: null });
    this.numero = ko.observable("");
    this.complemento = ko.observable("");
    this.bairro = ko.observable("").extend({ ValidaCadastro: null });
    this.estado = ko.observable("").extend({ ValidaCadastro: null });
    this.cidade = ko.observable("").extend({ ValidaCadastro: null });
    this.convenio = ko.observable("");
    this.estadoCivil = ko.observable("").extend({ ValidaCadastro: null });
    this.sexo = ko.observable("").extend({ ValidaCadastro: null });
    this.email = ko.observable("");
    this.cpf = ko.observable("").extend({ ValidaCadastro: null });
    this.profissao = ko.observable("");
    this.telefone = ko.observable("").extend({ ValidaCadastro: null });
    this.celular = ko.observable("").extend({ ValidaCadastro: null });
    this.cep = ko.observable("");
    this.password = ko.observable("");
    this.pacienteId = ko.observable("");
    this.pacienteList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplaypaciente = ko.observable(null);
    this.currentDisplaypacienteIdentity = ko.observable(null);

};


function setCurrentDisplaypaciente(id, name) {
    for (ct = 0; ct < pacienteKO.pacienteList().length; ct++) {
        var paciente = pacienteKO.pacienteList()[ct];
        if (paciente.pacienteId == id) {
            pacienteKO.currentDisplaypaciente(paciente);
            pacienteKO.currentDisplaypacienteIdentity(name)
        }
    }
};

function paciente() {
    this.nome = ""
    this.dataNasc = ""
    this.Endereco = ""
    this.numero = ""
    this.complemento = ""
    this.bairro = ""
    this.estado = ""
    this.cidade = ""
    this.convenio = ""
    this.estadoCivil = ""
    this.sexo = ""
    this.email = ""
    this.cpf = ""
    this.profissao = ""
    this.telefone = ""
    this.celular = ""
    this.cep = ""
    this.password = ""
    this.pacienteId = ""
}

function pacienteRequest() {
    this.paciente = new paciente();
};

function GetListPaciente() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListPacientes',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListPaciente, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                pacienteKO.pacienteList(data.value);

                                if (pacienteKO.currentDisplaypacienteIdentity()) {
                                    SetSelectonicFocus('#tblPaciente', pacienteKO.currentDisplaypacienteIdentity());
                                }
                            } else { pacienteKO.pacienteList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListPaciente, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListPaciente, 15000);
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

function Getpaciente(pacienteId) {

    var dataValue = new pacienteRequest();

    dataValue.paciente.pacienteId = pacienteId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetPaciente',
        data: JSON.stringify(dataValue.paciente),
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
                        pacienteKO.nome(data.value[0].nome);
                        pacienteKO.dataNasc(moment(data.value[0].dataNasc).format('DD/MM/YYYY'));
                        pacienteKO.endereco(data.value[0].endereco);
                        pacienteKO.numero(data.value[0].numero);
                        pacienteKO.complemento(data.value[0].complemento);
                        pacienteKO.bairro(data.value[0].bairro);
                        pacienteKO.estado(data.value[0].estado);
                        pacienteKO.cidade(data.value[0].cidade);
                        pacienteKO.convenio(data.value[0].convenio);
                        pacienteKO.estadoCivil(data.value[0].estadoCivil);
                        pacienteKO.sexo(data.value[0].sexo);
                        pacienteKO.email(data.value[0].email);
                        pacienteKO.cpf(data.value[0].cpf);
                        pacienteKO.profissao(data.value[0].profissao);
                        pacienteKO.telefone(data.value[0].telefone);
                        pacienteKO.celular(data.value[0].celular);
                        pacienteKO.cep(data.value[0].cep);
                        pacienteKO.pacienteId(data.value[0].pacienteId);
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


function GetCombo(Tipo, nomeCombo) {

    var dataValue = new pacienteRequest();

    dataValue.paciente.tipo = Tipo;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetCombo',
        data: JSON.stringify(dataValue.paciente),
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
                        opt = '';

                        opt += '<option value="-1">Selecione</option>';

                        $(data.value).each(function (index) {
                            opt += '<option value="' + this.value + '"> ' + this.text + ' </option>';
                        });

                        nomeCombo.each(function () {
                            $(this).empty().append(opt);
                        })
                      
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

function showModalUPT(paciente, event) {

    var run = true;

    if (paciente) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            Getpaciente(paciente.pacienteId);
            $('#btnSalvarNovoPaciente span').text('Alterar');
            $('h4').text('Alterar Cadastro do Paciente');
            $('#modalNovoCadastroPaciente').modal();
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

function AddUpdatepaciente(acao, pacienteID) {


    var dataValue = new pacienteRequest();

    pacienteKO.endereco($('#rua').val());
    pacienteKO.bairro($('#bairro').val());
    pacienteKO.estado($('#uf').val());
    pacienteKO.cidade($('#cidade').val());

    var parts = pacienteKO.dataNasc().split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));

    dataValue.paciente.nome = pacienteKO.nome();
    dataValue.paciente.dataNasc = dt;
    dataValue.paciente.endereco = pacienteKO.endereco();
    dataValue.paciente.numero = pacienteKO.numero();
    dataValue.paciente.complemento = pacienteKO.complemento();
    dataValue.paciente.bairro = pacienteKO.bairro();
    dataValue.paciente.estado = pacienteKO.estado();
    dataValue.paciente.cidade = pacienteKO.cidade();
    dataValue.paciente.convenio = pacienteKO.convenio();
    dataValue.paciente.estadoCivil = pacienteKO.estadoCivil();
    dataValue.paciente.sexo = pacienteKO.sexo();
    dataValue.paciente.email = pacienteKO.email();
    dataValue.paciente.cpf = pacienteKO.cpf().replace(/[\(\)\.\s-]+/g, '');
    dataValue.paciente.profissao = pacienteKO.profissao();
    dataValue.paciente.telefone = pacienteKO.telefone().replace(/[\(\)\.\s-]+/g, '');
    dataValue.paciente.celular = pacienteKO.celular().replace(/[\(\)\.\s-]+/g, '');
    dataValue.paciente.cep = pacienteKO.cep().replace(/[\(\)\.\s-]+/g, '');
    dataValue.paciente.password = base64_encode(randomPassword(6));
    dataValue.paciente.pacienteId = pacienteID;

    $.ajax({
        type: 'POST',
        url: '../api/values/SetPaciente',
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
                        $('#modalNovoCadastroPaciente').modal('hide');
                        if (acao == 'ADD') {
                            if (pacienteKO.email() != '') {
                                msg = 'Paciente adicionado com sucesso! <br /> Senha Provisória: <b>' + base64_decode(dataValue.paciente.password) + '</b><br /> A mesma foi enviada para seu e-mail seguida das instruções necessárias.'
                            } else {
                                msg = 'Paciente adicionado com sucesso!'
                            }
                        } else {
                            msg = 'Paciente Alterado com sucesso!';
                        }

                        modalShowing = false;
                        GetListPaciente();
                    } else {
                        msg = data.message;
                    }
                } else { msg = 'Erro ao adicionar Paciente!' }
            } else { msg = 'Erro ao adicionar Paciente!'; }

            AlertServerError(msg);

            Loader('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError('Erro ao adicionar Paciente!');
            Loader('hide');
        }
    });
}

function DeletePaciente(data) {


    var dataValue = new pacienteRequest();

    dataValue.paciente.pacienteId = data.pacienteId

    alertify.confirm('Confirma a exclusão do paciente selecionado?', function (e) {
        if (e) {

            $.ajax({
                type: 'POST',
                url: '../api/values/DeletePaciente',
                data: JSON.stringify(dataValue.paciente),
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
                                msg = 'Paciente excluído com sucesso!'
                                GetListPaciente();
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

    pacienteKO.nome("")
    pacienteKO.dataNasc("")
    pacienteKO.endereco("")
    pacienteKO.numero("")
    pacienteKO.complemento("")
    pacienteKO.bairro("")
    pacienteKO.estado("")
    pacienteKO.cidade("")
    pacienteKO.cpf("")
    pacienteKO.convenio("")
    pacienteKO.estadoCivil("")
    pacienteKO.sexo("")
    pacienteKO.profissao("")
    pacienteKO.email("")
    pacienteKO.celular("")
    pacienteKO.telefone("")
    $('#txtCPF').prop('placeholder', '');
    $('#txtEmail').prop('placeholder', '');
    $('#lblCPF').css('color', '')
    $('#lblEmail').css('color', '')
    $('#cep').prop('placeholder', '');
    $('#formPaciente').trigger('reset');

}

