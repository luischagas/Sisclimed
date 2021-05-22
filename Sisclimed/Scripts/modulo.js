showAlertError = false;

$(function () {

    var logged = getAuthorization();

    if (!logged) {
        goToIndex();
    }

    if (!window.JSON) {
        window.JSON = {
            parse: function (sJSON) { return eval('(' + sJSON + ')'); },
            stringify: (function () {
                var toString = Object.prototype.toString;
                var isArray = Array.isArray || function (a) { return toString.call(a) === '[object Array]'; };
                var escMap = { '"': '\\"', '\\': '\\\\', '\b': '\\b', '\f': '\\f', '\n': '\\n', '\r': '\\r', '\t': '\\t' };
                var escFunc = function (m) { return escMap[m] || '\\u' + (m.charCodeAt(0) + 0x10000).toString(16).substr(1); };
                var escRE = /[\\"\u0000-\u001F\u2028\u2029]/g;
                return function stringify(value) {
                    if (value == null) {
                        return 'null';
                    } else if (typeof value === 'number') {
                        return isFinite(value) ? value.toString() : 'null';
                    } else if (typeof value === 'boolean') {
                        return value.toString();
                    } else if (typeof value === 'object') {
                        if (typeof value.toJSON === 'function') {
                            return stringify(value.toJSON());
                        } else if (isArray(value)) {
                            var res = '[';
                            for (var i = 0; i < value.length; i++)
                                res += (i ? ', ' : '') + stringify(value[i]);
                            return res + ']';
                        } else if (toString.call(value) === '[object Object]') {
                            var tmp = [];
                            for (var k in value) {
                                if (value.hasOwnProperty(k))
                                    tmp.push(stringify(k) + ': ' + stringify(value[k]));
                            }
                            return '{' + tmp.join(', ') + '}';
                        }
                    }
                    return '"' + value.toString().replace(escRE, escFunc) + '"';
                };
            })()
        };
    }

    ko.bindingHandlers.stopBinding = {
        init: function () {
            return { controlsDescendantBindings: true };
        }
    };

    ko.virtualElements.allowedBindings.stopBinding = true;

    jQuery("#cep").mask("99999-999");

});


function InitSeletctonic(ids, select, unselectAll, keyDown) {
    $(ids).each(function () {
        $("#" + this + " tbody").selectonic({
            multi: false,
            mouseMode: "standard",
            keyboard: true,
            autoScroll: '#' + this + ' tbody',
            focusBlur: true,
            selectBlur: true,
            select: select,
            unselectAll: unselectAll,
            keyDown: keyDown
        });
    });
};

function Loader(state) {
    $('.divLoader').each(function () {
        if (state == 'show') { $(this).show(); } else if (state == 'hide') { $(this).hide(); }
    })
};


var alertErrorShowing = false;

function AlertServerError(msg) {

    if (!alertErrorShowing) {
        alertErrorShowing = true;

        if (!msg) {
            msg = 'Erro de acesso ao servidor!'
        }

        alertify.alert(msg, AlertServerErrorClosed);
    }

    Loader('hide');
};

function AlertServerErrorClosed() {
    alertErrorShowing = false;
}


function soNums(e) {

    keyCodesPermitidos = new Array(8, 9, 37, 39, 46);

    for (x = 48; x <= 57; x++) {
        keyCodesPermitidos.push(x);
    }

    for (x = 96; x <= 105; x++) {
        keyCodesPermitidos.push(x);
    }
    keyCode = e.which;

    if ($.inArray(keyCode, keyCodesPermitidos) != -1) {
        return true;
    }
    return false;
}

	


function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('rua').value = ("");
    document.getElementById('bairro').value = ("");
    document.getElementById('cidade').value = ("");
    document.getElementById('uf').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('rua').value = (conteudo.logradouro);
        document.getElementById('bairro').value = (conteudo.bairro);
        document.getElementById('cidade').value = (conteudo.localidade);
        document.getElementById('uf').value = (conteudo.uf);
    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulário_cep();
        $('#cep').val('');
        $('#cep').attr('placeholder', 'CEP Inválido');
        
    }
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('rua').value = "...";
            document.getElementById('bairro').value = "...";
            document.getElementById('cidade').value = "...";
            document.getElementById('uf').value = "...";

            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = '//viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);

        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
            $('#cep').val('');
            $('#cep').attr('placeholder', 'CEP Inválido');
            
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};


function goToIndex() {
    window.location = "/";
};