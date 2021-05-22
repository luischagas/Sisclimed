@Code
    Layout = Nothing
End Code
   



<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />

    <title>Sisclimed - Sistema de Clínicas Médicas</title>

    @*@Styles.Render("~/Content/css")*@
    @Scripts.Render("~/bundles/modernizr")
    @*@Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/basejs")*@

    <link href="~/Content/login/fonts.css" rel='stylesheet' type='text/css'>
    <link href="~/Content/login/normalize.min.css" rel="stylesheet">
    <link href="~/Content/login/font-awesome.min.css" rel="stylesheet">
    <link href="~/Content/login/style.css" rel="stylesheet">

    <script language="JavaScript" type="text/javascript" src="~/Scripts/prefixfree.min.js"></script>
</head>


<body>

    <div class="container-fluid">
        <div class="alinhamentotop row" style="margin-top:200px">
            <div class="col-md-12">

                <div  class="col-sm-6">
                    <div class="alinhamento imgLogo" style="text-align:right; margin-top:30px; margin-right:30px;">
                        <img src="~/Content/images/logo3.png" width="370" height="260">
                    </div>
                </div>
                
                <div class="col-sm-6">
                  
                    <form class="alinhamento formAlinhamento login" style="margin-left: 30px;">
                        <p class="title">Área de Acesso</p>

                        <input type="text" placeholder="Usuário" data-bind="textInput: loginKO.username, event: {keypress: enterSearch}" autofocus />

                  

                        <input type="password" placeholder="Senha" data-bind="textInput: loginKO.password, event: {keypress: enterSearch}" />

                       

                        <a href="#">Esqueceu a senha?</a>

                        <button id="btnEntrar" type="button" data-bind="enable: loginKO.validLogin">
                            
                            <span class="state">Entrar</span>
                        </button>

                        <div class="row" style="margin-top:10px; text-align:center;">
                            <span id="spnMesssage" title="Mensagem" data-bind="text: loginKO.message" style="color:red;"></span>
                        </div>

                        <div id="divLoaderLogin" style="display:none; top:230px;text-align:center">
                            <img src="~/Content/images/login-ajax-loader.gif" />
                        </div>

                    </form>
                       
                </div>
            </div>
        </div>
    </div>
        <script src='~/Scripts/jquery-3.1.1.js'></script>
        <script src="~/Scripts/knockout-3.4.0.js"></script>
        <script src="~/Scripts/module/encode64.js"></script>
        <script src="~/Scripts/module/storage.js"></script>
        <script src="~/Scripts/module/login.js"></script>
        <script src="~/Scripts/login/index.js"></script>
        <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.css">
        <link rel="stylesheet" type="text/css" href="~/Content/bootstrap-theme.css">
        @Scripts.Render("~/bundles/bootstrap")

    
</body>






</html>

@*<body>

        <div class="wrapper">
            <form class="login">
                <p class="title">Área de Acesso</p>

                 <input type="text" placeholder="Usuário" data-bind="textInput: loginKO.username, event: {keypress: enterSearch}" autofocus />

                 <i class="fa fa-user"></i>

                <input type="password" placeholder="Senha" data-bind="textInput: loginKO.password, event: {keypress: enterSearch}"/>

                 <i class="fa fa-key"></i>

                 <a href="#">Esqueceu a senha?</a>

                  <button id="btnEntrar" type="button" data-bind="enable: loginKO.validLogin"  >
                    <i class="spinner"></i>
                    <span class="state">Entrar</span>
                </button>

                <div Class="row" style="margin-top:10px;">
                    <span id="spnMesssage" title="Mensagem" data-bind="text: loginKO.message" style="color:red;"></span>
                </div>

                <div id="divLoaderLogin" style="position:absolute; display:none; top:230px; right:0px;">
                    <img src="~/Content/images/login-ajax-loader.gif" />
                </div>

            </form>


        </div>
        <script src='~/Scripts/jquery-3.1.1.js'></script>
        <script src="~/Scripts/knockout-3.4.0.js"></script>
        <script src="~/Scripts/module/encode64.js"></script>
        <script src="~/Scripts/module/storage.js"></script>
        <script src="~/Scripts/module/login.js"></script>
        <script src="~/Scripts/login/index.js"></script>
    </body>*@
