﻿@Code
    ViewData("Title") = "Cadastrar Médico"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<div id="cfVeiculo" class="container-fluid containersh">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-user-3" aria-hidden="true"></i> Médicos</legend>
        </fieldset>
        <div class="col-xs-12">
            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

            </div>
            <div id="divPesquisar" class="col-xs-6 col-lg-6" style="text-align:right;">
                <input type="text" id="txtSearch" class="input-search" alt="lista-medico" placeholder="Pesquisar" />
            </div>
        </div>
        <div class="row">
            <div id="divMedico">
              
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table id="tblMedico" class="lista-medico table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Nome do Médico</th>
                                    <th>CPF</th>
                                    <th>Especialidade</th>
                                    <th>CRM</th>
                                    <th>Excluir?</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyMedicos" class="tbodyMedicos" data-bind="foreach: medicoKO.medicoList">
                                <tr data-bind="attr: { id: 'trMedico' + medicoId, 'data-Id': medicoId }, event: { dblclick: showModalUPT, doubletap: showModalUPT }">
                                    <td data-bind="text: nome"></td>
                                    <td data-bind="text: cpf"></td>
                                    <td data-bind="text: nomeEspecialidade"></td>
                                    <td data-bind="text: crm"></td>
                                    <td><div data-bind="attr: { id: 'trMedico' + medicoId, 'data-Id': medicoId }, event: { click: DeleteMedico }" class="img-icone"></div></td>

                                </tr>
                            </tbody>


                        </table>
                    </div>
                </div>
            </div>
</div>
            <div class="row">
                <div class="col-md-12" style="margin:0; text-align:right;">
                    <button id="btnCadastrarNovoMedico" type="button" class="shortcut primary buttonInside" required>
                        <i class="icon-plus-2" style="margin-right:5px;"></i>
                        <span>Novo Cadastro</span>
                    </button>

                </div>
            </div>
            <div class="modal fade" id="modalNovoCadastroMedico" data-dismiss="modal" data-width="700px" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                            <h4 class="modal-title" id="myModalLabel">Novo Cadastro de Médico</h4>
                        </div>
                        <form name="form" id="formMedico" data-toggle="validator">
                            <div class="modal-body">
                                <div class="container-fluid bgFFF">


                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Nome:</label>
                                                    <input id="txtNome_Medico" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.nome, event: {keypress: enterSearch}" tabindex="1" required>

                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label>Data Nascimento:</label>
                                                    <input id="txtDataNasc_Medico" type="text" class="DataNasc form-control input-sm" data-bind="textInput: medicoKO.dataNasc, event: {keypress: enterSearch}" tabindex="2" required>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label id="lblCEP">CEP:</label>
                                                    <input id="cep" type="text" class="form-control input-sm" tabindex="3" data-bind="textInput: medicoKO.cep, event: {keypress: enterSearch}" onblur="pesquisacep(this.value);">

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-5">
                                                    <label>Endereço:</label>
                                                    <input id="rua" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.endereco, event: {keypress: enterSearch}" tabindex="4" required>
                                                </div>

                                                <div class="form-group col-md-2">
                                                    <label>Número:</label>
                                                    <input id="txtNumero_Medico" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.numero, event: {keypress: enterSearch}" tabindex="5">

                                                </div>
                                                <div class="form-group col-md-5">
                                                    <label>Complemento:</label>
                                                    <input id="txtComplemento_Medico" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.complemento, event: {keypress: enterSearch}" tabindex="6">

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-5">
                                                    <label>Bairro:</label>
                                                    <input id="bairro" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.bairro, event: {keypress: enterSearch}" tabindex="7" required>

                                                </div>

                                                <div class="form-group col-md-2">
                                                    <label>Estado:</label>
                                                    <input id="uf" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.estado, event: {keypress: enterSearch}" tabindex="8" required>
                                                </div>
                                                <div class="form-group col-md-5">
                                                    <label>Cidade:</label>
                                                    <input id="cidade" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.cidade, event: {keypress: enterSearch}" tabindex="9" required>
                                                </div>



                                            </div>
                                        </div>



                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label id="lblCPF">CPF:</label>
                                                    <input id="txtCPF" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.cpf, event: {keypress: enterSearch}" tabindex="10">

                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label id="lblEmail">E-mail:</label>
                                                    <input id="txtEmail" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.email, event: {keypress: enterSearch}" tabindex="11" onblur="validacaoEmail(this)">

                                                </div>

                                            

                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Especialidade:</label>
                                                    <select id="txtEspecialidade " class="slcEspecialidade form-control input-sm" data-bind="textInput: medicoKO.especialidade, event: {keypress: enterSearch}" tabindex="12" required>
                                                      
                                                    </select>

                                                </div>

                                                <div class="form-group col-md-6">
                                                    <label>CRM:</label>
                                                    <input id="txtCRM" type="text" class="form-control input-sm" data-bind="textInput: medicoKO.crm, event: {keypress: enterSearch}" tabindex="13" required>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12">
                                            <div class="row" id="jornada">
                                                <div class="form-group col-md-6" >
                                                    <p><label>Dia da Semana:</label></p>
                                                    <input type="checkbox" data-bind="checked: medicoKO.seg" id="chkSeg"> Segunda-Feira
                                                    <br>
                                                    <input type="checkbox"  data-bind="checked: medicoKO.ter"id="chkTer"> Terça-Feira
                                                    <br>
                                                    <input type="checkbox"  data-bind="checked: medicoKO.qua"id="chkQua" > Quarta-Feira
                                                    <br>
                                                    <input type="checkbox" data-bind="checked: medicoKO.qui" id="chkQui"  > Quinta-Feira
                                                    <br>
                                                    <input type="checkbox"  data-bind="checked: medicoKO.sex"id="chkSex" > Sexta-Feira
                                                    <br>
                                                        
