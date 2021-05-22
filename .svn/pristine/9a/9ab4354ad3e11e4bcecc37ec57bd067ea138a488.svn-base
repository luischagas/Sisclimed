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

                if (historicoKO) {
                    historicoKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var historicoKO = new historicoData();

$(function () {
    HistoricoStart();
});


function HistoricoStart() {

    var datako = $("[data-bind*='historicoKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(historicoKO, datakoelement);
    }

    Loader('show');

    GetListHistoricos();

    $('#btnCadastrarNovoHistorico').on('click', function () {

        $('#modalNovoCadastroHistorico').modal()
        estado = true;
        clearFields();
        $('#btnSalvarNovoHistorico span').text("Salvar");
        $('h4').text('Novo Cadastro de Histórico');
    })

    $('#modalNovoCadastroHistorico').on('shown.bs.modal', function (e) {
        modalShowing = true;
    });

    $('#modalNovoCadastroHistorico').on('hidden.bs.modal', function (e) {
        modalShowing = false;
        SetSelectonicFocus('#tblHistorico', historicoKO.currentDisplayHistoricoIdentity());
    });

    $('#btnLimparHistorico').on('click', function () {

        clearFields();

    })

    GetCombo('ESPECIALIDADE', $('.slcEspecialidade'));
    GetCombo('MEDICOAGENDA', $('.slcMedico'));


    InitSeletctonic(['tblHistorico'],
       function (event, ui) { //Select Event
           if (!modalShowing) {
               setcurrentDisplayHistorico($(ui.items).data("id"), $(ui.items).attr("id"));
           }
       },

       function (event, ui) { // Unselect Event

       },

       function (event, ui) { //KeyDown Event

           if (event.originalEvent.which == 13) {
               showModalUPT(historicoKO.currentDisplayHistorico());
           }
       });

    $('#btnSalvarNovoHistorico').on('click', function () {
        estado ? AddUpdateHistorico('ADD') : AddUpdateHistorico('UPT', historicoKO.historicoId());
    })

    $('#modalNovoCadastroHistorico').on('shown.bs.modal', function () {
        $('#txtSintomas').focus();
    })

    $('.j-selected').on('keydown', function () {
        showModalUPT(historicoKO.currentDisplayHistorico());
    });



 

}


var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoHistorico').click();
        $('#btnSalvarNovoHistorico').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return historicoKO.data().length > 0 && historicoKO.hora().length > 0
};

function historicoData() {
    this.data = ko.observable("").extend({ ValidaCadastro: null });
    this.hora = ko.observable("").extend({ ValidaCadastro: null });
    this.pacienteId = ko.observable("").extend({ ValidaCadastro: null });
    this.nomePaciente = ko.observable("").extend({ ValidaCadastro: null });
    this.especialidade = ko.observable("").extend({ ValidaCadastro: null });
    this.dataNasc = ko.observable("");
    this.cpf = ko.observable("");
    this.medicoId = ko.observable("");
    this.sintomas = ko.observable("");
    this.evolucao = ko.observable("");
    this.receita = ko.observable("");
    this.exames = ko.observable("");
    this.atendimentoId = ko.observable("");
    this.historicoId = ko.observable("");
    this.historicoList = ko.observableArray([]);
    this.validCadastro = ko.observable(false);
    this.currentDisplayHistorico = ko.observable(null);
    this.currentDisplayHistoricoIdentity = ko.observable(null);

};


function setcurrentDisplayHistorico(id, name) {
    for (ct = 0; ct < historicoKO.historicoList().length; ct++) {
        var historico = historicoKO.historicoList()[ct];
        if (historico.historicoId == id) {
            historicoKO.currentDisplayHistorico(historico);
            historicoKO.currentDisplayHistoricoIdentity(name)
        }
    }
};

function historico() {
    this.data = ""
    this.hora = ""
    this.pacienteId = ""
    this.nomePaciente = ""
    this.medicoId = ""
    this.especialidade = ""
    this.cpf = ""
    this.atendimentoId = ""
    this.sintomas = ""
    this.evolucao = ""
    this.receita = ""
    this.exames = ""
    this.historicoId = ""
  
}

