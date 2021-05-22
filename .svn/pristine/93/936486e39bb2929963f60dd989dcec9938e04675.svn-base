@Code
    ViewData("Title") = "CadastrarExame"
End Code

<div id="cfVeiculo" class="container-fluid">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-book" aria-hidden="true"></i> Exames</legend>
        </fieldset>
        <div class="col-xs-12">
            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

            </div>
            <div class="col-xs-6 col-lg-6" style="text-align:right;">
                <input type="text" id="txtSearch" class="input-search" alt="lista-exame" placeholder="Pesquisar" />
            </div>
        </div>
        <div class="row">
            <div id="divExame">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table id="tblExame" class="lista-exame table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Nome do Exame</th>
                                    <th>Valor</th>
                                    <th>Excluir?</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyExames" class="tbodyExames" data-bind="foreach: exameKO.exameList">
                                <tr data-bind="attr: { id: 'trExame' + exameId, 'data-Id': exameId }, event: { dblclick: showModalUPT, doubletap: showModalUPT }">
                                    <td data-bind="text: nome"></td>
                                    <td data-bind="text: valor"></td>

                                    <td><div class="img-icone" data-bind="attr: { id: 'trExame' + exameId, 'data-Id': exameId }, event: { click: DeleteExame }"></div></td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12" style="margin:0; text-align:right;">
                    <button id="btnCadastrarNovoExame" type="button" class="shortcut primary buttonInside" required>
                        <i class="icon-plus-2" style="margin-right:5px;"></i>
                        <span>Novo Cadastro</span>
                    </button>

                </div>
            </div>
            <div class="modal fade" id="modalNovoCadastroExame" data-width="700px" data-dismiss="modal" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                            <h4 class="modal-title" id="myModalLabel">Novo Cadastro de Exame</h4>
                        </div>
                        <form name="form" data-toggle="validator">
                            <div class="modal-body">
                                <div class="container-fluid bgFFF">


                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Nome do Exame:</label>
                                                    <input id="txtNomeExame" class="form-control input-sm" data-bind="textInput: exameKO.nome, event: {keypress: enterSearch}" tabindex="1" required>
                                                </div>

                                                <div class="form-group col-md-6">
                                                    <label>Valor:</label>
                                                    <input id="txtValorExame" class="form-control input-sm" data-bind="textInput: exameKO.valor, event: {keypress: enterSearch}" tabindex="2" required>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button id="btnSalvarNovoExame" type="button" class="shortcut primary buttonFooter" data-bind="enable: exameKO.validCadastro" required>
                                    <i class="icon-floppy" style="margin-right:5px;"></i>
                                    <span>Salvar</span>
                                </button>

                                <button id="btnLimparExame" type="button" class="shortcut primary buttonFooter" required>
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

@Scripts.Render("~/bundles/exame")
<script src="~/Scripts/bootstrap-contextmenu.js"></script>