</div>

                                                <div class="form-group col-md-6">
                                                    <p><label>Turno:</label></p>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optSeg" data-bind="checked: medicoKO.turnoSeg" tabindex="12" value="M">Manhã
                                                    </label>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optSeg" data-bind="checked: medicoKO.turnoSeg" tabindex="13" value="T" >Tarde
                                                    </label><br>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optTer" data-bind="checked: medicoKO.turnoTer" tabindex="12" value="M" d>Manhã
                                                    </label>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optTer"  data-bind="checked: medicoKO.turnoTer"tabindex="13" value="T" >Tarde
                                                    </label><br>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optQua"  data-bind="checked: medicoKO.turnoQua"tabindex="12" value="M">Manhã
                                                    </label>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optQua" data-bind="checked: medicoKO.turnoQua" tabindex="13" value="T">Tarde
                                                    </label><br>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optQui" data-bind="checked: medicoKO.turnoQui"tabindex="12" value="M">Manhã
                                                    </label>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optQui" data-bind="checked: medicoKO.turnoQui" tabindex="13" value="T">Tarde
                                                    </label><br>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optSex" data-bind="checked: medicoKO.turnoSex" tabindex="12" value="M">Manhã
                                                    </label>
                                                    <label class="form-inline radio-inline">
                                                        <input type="radio" id="optSex" data-bind="checked: medicoKO.turnoSex" tabindex="13" value="T" >Tarde
                                                    </label>

                                                </div>
                                            </div>

                                        </div>


                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button id="btnSalvarNovoMedico" type="button" class="shortcut primary buttonFooter" data-bind="enable: medicoKO.validCadastro" tabindex="14" required>
                                    <i class="icon-floppy" style="margin-right:5px;"></i>
                                    <span>Salvar</span>
                                </button>
                                <button id="btnLimparMedico" type="button" class="shortcut primary buttonFooter" required>
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
@Scripts.Render("~/bundles/medico")

