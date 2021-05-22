﻿var perfilKO = new perfilData();
$(function () {
    var logged = getAuthorization();

    GetUsuario();

    var datako = $("[data-bind*='perfilKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(perfilKO, datakoelement);
    }
    $('.changePassword a').on('click', function () {

        $('#modalChangePassword').modal()
        $('#txtPassword').focus();
        $('#txtPassword').val('');
        $('#txtNewPassword').val('');
        $('#txtReNewPassword').val('');

        $('h4').text('Editar Senha');

    })
    $('#btnChangePassword').on('click', function () {
        var newPasswordB = perfilKO.newPasswordB();
        var password = perfilKO.newPassword();
        var regex = new RegExp("^[a-zA-Z0-9!@#$%\^&*)(+=._-]*$")

        if (password != newPasswordB) {
            AlertServerError('As senhas devem ser iguais!');
        } else if (newPasswordB.indexOf(" ") >= 0) {
            AlertServerError('A senha não pode conter espaço em branco!');
        } else if (!regex.test(newPasswordB)) {
            AlertServerError('A senha não pode conter acentos!');
        } else if (newPasswordB.length < 4) {
            AlertServerError('A senha deve conter mais que 4 caracteres!');
        }
        else {
            RedefinirSenha();;
        }
        perfilKO.password("");
        perfilKO.newPassword("");
        perfilKO.newPasswordB("");
    });

    $('#txtPassword').focus().select();
        
  
});


function perfilData() {
    this.usuarioId = ko.observable("")
    this.login = ko.observable("")
    this.nome = ko.observable("");
    this.tipo = ko.observable("");
    this.nomeTipo = ko.observable("");
    this.NumeroAcesso = ko.observable("");
    this.password = ko.observable("");
    this.newPassword = ko.observable("");
    this.newPasswordB = ko.observable("");
};

function GetUsuario() {


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
                        perfilKO.login(data.Login);
                        perfilKO.nome(data.Nome);
                        perfilKO.tipo(data.Tipo);
                        perfilKO.usuarioId(data.UsuarioId);
                        perfilKO.NumeroAcesso(data.NumeroAcesso);


                        if (perfilKO.tipo() == 3) {
                            $('#GpCadastro').show();
                            $('#menuPaciente').show();
                            $('#menuConvenio').show();
                            $('#menuExame').show();
                            $('#menuAgendamento').show();
                            $('#menuAtendimento').show();
                            $('#GpAgenda').show();
                            $('#btnCadastrarNovoAgendamento').show();
                            $('#lblExcluirAgendamento').show();
                            $('#iconExcluirAgendamento').show();
                            $('#btnSalvarNovoAgendamento').show();
                            $('#btnLimparAgendamento').show();
                            $('#btnSalvarNovoHistorico').show();
                            $('#btnLimparHistorico').show();
                        } else if (perfilKO.tipo() == 2) {
                            $('#GpCadastro').show();
                            $('#menuHistorico').show();
                            $('#GpAgenda').show();
                            $('#menuAgendamento').show();
                            $('#menuAtendimento').show();
                            $('#txtNomeAgendamento').prop("disabled", true);
                            $('#txtEspecialidadeAgendamento').prop("disabled", true);
                            $('#txtMedicoAgendamento').prop("disabled", true);
                            $('#txtDataAgendamento').prop("disabled", true);
                            //$('#txtHoraAgendamento').prop("disabled", true);
                            $('#txtObsAgendamento').prop("disabled", true);
                            $('#btnSalvarNovoHistorico').show();
                            $('#btnLimparHistorico').show();
                        } else if (perfilKO.tipo() == 4) {
                            $('#GpCadastro').show();
                            $('#menuHistorico').show();
                            $('#GpAgenda').show();
                            $('#menuAgendamento').show();
                            $('#menuAtendimento').show();
                            $('#txtNomeAgendamento').prop("disabled", true);
                            $('#txtEspecialidadeAgendamento').prop("disabled", true);
                            $('#txtMedicoAgendamento').prop("disabled", true);
                            $('#txtDataAgendamento').prop("disabled", true);
                            //$('#txtHoraAgendamento').prop("disabled", true);
                            $('#txtObsAgendamento').prop("disabled", true);
                            $('#txtSintomas').prop("disabled", true);
                            $('#txtEvolucao').prop("disabled", true);
                            $('#txtReceita').prop("disabled", true);
                            $('#txtExame').prop("disabled", true);
                        }
                        else {
                            $('#GpCadastro').show();
                            $('#menuMedico').show();
                            $('#menuAtendente').show();
                            $('#menuPaciente').show();
                            $('#menuConvenio').show();
                            $('#menuExame').show();
                            $('#menuHistorico').show();
                            $('#menuAgendamento').show();
                            $('#menuAtendimento').show();
                            $('#GpAgenda').show();
                            $('#GpRelatorio').show();
                            $('#menuRelatorioAgendamento').show();
                            $('#menuRelatorioAtendimento').show();
                            //$('#menurRelatorioAuditoria').show();
                            $('#btnCadastrarNovoAgendamento').show();
                            $('#lblExcluirAgendamento').show();
                            $('#iconExcluirAgendamento').show();
                            $('#btnSalvarNovoAgendamento').show();
                            $('#btnLimparAgendamento').show();
                            $('#btnSalvarNovoHistorico').show();
                            $('#btnLimparHistorico').show();
                        }

                        if (perfilKO.tipo() == 1) {
                            perfilKO.nomeTipo('Administrador');
                        } else if (perfilKO.tipo() == 2) {
                            perfilKO.nomeTipo('Médico');
                        } else if (perfilKO.tipo() == 3) {
                            perfilKO.nomeTipo('Atendente');
                        } else if (perfilKO.tipo() == 4) {
                            perfilKO.nomeTipo('Paciente');
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
function RedefinirSenhaJS() {
    this.code = '';
    this.password = '';
    this.newPassword = '';
};

function RedefinirSenha()
{
    var redefinirSenha = new RedefinirSenhaJS();
    redefinirSenha.code = perfilKO.usuarioId();    
    redefinirSenha.password = base64_encode(perfilKO.password());
    redefinirSenha.newPassword = base64_encode(perfilKO.newPassword());

    $.ajax({
        type: 'POST',
        url: '../api/values/EditarSenha',
        data: JSON.stringify(redefinirSenha),
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
                        $('#modalChangePassword').modal('hide');
                       msg = 'Senha alterada com sucesso!'
                        
                    } else {
                        msg = data.message;
                    }
                } else { msg = 'Erro ao redefinir senha!' }
            } else { msg = 'Erro ao redefinir senha!' }

            AlertServerError(msg);

            Loader('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError('Erro ao redefinir senha!');
            Loader('hide');
        }
    });
}