function historicoRequest() {
    this.historico = new historico();
};

function GetListHistoricos() {
    if ($('#txtSearch').val() == '') {
        if (!modalShowing) {
            $.ajax({
                type: 'GET',
                url: '../api/values/GetListHistoricos',
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListHistoricos, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                historicoKO.historicoList(data.value);

                                if (historicoKO.currentDisplayHistoricoIdentity()) {
                                    SetSelectonicFocus('#tblHistorico', historicoKO.currentDisplayHistoricoIdentity());
                                }
                            } else { historicoKO.historicoList(data.value); Loader('hide'); }
                        } else { Loader('hide'); }
                    }

                    setTimeout(GetListHistoricos, 15000);

                    $('progress').val(10000);

                    var refreshIntervalId = setInterval(function () {
                        if ($('progress').val() == 0) { clearInterval(refreshIntervalId); }
                        $('progress').val(($('progress').val() - 50));
                    }, 50);

                    Loader('hide');
                },


            });

        } else {
            setTimeout(GetListHistoricos, 15000);
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

function GetHistorico(historicoId) {

    var dataValue = new historicoRequest();

    dataValue.historico.historicoId = historicoId;

    $.ajax({
        type: 'POST',
        url: '../api/values/GetHistorico',
        data: JSON.stringify(dataValue.historico),
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
                        historicoKO.data(moment(data.value[0].data).format('DD/MM/YYYY'));
                        historicoKO.hora(data.value[0].hora);
                        historicoKO.pacienteId(data.value[0].pacienteId);
                        historicoKO.nomePaciente(data.value[0].nomePaciente);
                        historicoKO.medicoId(data.value[0].medicoId);
                        historicoKO.especialidade(data.value[0].especialidade);
                        historicoKO.dataNasc(moment(data.value[0].dataNasc).format('DD/MM/YYYY'));
                        historicoKO.cpf(data.value[0].cpf);
                        historicoKO.atendimentoId(data.value[0].atendimentoId);
                        historicoKO.sintomas(data.value[0].sintomas);
                        historicoKO.evolucao(data.value[0].evolucao);
                        historicoKO.receita(data.value[0].receita);
                        historicoKO.exames(data.value[0].exames);
                        historicoKO.historicoId(data.value[0].historicoId);
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



function showModalUPT(historico, event) {

    var run = true;

    if (historico) {

        if (event) {
            if (event.keyCode) {
                if (event.keyCode != 13) { run = false; }
            }
        }

        if (run) {
            clearFields();
            GetHistorico(historico.historicoId);

            $('#btnSalvarNovoHistorico span').text('Alterar');
            $('h4').text('Alterar Cadastro do Historico');
            $('#modalNovoCadastroHistorico').modal();
            estado = false;
        }
    }
}

function AddUpdateHistorico(acao, historicoId) {


    var dataValue = new historicoRequest();


    dataValue.historico.atendimentoId = historicoKO.atendimentoId();
    dataValue.historico.sintomas = historicoKO.sintomas();
    dataValue.historico.evolucao = historicoKO.evolucao();
    dataValue.historico.receita = historicoKO.receita();
    dataValue.historico.exames = historicoKO.exames();
    dataValue.historico.historicoId = historicoId;


    $.ajax({
        type: 'POST',
        url: '../api/values/SetHistorico',
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
                        $('#modalNovoCadastroHistorico').modal('hide');
                        acao == 'ADD' ? msg = 'Historico adicionado com sucesso!' : msg = 'Historico Alterado com sucesso!';
                        modalShowing = false;
                        GetListHistoricos();
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

    var dataValue = new historicoRequest();

    dataValue.historico.tipo = Tipo;

    dataValue.historico.especialidade = especialidade;

    dataValue.historico.pacienteId = paciente;


    $.ajax({
        type: 'POST',
        url: '../api/values/GetCombo',
        data: JSON.stringify(dataValue.historico),
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

    historicoKO.sintomas("");
    historicoKO.evolucao("");
    historicoKO.receita("");
    historicoKO.exames("");
    



}
