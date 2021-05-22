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

                if (atendimentoKO) {
                    atendimentoKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var atendimentoKO = new atendimentoData();

$(function () {
    AtendimentoStart();
});


function AtendimentoStart() {

    var datako = $("[data-bind*='atendimentoKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(atendimentoKO, datakoelement);
    }

    Loader('show');
    GetUsuarioAtend();
    GetListAtendimentos();

    $('#btnCadastrarNovoAtendimento').on('click', function () {

        $('#modalNovoCadastroAtendimento').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoAtendimento span').text("Salvar");
        $('h4').text('Novo Cadastro de Atendimento');
    })

    $('#modalNovoCadastroAtendimento').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroAtendimento').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblAtendimento', atendimentoKO.currentDisplayAtendimentoIdentity());
    });

    $('#btnLimparAtendimento').on('click', function () {

        clearFields();

    })

    GetCombo('ESPECIALIDADE', $('.slcEspecialidade'));
    GetCombo('MEDICOAGENDA', $('.slcMedico'));
    GetCombo('CONVENIO', $('.slcConvenio'));

   
    InitSeletctonic(['tblAtendimento'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setcurrentDisplayAtendimento($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(atendimentoKO.currentDisplayAtendimento());
           }
       });

    $('#btnSalvarNovoAtendimento').on('click', function () {
        estado ? AddUpdateAtendimento('ADD') : AddUpdateAtendimento('UPT', atendimentoKO.atendimentoId());
    })

    $('#modalNovoCadastroAtendimento').on('shown.bs.modal', function () {
        $('#txtStatus').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(atendimentoKO.currentDisplayAtendimento());
    });



    $('#divCartao').hide();
    $('#divConvenioAtendimento').hide();
    $('#btnRealizarPagamento').hide();
    $('#divValor').hide();
    $('#divFormaPgto').hide();
    

    $('#txtStatus').on('change', function () {

        if ($('#txtStatus option:selected').text() == "Compareceu") {
            $('#divFormaPgto').show();
        } else {
            $('#divFormaPgto').hide();
            $('#divCartao').hide();
            $('#divValor').hide();
            $('#divConvenioAtendimento').hide();
            atendimentoKO.numCartao('');
            atendimentoKO.convenio('');
            atendimentoKO.formapgto('');
            atendimentoKO.valor(0);
        }
    });

    $('#txtFormaPagamento').on('change', function () {

        if ($('#txtFormaPagamento option:selected').text() == "Cartão") {
            $('#divCartao').show();
            $('#divConvenioAtendimento').hide();
            $('#divValor').show();
            $('#btnRealizarPagamento').show();
            atendimentoKO.numCartao('');
            atendimentoKO.convenio('');
        } else if ($('#txtFormaPagamento option:selected').text() == "Convênio") {
            $('#divCartao').hide();
            $('#divConvenioAtendimento').show();
            $('#divValor').show();
            $('#btnRealizarPagamento').show();
            atendimentoKO.numCartao('');
            atendimentoKO.convenio('');
        } else if ($('#txtFormaPagamento option:selected').text() == "Dinheiro") {
            $('#btnRealizarPagamento').show();
            $('#divCartao').hide();
            $('#divConvenioAtendimento').hide();
            $('#divValor').show();
            atendimentoKO.numCartao('');
            atendimentoKO.convenio('');
        } else {
            $('#divCartao').hide();
            $('#divValor').hide();
            $('#divConvenioAtendimento').hide();
            $('#btnRealizarPagamento').hide();
            atendimentoKO.numCartao('');
            atendimentoKO.convenio('');
        }



    });
    $('#txtNumCartao').bind('keydown', soNums);
   
    $('#txtNumCartao').mask('9999 9999 9999 9999');

    $('#txtValor').mask('000.000.000.000.000,00', { reverse: true });

}


var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoAtendimento').click();
        $('#btnSalvarNovoAtendimento').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return atendimentoKO.statusComparecimento().length > 0
};

