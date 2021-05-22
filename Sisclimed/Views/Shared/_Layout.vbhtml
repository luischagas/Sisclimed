﻿<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Sisclimed - Sistema de Clínicas Médicas</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/basejs")
    <script language="JavaScript" type="text/javascript" src="~/Scripts/cidades-estados-1.4-utf8.js"></script>
    <script language="JavaScript" type="text/javascript" src="~/Scripts/ValidacaoCPFeEmail.js"></script>    


    @Imports Sisclimed.Util

    <script>
        $(function () {
            $(".usuario a").click(function () {
                $(".openPop").toggle();
            });

            $('.usuario').click(function (event) {
                event.stopPropagation();
            });
        });


    </script>

</head>
<body>
    <header class="cd-main-header">
        <nav class="logo">
            <a href="@Url.Action("CadastrarMedico", "Home")" class="cd-logo"><img src="~/Content/images/letra3.png" alt="Logo"></a>
        </nav>


        <a href="#0" class="cd-nav-trigger"><span></span></a>

        <nav class="cd-nav">
            <ul class="cd-top-nav">
                @*<li><a href="#0">Tour</a></li>
                    <li><a href="#0">Support</a></li>*@
                <li class="has-children usuario">
                    <a style="cursor:pointer;">
                        <i class="icon-user-2" aria-hidden="true"></i> <span data-bind="text: perfilKO.nome" style="display:inline-block;"></span> | <span data-bind="text: perfilKO.nomeTipo" style="display:inline-block;"></span>
                      
                    </a>
                    <ul class="openPop">
                        <li><a>Minha Conta</a></li>
                        <li class="changePassword"><a style="cursor:pointer;">Editar Senha</a></li>
                        <li><a href="\" onclick="Logout();">Sair</a></li>
                    </ul>
                </li>
            </ul>
        </nav>
    </header> <!-- .cd-main-header -->
    @* Modal Trocar Senha *@
    <div class="modal fade" id="modalChangePassword" data-width="300px" data-dismiss="modal" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

        <div class="modal-dialog modalSenha">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h4 class="modal-title" id="myModalLabel">Editar Senha</h4>
                </div>

                <div class="modal-body">
                    <div class="container-fluid bgFFF">
                        <div class="form-group ">
                            <label>Senha Atual:</label>
                            <input id="txtPassword" class="form-control input-sm" type="password" data-bind="value: perfilKO.password" tabindex="1">
                        </div>
                        <div class="form-group ">
                            <label>Nova Senha:</label>
                            <input id="txtNewPassword" class="form-control input-sm" type="password" data-bind="value: perfilKO.newPassword" tabindex="2">
                        </div>
                        <div class="form-group ">
                            <label>Confirmar Nova Senha:</label>
                            <input id="txtReNewPassword" class="form-control input-sm" type="password" data-bind="value: perfilKO.newPasswordB" tabindex="3">
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <button id="btnChangePassword" type="button" class="shortcut primary buttonFooter" required tabindex="4">
                        <i class="icon-pencil" style="margin-right:5px;"></i>
                        <span>Alterar</span>
                    </button>

                    <button type="button" class="shortcut primary buttonFooter" data-dismiss="modal" aria-hidden="true">
                        <i class="icon-cancel-2" style="margin-right:5px;"></i>
                        <span>Fechar</span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <main class="cd-main-content">
        <nav class="cd-side-nav">

            <ul>
                <li class="cd-label">Menu</li>
                <li class="has-children overview active" id="GpCadastro" style="display:none">
                    <a href="#0" class="botaoMenu">Cadastros</a>


                    <ul>

                        <li id="menuMedico" style="display:none" class="@Html.IsActive("Home", "CadastrarMedico")"><a href="@Url.Action("CadastrarMedico", "Home")">Médico</a></li>
                        <li id="menuAtendente" style="display:none" class="@Html.IsActive("Home", "CadastrarAtendente")"><a href="@Url.Action("CadastrarAtendente", "Home")">Atendente</a></li>
                        <li id="menuPaciente" style="display:none" class="@Html.IsActive("Home", "CadastrarPaciente")"><a href="@Url.Action("CadastrarPaciente", "Home")">Paciente</a></li>
                        <li id="menuConvenio" style="display:none" class="@Html.IsActive("Home", "CadastrarConvenio")"><a href="@Url.Action("CadastrarConvenio", "Home")">Convênio</a></li>
                        <li id="menuExame" style="display:none" class="@Html.IsActive("Home", "CadastrarExame")"><a href="@Url.Action("CadastrarExame", "Home")">Exame</a></li>
                        <li id="menuHistorico" style="display:none" class="@Html.IsActive("Home", "CadastrarHistorico")"><a href="@Url.Action("CadastrarHistorico", "Home")">Histórico</a></li>

                    </ul>
                </li>
                @*<li class="has-children notifications active">
                        <a href="#0">Consultas</a>

                        <ul>
                            <li class="@Html.IsActive("Home", "Teste")"><a href="@Url.Action("Teste", "Home")">Consultar Médico</a></li>
                            <li><a href="#0">Consultar Atendente</a></li>
                            <li><a href="#0">Consultar Paciente</a></li>
                            <li><a href="#0">Consultar Histórico</a></li>
                        </ul>
                    </li>*@



                <li class="has-children comments active" id="GpAgenda" style="display:none">
                    <a href="#0" class="botaoMenu">Agendamento</a>
                    <ul>
                        <li id="menuAgendamento" style="display:none" class="@Html.IsActive("Home", "CadastrarAgendamento")"><a href="@Url.Action("CadastrarAgendamento", "Home")">Agendar Consultas</a></li>
                        <li id="menuAtendimento" style="display:none" class="@Html.IsActive("Home", "Atendimento")"><a href="@Url.Action("Atendimento", "Home")">Atendimento</a></li>
                    </ul>
                </li>


                <li class="has-children relatorios active" id="GpRelatorio" style="display:none">
                    <a href="#0" class="botaoMenu">Relatórios</a>
                    <ul>
                        <li id="menuRelatorioAgendamento" style="display:none" class="@Html.IsActive("Home", "RelatorioAgendamento")"><a href="@Url.Action("RelatorioAgendamento", "Home")">Relatório Agendamento</a></li>
                        <li id="menuRelatorioAtendimento" style="display:none" class="@Html.IsActive("Home", "RelatorioAtendimento")"><a href="@Url.Action("RelatorioAtendimento", "Home")">Relatório Atendimento</a></li>
                        @*<li id="menurRelatorioAuditoria" style="display:none" class="@Html.IsActive("Home", "RelatorioAuditoria")"><a href="@Url.Action("RelatorioAuditoria", "Home")">Relatório Auditoria</a></li>*@

                    </ul>

                </li>
            </ul>


            @*<div class="rodape">
                <img class="logotipo" src="~/Content/images/logo2.png" alt="Logo">
            </div>*@
        </nav>

        <div class="content-wrapper">
            @RenderBody()
        </div>


    </main>

    @Scripts.Render("~/bundles/bootstrap")    
    @Scripts.Render("~/bundles/home")


    
          

    <script type="text/javascript" src="~/Scripts/jquery.maskedinput.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="~/Scripts/jquery.maskMoney.js"></script>
    <script type="text/javascript" src="~/Scripts/jquery.mask.min.js"></script>


</body>

</html>
