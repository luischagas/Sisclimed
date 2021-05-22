@Code
    ViewData("Title") = "Relatorios"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code



<div id="cfVeiculo" class="container-fluid">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-calendar" aria-hidden="true"></i> Relatório de Agendamento</legend>
        </fieldset>
      
        <div class="row">

            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>Data Inicial:</label>
                        <input id="txtDataInicial" type="text" class="dataInicial form-control input-sm" data-bind="textInput:relatorioKO.dataInicial, event: {keypress: enterSearch}" tabindex="1" >
                    </div>

                    <div class="form-group col-md-4">
                        <label>Data Final:</label>
                        <input id="txtDataFinal" type="text" class="dataFinal form-control input-sm" data-bind="textInput:relatorioKO.dataFinal, event: {keypress: enterSearch}" tabindex="2">

                    </div>

                    <div class="form-group col-md-4">
                        <label>Médico:</label>
                        <select id="txtMedico" type="text" class="slcMedico form-control input-sm" data-bind="textInput:relatorioKO.medicoId, event: {keypress: enterSearch}" tabindex="3"></select>
                    </div>
                  


                </div>
            </div>

            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>Especialidade:</label>
                        <select id="txtEspecialidade" type="text" class="slcEspecialidade form-control input-sm" data-bind="textInput:relatorioKO.especialidadeId, event: {keypress: enterSearch}" tabindex="4"></select>
                    </div>

                    <div class="form-group col-md-4">
                        <label>Paciente:</label>
                        <select id="txtDataInicial" type="text" class="slcPaciente form-control input-sm" data-bind="textInput:relatorioKO.pacienteId, event: {keypress: enterSearch}" tabindex="5"></select>
                    </div>

                    <div class="form-group col-md-4 botaoRelatorio">
                        <button id="btnPesquisar" type="button" class="shortcut primary buttonFooter" tabindex="6" required>
                            <i class="icon-plus-2" style="margin-right:5px; text-align:right"></i>
                            <span>Gerar</span>
                        </button>
                        <button id="btnExportar" type="button" class="shortcut primary buttonFooter" data-bind="enable: relatorioKO.validCadastro" required>
                            <i class="icon-plus-2" style="margin-right:5px; text-align:right"></i>
                            <span>Exportar</span>
                        </button>
                        </div>

                    </div>
                    </div>

            <div id="divRelatorios">
               
                <div class="col-md-12">
                    <div id="divCont" style="text-align:right"></div>
                    <div class="table-responsive">
                        <table id="tblRelatorio" class="lista-agendamento table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Paciente</th>
                                    <th>CPF</th>
                                    <th>Data</th>
                                    <th>Hora</th>
                                    <th>Médico</th>
                                    <th>Especialidade</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyRelatorios" class="tbodyRelatorios" data-bind="foreach: relatorioKO.relatorioList">
                                <tr data-bind="attr: { id: 'trRelatorio'}">
                                    <td data-bind="text: nomePaciente"></td>
                                    <td data-bind="text: cpf"></td>
                                    <td data-bind="text: moment(data).format('DD/MM/YYYY')"></td>
                                    <td data-bind="text: hora"></td>
                                    <td data-bind="text: nomeMedico"></td>
                                    <td data-bind="text: especialidade"></td>
                                </tr>
                            </tbody>
                                              
                         </table>
                </div>
            </div>
        </div>
            </div>
            </div>
        </div>


@Scripts.Render("~/bundles/relatorioAgendamento")
<script src="~/Scripts/jspdf.debug.js"></script>
<script src="~/Scripts/jspdf.plugin.autotable.js"></script>