function atendimentoData() {
    this.data = ko.observable("");
    this.hora = ko.observable("");
    this.pacienteId = ko.observable("");
    this.nomePaciente = ko.observable("");
    this.especialidade = ko.observable("");
    this.obs = ko.observable("")
    this.dataNasc = ko.observable("");
    this.endereco = ko.observable("");
    this.numero = ko.observable("");
    this.complemento = ko.observable("");
    this.bairro = ko.observable("");
    this.estado = ko.observable("");
    this.cidade = ko.observable("");
    this.termo = ko.observable("");
    this.tipo = ko.observable("");
    this.medicoId = ko.observable("");
    this.atendimentoId = ko.observable("");
    this.statusComparecimento = ko.observable("").extend({ ValidaCadastro: null });
    this.statusPagamento = ko.observable("");
    this.valor = ko.observable("");
    this.formapgto = ko.observable("");
    this.agendamentoId = ko.observable("");
    this.numCartao = ko.observable("");
    this.convenio = ko.observable("");
    this.tipo = ko.observable("");
    this.atendimentoList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplayAtendimento = ko.observable(null);
    this.currentDisplayAtendimentoIdentity = ko.observable(null);

};


function setcurrentDisplayAtendimento(id, name) {
    for (ct = 0; ct < atendimentoKO.atendimentoList().length; ct++) {
        var atendimento = atendimentoKO.atendimentoList()[ct];
        if (atendimento.atendimentoId == id) {
            atendimentoKO.currentDisplayAtendimento(atendimento);
            atendimentoKO.currentDisplayAtendimentoIdentity(name)
        }
    }
};

function atendimento() {
    this.data = ""
    this.hora = ""
    this.pacienteId = ""
    this.nomePaciente = ""
    this.medicoId = ""
    this.especialidade = ""
    this.obs = ""
    this.termo = ""
    this.tipo = ""
    this.atendimentoId = ""
    this.agendamentoId = ""
    this.statusComparecimento = ""
    this.statusPagamento = ""
    this.valor = ""
    this.formapgto = ""
    this.convenio = ""
    this.numCartao = ""
}

function GetUsuarioAtend() {


    $.ajax({
        type: 'GET',
        url: '../api/values/GetUsuario',
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

                if (data) {
                    atendimentoKO.tipo(data.Tipo);
                   


                    if (atendimentoKO.tipo() == 2 || atendimentoKO.tipo() == 4) {
                        $('#txtStatus').prop("disabled", true);
                        $('#txtFormaPagamento').prop("disabled", true);
                        $('#txtNumCartao').prop("disabled", true);
                        $('#txtConvenio').prop("disabled", true);
                        $('#txtValor').prop("disabled", true);
                        $('#btnSalvarNovoAtendimento').hide();
                        $('#btnLimparAtendimento').hide();
                
                    }

                  


                }

            }
            Loader('hide');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError();
        }
    });
}

function atendimentoRequest() {
    this.atendimento = new atendimento();
};

function GetListAtendimentos() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListAtendimentos',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListAtendimentos, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                atendimentoKO.atendimentoList(data.value);

                                if (atendimentoKO.currentDisplayAtendimentoIdentity()) {
                                    SetSelectonicFocus('#tblAtendimento', atendimentoKO.currentDisplayAtendimentoIdentity());
                                }
                            } else { atendimentoKO.atendimentoList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListAtendimentos, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListAtendimentos, 15000);
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

