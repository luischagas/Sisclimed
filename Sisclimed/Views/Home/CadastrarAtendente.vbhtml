@Code
    ViewData("Title") = "Cadastrar Atendente"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<div id="cfVeiculo" class="container-fluid">
    <div class="divCadastro">
        <fieldset>
            <legend><i class="icon-user-2" aria-hidden="true"></i> Atendente</legend>
        </fieldset>
        <div class="col-xs-12">
            <div class="col-xs-6 col-lg-6  divFinalizadas checkbox" style="text-align:left;">

            </div>
            <div class="col-xs-6 col-lg-6" style="text-align:right;">
                <input type="text" id="txtSearch" class="input-search" alt="lista-atendente" placeholder="Pesquisar" />
            </div>
        </div>
        <div class="row">
            <div id="divAtendente">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table id="tblAtendente" class="lista-atendente table table-condensed table-striped">
                            <thead>
                                <tr>
                                    <th>Nome do Atendente</th>
                                    <th>CPF</th>
                                    <th>E-mail</th>
                                    <th>Celular</th>
                                    <th>Excluir?</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyAtendentes" class="tbodyAtendentes" data-bind="foreach: atendenteKO.atendenteList">
                                <tr data-bind="attr: { id: 'trAtendente' + atendenteId, 'data-Id': atendenteId }, event: { dblclick: showModalUPT, doubletap: showModalUPT }">
                                    <td data-bind="text: nome"></td>
                                    <td data-bind="text: cpf"></td>
                                    <td data-bind="text: email"></td>
                                    <td data-bind="text: celular"></td>
                                    <td><div data-bind="attr: { id: 'trAtendente' + atendenteId, 'data-Id': atendenteId }, event: { click: DeleteAtendente }" class="img-icone"></div></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

</div>
            <div class="row">
                <div class="col-md-12" style="margin:0; text-align:right;">
                    <button id="btnCadastrarNovoAtendente" type="button" class="shortcut primary buttonInside" required>
                        <i class="icon-plus-2" style="margin-right:5px;"></i>
                        <span>Novo Cadastro</span>
                    </button>

                </div>
            </div>
            <div class="modal fade" id="modalNovoCadastroAtendente" data-width="700px" data-dismiss="modal" role="dialog" data-keyboard="true" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                            <h4 class="modal-title" id="myModalLabel">Novo Cadastro de Atendente</h4>
                        </div>
                        <form name="form" id="formAtendente" data-toggle="validator">
                            <div class="modal-body">
                                <div class="container-fluid bgFFF">


                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Nome:</label>
                                                    <input id="txtNome_Atendente" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.nome, event: {keypress: enterSearch}" tabindex="1" required>

                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label>Data Nascimento:</label>
                                                    <input id="txtDataNasc_Atendente" type="text" class="DataNasc form-control input-sm" data-bind="textInput: atendenteKO.dataNasc, event: {keypress: enterSearch}" tabindex="2" required>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label id="lblCEP">CEP:</label>
                                                    <input id="cep" type="text" class="form-control input-sm" tabindex="3" data-bind="textInput: atendenteKO.cep, event: {keypress: enterSearch}" onblur="pesquisacep(this.value);">

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-5">
                                                    <label>Endereço:</label>
                                                    <input id="rua" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.endereco, event: {keypress: enterSearch}" tabindex="4" required>
                                                </div>

                                                <div class="form-group col-md-2">
                                                    <label>Número:</label>
                                                    <input id="txtNumero_Atendente" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.numero, event: {keypress: enterSearch}" tabindex="5">

                                                </div>
                                                <div class="form-group col-md-5">
                                                    <label>Complemento:</label>
                                                    <input id="txtComplemento_Atendente" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.complemento, event: {keypress: enterSearch}" tabindex="6">

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-5">
                                                    <label>Bairro:</label>
                                                    <input id="bairro" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.bairro, event: {keypress: enterSearch}" tabindex="7" required>

                                                </div>

                                                <div class="form-group col-md-2">
                                                    <label>Estado:</label>
                                                    <input id="uf" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.estado, event: {keypress: enterSearch}" tabindex="8" required>
                                                </div>
                                                <div class="form-group col-md-5">
                                                    <label>Cidade:</label>
                                                    <input id="cidade" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.cidade, event: {keypress: enterSearch}" tabindex="9" required>
                                                </div>




                                            </div>
                                        </div>



                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label id="lblCPF">CPF:</label>
                                                    <input id="txtCPF" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.cpf, event: {keypress: enterSearch}" tabindex="10">

                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label id="lblEmail">E-mail:</label>
                                                    <input id="txtEmail" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.email, event: {keypress: enterSearch}" tabindex="11" onblur="validacaoEmail(this)">

                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="form-group col-md-6">
                                                    <label>Telefone:</label>
                                                    <input id="txtTel" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.telefone, event: {keypress: enterSearch}" tabindex="12" maxlength="15">
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Celular:</label>
                                                    <input id="txtCel" type="text" class="form-control input-sm" data-bind="textInput: atendenteKO.celular, event: {keypress: enterSearch}" tabindex="13" maxlength="15">
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button id="btnSalvarNovoAtendente" type="button" class="shortcut primary buttonFooter" data-bind="enable: atendenteKO.validCadastro" tabindex="14" required>
                                    <i class="icon-floppy" style="margin-right:5px;"></i>
                                    <span>Salvar</span>
                                </button>
                                <button id="btnLimparAtendente" type="button" class="shortcut primary buttonFooter" required>
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
@Scripts.Render("~/bundles/atendente")
<script src="~/Scripts/bootstrap-contextmenu.js"></script>

<script type="text/javascript">
    function mascara(o, f) {
        v_obj = o
        v_fun = f
        setTimeout("execmascara()", 1)
    }
    function execmascara() {
        v_obj.value = v_fun(v_obj.value)
    }
    function mtel(v) {
        v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
        v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
        v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
        return v;
    }
    function id(el) {
        return document.getElementById(el);
    }
    window.onload = function () {
        id('txtTel').onkeyup = function () {
            mascara(this, mtel);
        }
        id('txtCel').onkeyup = function () {
            mascara(this, mtel);
        }
    }
</script>