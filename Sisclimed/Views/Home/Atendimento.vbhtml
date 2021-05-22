@Code
    ViewData("Title") = "Atendimento"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div id="cfVeiculo" class="container-fluid">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-calendar" aria-hidden="true"></i> Atendimento</legend>
        </fieldset>
        <div class="col-xs-12">
            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

            </div>
            <div class="col-xs-6 col-lg-6" style="text-align:right;">
                <input type="text" id="txtSearch" class="input-search" alt="lista-atendimento" placeholder="Pesquisar" />
            </div>
        </div>
        <div class="row">
            <div id="divAtendimento">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table id="tblAtendimento" class="lista-atendimento table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Nome do Paciente</th>
                                    <th>Data Agendamento</th>
                                    <th>Hora Agendamento</th>
                                    <th>Médico</th>
                                    <th>Status</th>
                                    <th>Pagamento</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyAtendimentos" class="tbodyAtendimentos" data-bind="foreach: atendimentoKO.atendimentoList">
                                <tr data-bind="attr: { id: 'trAtendimento' + atendimentoId, 'data-Id': atendimentoId }, event: { dblclick: showModalUPT, doubletap: showModalUPT }">
                                    <td data-bind="text: nomePaciente"></td>
                                    <td data-bind="text: moment(data).format('DD/MM/YYYY')"></td>
                                    <td data-bind="text: hora"></td>
                                    <td data-bind="text: nomeMedico"></td>
                                    <td data-bind="text: statusComparecimento"></td>
                                    <td data-bind="text: statusPagamento"></td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalNovoCadastroAtendimento" data-width="700px" data-dismiss="modal" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                        <h4 class="modal-title" id="myModalLabel">Atendimento</h4>
                    </div>
                    <form name="form" data-toggle="validator">
                        <div class="modal-body">
                            <div class="container-fluid bgFFF">


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <label>Nome:</label>
                                                <input id="txtNome" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.nomePaciente, event: {keypress: enterSearch}"  disabled="disabled" required>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <label>Data Nascimento:</label>
                                                <input id="txtDataNasc" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.dataNasc, event: {keypress: enterSearch}" disabled="disabled" required>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-md-5">
                                                <label>Endereço:</label>
                                                <input id="txtEndereco_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.endereco, event: {keypress: enterSearch}"  disabled="disabled" required>
                                            </div>

                                            <div class="form-group col-md-2">
                                                <label>Número:</label>
                                                <input id="txtNumero_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.numero, event: {keypress: enterSearch}"  disabled="disabled">

                                            </div>
                                            <div class="form-group col-md-5">
                                                <label>Complemento:</label>
                                                <input id="txtComplemento_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.complemento, event: {keypress: enterSearch}" disabled="disabled">

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-md-5">
                                                <label>Bairro:</label>
                                                <input id="txtBairro_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.bairro, event: {keypress: enterSearch}" disabled="disabled">
                                            </div>

                                            <div class="form-group col-md-2">
                                                <label>Estado:</label>
                                                <input id="txtEstado_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.estado, event: {keypress: enterSearch}" disabled="disabled">
                                            </div>
                                            <div class="form-group col-md-5">
                                                <label>Cidade:</label>
                                                <input id="txtCidade_Atendimento" type="text" class="form-control input-sm" data-bind="textInput:atendimentoKO.cidade, event: {keypress: enterSearch}" disabled="disabled">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-md-3">
                                                <label>Especialidade:</label>
                                                <select id="txtEspecialidade" class="slcEspecialidade form-control input-sm" data-bind="textInput:atendimentoKO.especialidade, event: {keypress: enterSearch}" disabled="disabled"></select>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label>Médico:</label>
                                                <select id="txtMedico" class="slcMedico form-control input-sm" data-bind="textInput:atendimentoKO.medicoId, event: {keypress: enterSearch}"  disabled="disabled"></select>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label>Data:</label>
                                                <select id="txtData" class="form-control input-sm" data-bind="textInput:atendimentoKO.data, event: {keypress: enterSearch}" disabled="disabled">
                                                    <option value="">Tipo</option>
                                                    <option value="08/05/2016">08/05/2016</option>
                                                    <option value="10/02/2016">10/02/2016</option>
                                                    <option value="20/02/2016">20/02/2016</option>
                                                    <option value="28/02/2016">28/02/2016</option>
                                                </select>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label>Hora:</label>
                                                <select id="txtHora" class="form-control input-sm" data-bind="textInput:atendimentoKO.hora, event: {keypress: enterSearch}" disabled="disabled">
                                                    <option value="">Tipo</option>
                                                    <option value="10:00">10:00</option>
                                                    <option value="13:00">13:00</option>
                                                    <option value="14:00">14:00</option>
                                                    <option value="15:00">15:00</option>
                                                </select>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="form-group col-md-12">
                                                <label>Observação</label>
                                                <textarea rows="4" cols="40" maxlength="300" class="form-control input-sm" id="txtEvolucao" data-bind="textInput:atendimentoKO.obs, event: {keypress: enterSearch}" disabled="disabled"></textarea>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-md-4">
                                                <label>Status:</label>
                                                <select id="txtStatus" class="form-control input-sm" data-bind="textInput:atendimentoKO.statusComparecimento, event: {keypress: enterSearch}" tabindex="1">
                                                    <option value="">Selecione</option>
                                                    <option value="Compareceu">Compareceu</option>
                                                    <option value="Ausente">Ausente</option>

                                                </select>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <div id="divFormaPgto">
                                                    <label>Forma de pagamento:</label>
                                                    <select id="txtFormaPagamento" class="form-control input-sm" data-bind="textInput:atendimentoKO.formapgto, event: {keypress: enterSearch}" tabindex="2">
                                                        <option value="">Selecione</option>
                                                        <option value="Dinheiro">Dinheiro</option>
                                                        <option value="Cartão">Cartão</option>
                                                        <option value="Convênio">Convênio</option>
                                                    </select>
                                                </div>
                                            </div>


                                            <div class="form-group col-md-4">
                                                <div id="divCartao">
                                                    <label>Nº do Cartão</label>
                                                    <input id="txtNumCartao" class="form-control input-sm" data-bind="textInput:atendimentoKO.numCartao, event: {keypress: enterSearch}" maxlength="16" tabindex="3">
                                                </div>
                                                <div id="divConvenioAtendimento">
                                                    <label>Convênio:</label>
                                                    <select id="txtConvenio" class="slcConvenio form-control input-sm" data-bind="textInput:atendimentoKO.convenio, event: {keypress: enterSearch}" tabindex="4"></select>
                                                </div>
                                                <div id="divValor">
                                                    <label>Valor:</label>
                                                    <input id="txtValor" class="form-control input-sm" data-bind="textInput:atendimentoKO.valor, event: {keypress: enterSearch}" tabindex="5">
                                                </div>
                                            </div>


                                        </div>
                                    </div>


                                    @*<div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-4">

                                                    </div>
                                            </div>
                                        </div>*@

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

                                            </div>
                                            @*<div class="form-group col-md-6" style="text-align:right;">
                                                    <p></p>
                                                    <button id="btnRealizarPagamento" class="shortcut primary buttonPagamento" required>
                                                        <i class="icon-floppy"></i>
                                                        <span>Pagar</span>
                                                    </button>
                                                </div>*@
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button id="btnSalvarNovoAtendimento" type="button" class="shortcut primary buttonFooter" data-bind="enable: atendimentoKO.validCadastro" tabindex="6" required>
                                <i class="icon-floppy" style="margin-right:5px;"></i>
                                <span>Salvar</span>
                            </button>
                            <button id="btnLimparAtendimento" type="button" class="shortcut primary buttonFooter" required>
                                <i class="icon-remove" style="margin-right:5px;"></i>
                                <span>Limpar</span>
                            </button>

                            <button type="button" class="shortcut primary buttonFooter" data-dismiss="modal" aria-hidden="true">
                                <i class="icon-cancel-2" style="margin-right:5px;"></i>
                                <span>Fechar</span>
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>

    </div>
        </div>
        @Scripts.Render("~/bundles/atendimento")
        <script src="~/Scripts/bootstrap-contextmenu.js"></script>

     