function GetAtendimento(atendimentoId) {

    var dataValue = new atendimentoRequest();

    dataValue.atendimento.atendimentoId = atendimentoId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetAtendimento',
        data: JSON.stringify(dataValue.atendimento),
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
                        atendimentoKO.data(moment(data.value[0].data).format('DD/MM/YYYY'));
                        atendimentoKO.hora(data.value[0].hora);
                        atendimentoKO.pacienteId(data.value[0].pacienteId);
                        atendimentoKO.nomePaciente(data.value[0].nomePaciente);
                        atendimentoKO.medicoId(data.value[0].medicoId);
                        atendimentoKO.especialidade(data.value[0].especialidade);
                        atendimentoKO.obs(data.value[0].obs);
                        atendimentoKO.dataNasc(moment(data.value[0].dataNasc).format('DD/MM/YYYY'));
                        atendimentoKO.endereco(data.value[0].endereco);
                        atendimentoKO.numero(data.value[0].numero);
                        atendimentoKO.complemento(data.value[0].complemento);
                        atendimentoKO.bairro(data.value[0].bairro);
                        atendimentoKO.estado(data.value[0].estado);
                        atendimentoKO.cidade(data.value[0].cidade);
                        atendimentoKO.statusComparecimento(data.value[0].statusComparecimento);
                        atendimentoKO.statusPagamento(data.value[0].statusPagamento);
                        atendimentoKO.valor(data.value[0].valor);
                        atendimentoKO.formapgto(data.value[0].formapgto);
                        atendimentoKO.atendimentoId(data.value[0].atendimentoId);
                        atendimentoKO.agendamentoId(data.value[0].agendamentoId);
                       
                        selectComboBoxStatus();
                        selectComboBox();
                        atendimentoKO.numCartao(data.value[0].numCartao);
                        atendimentoKO.convenio(data.value[0].convenio);
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

function selectComboBoxStatus() {

    if (atendimentoKO.statusComparecimento() == "Compareceu") {

        $('#txtStatus option[value="Compareceu"]').prop('selected', true).change();

    }
    else if (atendimentoKO.statusComparecimento() == "Ausente") {
        $('#txtStatus option[value="Ausente"]').prop('selected', true).change();
    }
    else {
        $('#txtStatus option[value=""]').prop('selected', true).change();
    }
}

function selectComboBox() {

    if (atendimentoKO.formapgto() == "Cartão") {

        $('#txtFormaPagamento option[value="Cartão"]').prop('selected', true).change();

    }
    else if (atendimentoKO.formapgto()  == "Convênio") {
        $('#txtFormaPagamento option[value="Convênio"]').prop('selected', true).change();
    }

    else if (atendimentoKO.formapgto() == "Dinheiro") {
        $('#txtFormaPagamento option[value="Dinheiro"]').prop('selected', true).change();
    }

    else {
        $('#txtFormaPagamento option[value=""]').prop('selected', true).change();
    }
}


function showModalUPT(atendimento, event) {

    var run = true;

    if (atendimento) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            GetAtendimento(atendimento.atendimentoId);

            $('#btnSalvarNovoAtendimento span').text('Alterar');
            $('h4').text('Alterar Cadastro do Atendimento');
            $('#modalNovoCadastroAtendimento').modal();
            estado = false;
        }
    }
}

function AddUpdateAtendimento(acao, atendimentoId) {


    var dataValue = new atendimentoRequest();


    dataValue.atendimento.statusComparecimento = atendimentoKO.statusComparecimento();
    dataValue.atendimento.formapgto = atendimentoKO.formapgto();
    dataValue.atendimento.convenio = atendimentoKO.convenio();
    dataValue.atendimento.numCartao = atendimentoKO.numCartao();
    if (atendimentoKO.valor() != 0) {
        dataValue.atendimento.valor = atendimentoKO.valor().replace(/[R$]+/g, "");
    } else{
        dataValue.atendimento.valor = 0
    }

    dataValue.atendimento.agendamentoId = atendimentoKO.agendamentoId();
    dataValue.atendimento.pacienteId = atendimentoKO.pacienteId();
    dataValue.atendimento.atendimentoId = atendimentoId;


    $.ajax({
        type: 'POST',
        url: '../api/values/SetAtendimento',
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
                        $('#modalNovoCadastroAtendimento').modal('hide');
                        acao == 'ADD' ? msg = 'Atendimento adicionado com sucesso!' : msg = 'Atendimento Alterado com sucesso!';
                        modalShowing = false;
                        GetListAtendimentos();
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


function GetCombo(Tipo, nomeCombo, especialidade, paciente) {

    var dataValue = new atendimentoRequest();

    dataValue.atendimento.tipo = Tipo;

    dataValue.atendimento.especialidade = especialidade;

    dataValue.atendimento.pacienteId = paciente;


    $.ajax({
        type: 'POST',
        url: '../api/values/GetCombo',
        data: JSON.stringify(dataValue.atendimento),
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

                    } else {
                        opt = '';

                        opt += '<option value="-1">Selecione</option>';

                        nomeCombo.each(function () {
                            $(this).empty().append(opt);
                        })
                    }
                } else {
                    Loader('hide');
                }
            }
            Loader('hide');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError();
        }
    });
}

function clearFields() {

    atendimentoKO.statusComparecimento(-1);
    atendimentoKO.formapgto("");
    atendimentoKO.valor("");
    atendimentoKO.numCartao("");
    atendimentoKO.convenio("");
    selectComboBoxStatus();



}
