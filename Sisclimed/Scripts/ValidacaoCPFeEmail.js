﻿$(function () {
    //Executa a requisição quando o campo username perder o foco
    $('#txtCPF').blur(function () {
        var cpf = $('#txtCPF').val().replace(/[^0-9]/g, '').toString();

        if (cpf.length == 11) {
            var v = [];

            //Calcula o primeiro dígito de verificação.
            v[0] = 1 * cpf[0] + 2 * cpf[1] + 3 * cpf[2];
            v[0] += 4 * cpf[3] + 5 * cpf[4] + 6 * cpf[5];
            v[0] += 7 * cpf[6] + 8 * cpf[7] + 9 * cpf[8];
            v[0] = v[0] % 11;
            v[0] = v[0] % 10;

            //Calcula o segundo dígito de verificação.
            v[1] = 1 * cpf[1] + 2 * cpf[2] + 3 * cpf[3];
            v[1] += 4 * cpf[4] + 5 * cpf[5] + 6 * cpf[6];
            v[1] += 7 * cpf[7] + 8 * cpf[8] + 9 * v[0];
            v[1] = v[1] % 11;
            v[1] = v[1] % 10;

            //Retorna Verdadeiro se os dígitos de verificação são os esperados.
            if ((v[0] != cpf[9]) || (v[1] != cpf[10])) {
                $('#lblCPF').css('color', 'red');
                $('#txtCPF').attr('placeholder', 'CPF Inválido');


                $('#txtCPF').val('');
            } else {
                $('#lblCPF').css('color', '');
                $('#txtCPF').attr('placeholder', 'CPF Inválido');
            }
        }
        else {
            $('#lblCPF').css('color', 'red');
            $('#txtCPF').attr('placeholder', 'CPF Inválido');
            $('#txtCPF').val('');
        }
    });
});

function remove(str, sub) {
    i = str.indexOf(sub);
    r = "";
    if (i == -1) return str;
    {
        r += str.substring(0, i) + remove(str.substring(i + sub.length), sub);
    }

    return r;
}

function validacaoEmail(field) {
    usuario = field.value.substring(0, field.value.indexOf("@"));
    dominio = field.value.substring(field.value.indexOf("@")+ 1, field.value.length);

    if ((usuario.length >=1) &&
        (dominio.length >=3) && 
        (usuario.search("@")==-1) && 
        (dominio.search("@")==-1) &&
        (usuario.search(" ")==-1) && 
        (dominio.search(" ")==-1) &&
        (dominio.search(".")!=-1) &&      
        (dominio.indexOf(".") >=1)&& 
        (dominio.lastIndexOf(".") < dominio.length - 1)) {
        $('#lblEmail').css('color', '');
    }
    else {
        $('#lblEmail').css('color', 'red');
        $('#txtEmail').attr('placeholder', 'E-mail Inválido');
        $('#txtEmail').val('');
       
    }

   
}