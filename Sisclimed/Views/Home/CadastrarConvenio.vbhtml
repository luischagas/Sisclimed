@Code
    ViewData("Title") = "CadastrarConvenio"

End Code

<div id="cfVeiculo" class="container-fluid">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-credit-card" aria-hidden="true"></i> Convênios</legend>
        </fieldset>
        <div class="col-xs-12">
            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

            </div>
            <div class="col-xs-6 col-lg-6" style="text-align:right;">
                <input type="text" id="txtSearch" class="input-search" alt="lista-convenio" placeholder="Pesquisar" />
            </div>
        </div>
        <div class="row">
            <div id="divConvenio">
             
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table id="tblConvenio" class="lista-convenio table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Nome do Convênio</th>
                                    <th>Tipo</th>
                                    <th>Excluir?</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyConvenios" class="tbodyConvenios" data-bind="foreach: convenioKO.convenioList">
                                <tr data-bind="attr: { id: 'trConvenio' + convenioId, 'data-Id': convenioId }, event: { dblclick: showModalUPT, doubletap: showModalUPT }">
                                    <td data-bind="text: nome"></td>
                                    <td data-bind="text: tipo"></td>

                                    <td><div data-bind="attr: { id: 'trConvenio' + convenioId, 'data-Id': convenioId }, event: { click: DeleteConvenio }" class="img-icone"></div></td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="margin:0; text-align:right;">
                    <button id="btnCadastrarNovoConvenio" type="button" class="shortcut primary buttonInside" required>
                        <i class="icon-plus-2" style="margin-right:5px;"></i>
                        <span>Novo Cadastro</span>
                    </button>

                </div>
            </div>
            <div class="modal fade" id="modalNovoCadastroConvenio" data-width="700px" data-dismiss="modal" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                            <h4 class="modal-title" id="myModalLabel">Novo Cadastro de Convênio</h4>
                        </div>
                        <form name="form" data-toggle="validator">
                            <div class="modal-body">
                                <div class="container-fluid bgFFF">


                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Nome do Convênio:</label>
                                                    <input id="txtNomeConvenio" type="text" class="form-control input-sm" data-bind="textInput: convenioKO.nome, event: {keypress: enterSearch}" tabindex="1" required>

                                                </div>

                                                <div class="form-group col-md-6">
                                                    <label>Tipo:</label>
                                                    <select class="form-control" name="size" data-bind="textInput: convenioKO.tipo, event: {keypress: enterSearch}" tabindex="2" required>
                                                        <option value="">Tipo</option>
                                                        <option value="Individual">Individual</option>
                                                        <option value="Empresarial">Empresarial</option>
                                                    </select>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button id="btnSalvarNovoConvenio" type="button" class="shortcut primary buttonFooter" data-bind="enable: convenioKO.validCadastro" required>
                                    <i class="icon-floppy" style="margin-right:5px;"></i>
                                    <span>Salvar</span>
                                </button>

                                <button id="btnLimparConvenio" type="button" class="shortcut primary buttonFooter" required>
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

@Scripts.Render("~/bundles/convenio")
<script src="~/Scripts/bootstrap-contextmenu.js"></script>
