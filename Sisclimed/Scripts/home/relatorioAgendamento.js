var estado;

var total;

ko.extenders.ValidaCadastro = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var current = target()

            if (newValue !== current) {
                //msgLogin("");
                target(newValue);
                target.notifySubscribers(newValue);

                if (relatorioKO) {
                    relatorioKO.validCadastro(validarCampos());
                }
            }
        }
    }).extend({ notify: 'always' });;

    result(target());

    return result;
};


var relatorioKO = new relatorioData();

$(function () {
    RelatorioStart();
});


function RelatorioStart() {

    var datako = $("[data-bind*='relatorioKO']");

    for (ct = 0; ct < datako.length; ct++) {
        var datakoelement = datako[ct];
        ko.applyBindings(relatorioKO, datakoelement);
    }

    Loader('show');

    $('#btnPesquisar').on('click', function () {

        if (relatorioKO.dataInicial() == '' || relatorioKO.dataFinal() == '')
        {
            AlertServerError("Informe uma data inicial e uma data final para a filtragem!");

        } else {


            GetListRelatorioAgenda();
        }

    })

    var doc = new jsPDF();
    var specialElementHandlers = {
        '#tblRelatorio': function (element, renderer) {
            return true;
        }
    };

    $('#btnExportar').on('click', function () {
        alertify.confirm('Confirma a exportação do relatório para PDF?', function (e) {
            if (e) {
                generatePDF();
            }
    });
    });


        GetCombo('ESPECIALIDADE', $('.slcEspecialidade'));
        GetCombo('MEDICOAGENDA', $('.slcMedico'));
        GetCombo('PACIENTE', $('.slcPaciente'));


        InitSeletctonic(['tblRelatorio'],
           function (event, ui) { //Select Event
              
                   setcurrentDisplayRelatorio($(ui.items).data("id"), $(ui.items).attr("id"));
           },

           function (event, ui) { // Unselect Event

           },

           function (event, ui) { //KeyDown Event

             
           });



 
            $('#txtDataInicial').focus();
 


        var dateToday = new Date();
        $(".dataInicial").datepicker({
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior',
            currentText: 'Agora',
            closeText: 'Fechar',
            changeMonth: true,
            changeYear: true,
            yearRange: '1945:' + (new Date).getFullYear(),
            showButtonPanel: true,

        });

        $(".dataFinal").datepicker({
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior',
            currentText: 'Agora',
            closeText: 'Fechar',
            changeMonth: true,
            changeYear: true,
            yearRange: '1945:' + (new Date).getFullYear(),
            showButtonPanel: true,

        });

        $("#txtDataInicial").mask("99/99/9999");
        $("#txtDataFinal").mask("99/99/9999");


}

function generatePDF() {
    var pdfsize = 'a4';
    var pdf = new jsPDF('l', 'pt', pdfsize);

    var res = pdf.autoTableHtmlToJson(document.getElementById("tblRelatorio"));

    

    var header = function(data) {
        pdf.setFontSize(10);
        pdf.setTextColor(100);
        pdf.setFontStyle('normal');
        pdf.text(675, 50, "Total de Agendamentos: " + total.toString());
        pdf.setFontSize(18);
        pdf.text("Relatório de Agendamento", data.settings.margin.left, 50);
    }

    pdf.autoTable(res.columns, res.data, {

        beforePageContent: header,
        startY: 60,
        drawHeaderRow: function(row, data) {
            row.height = 46;
        },
        drawHeaderCell: function (cell, data) {

            pdf.rect(cell.x, cell.y, cell.width, cell.height, cell.styles.fillStyle);

            pdf.setTextColor(100);
            pdf.rect(cell.x, cell.y + (cell.height / 2), cell.width, cell.height / 2, cell.styles.fillStyle);
            pdf.autoTableText(cell.text, cell.textPos.x, cell.textPos.y, {
                halign: cell.styles.halign,
                valign: cell.styles.valign
            });
            pdf.setTextColor(100);
            pdf.setFontStyle('normal');
            var text = data.table.rows[0].cells[data.column.dataKey].text;
            pdf.autoTableText(text, cell.textPos.x, cell.textPos.y + (cell.height / 2), {
                halign: cell.styles.halign,
                valign: cell.styles.valign
            });

            return false;
        },
        drawRow: function(row, data) {
            if (row.index === 0) return false;
        },
        margin: {
            top: 60
        },
        styles: {
            overflow: 'linebreak',
            fontSize: 10,
            tableWidth: 'auto',
            columnWidth: 'auto',
        },
        columnStyles: {
            1: {
                columnWidth: 'auto'
            }
        },


    });


    pdf.save(pdfsize + ".pdf");
};

function tableToJson(table) {
    var data = [];

    // first row needs to be headers
    var headers = [];
    for (var i=0; i<table.rows[0].cells.length; i++) {
        headers[i] = table.rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi,'');
    }


    // go through cells
    for (var i=0; i<table.rows.length; i++) {

        var tableRow = table.rows[i];
        var rowData = {};

        for (var j=0; j<tableRow.cells.length; j++) {

            rowData[ headers[j] ] = tableRow.cells[j].innerHTML;

        }

        data.push(rowData);
    }       

    return data;
}

var enterSearch = function (d, e) {
    if (e.keyCode == 13 && validarCampos()) {
        $('#btnSalvarNovoHistorico').click();
        $('#btnSalvarNovoHistorico').removeAttr("disabled");
    }
    return true;
};


function validarCampos() {

    return relatorioKO.relatorioList().length > 0
};

function relatorioData() {
    this.dataInicial = ko.observable("");
    this.dataFinal = ko.observable("");
    this.data = ko.observable("");
    this.hora = ko.observable("");
    this.pacienteId = ko.observable("");
    this.nomePaciente = ko.observable("");
    this.cpf = ko.observable("");
    this.medicoId = ko.observable("");
    this.nomeMedico = ko.observable("");
    this.especialidade = ko.observable("");
    this.especialidadeId = ko.observable("");
    this.agendamentoId = ko.observable("");
    this.relatorioList = ko.observableArray([]).extend({ ValidaCadastro: null });
    this.validCadastro = ko.observable(false);
    this.currentDisplayRelatorio = ko.observable(null);
    this.currentDisplayRelatorioIdentity = ko.observable(null);

};


function setcurrentDisplayRelatorio(id, name) {
    for (ct = 0; ct < relatorioKO.relatorioList().length; ct++) {
        var relatorio = relatorioKO.relatorioList()[ct];
        if (relatorio.agendamentoId == id) {
            relatorioKO.currentDisplayRelatorio(relatorio);
            relatorioKO.currentDisplayRelatorioIdentity(name)
        }
    }
};

function relatorio() {
    this.data = ""
    this.hora = ""
    this.pacienteId = ""
    this.nomePaciente = ""
    this.cpf = ""
    this.medicoId = ""
    this.nomeMedico = ""
    this.especialidade = ""
    this.especialidadeId = ""
    this.agendamentoId = ""

}

function relatorioRequest() {
    this.relatorio = new relatorio();
};

function GetListRelatorioAgenda() {

    var dataValue = new relatorioRequest();

    var partsInicial = relatorioKO.dataInicial().split("/");
    var dtInicial = new Date(parseInt(partsInicial[2], 10),
                      parseInt(partsInicial[1], 10) - 1,
                      parseInt(partsInicial[0], 10));

    var partsFinal = relatorioKO.dataFinal().split("/");
    var dtFinal = new Date(parseInt(partsFinal[2], 10),
                      parseInt(partsFinal[1], 10) - 1,
                      parseInt(partsFinal[0], 10));



    dataValue.relatorio.dataInicial = dtInicial;
    dataValue.relatorio.dataFinal = dtFinal;
    dataValue.relatorio.medicoId = relatorioKO.medicoId();
    dataValue.relatorio.especialidadeId = relatorioKO.especialidadeId();
    dataValue.relatorio.pacienteId = relatorioKO.pacienteId();

   
            $.ajax({
                type: 'POST',
                url: '../api/values/GetListRelatorioAgenda',
                data: JSON.stringify(dataValue),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                xhrFields: {
                    withCredentials: true
                },
                beforeSend: function (xhr, opts) {
                    xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                    setTimeout(GetListRelatorioAgenda, 15000);
                },
                success: function (data, ajaxOptions, response) {

                    if (data) {
                        if (data.value) {
                            if (data.value.length > 0) {
                                relatorioKO.relatorioList(data.value);

                                total = data.value.length;
                                $('#divCont').html('Total de Agendamentos: ' + data.value.length);


                                if (relatorioKO.currentDisplayRelatorioIdentity()) {
                                    SetSelectonicFocus('#tblRelatorio', relatorioKO.currentDisplayRelatorioIdentity());
                                }
                            } else {
                                relatorioKO.relatorioList(data.value); Loader('hide');
                                $('#divCont').html('Total de Agendamentos: ' + data.value.length);
                            }
                        } else { Loader('hide'); }
                    }
                    Loader('hide');

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    AlertServerError();
                }
            });
        }

function SetSelectonicFocus(table, selected) {

    var tbl = $(table + ' tbody');

    if (selected) {
        tbl.selectonic('select', $('#' + selected));
        tbl.selectonic('focus', $('#' + selected));
    }

};




function GetCombo(Tipo, nomeCombo, especialidade, paciente) {

    var dataValue = new relatorioRequest();

    dataValue.relatorio.tipo = Tipo;

    dataValue.relatorio.especialidade = especialidade;

    dataValue.relatorio.pacienteId = paciente;


    $.ajax({
        type: 'POST',
        url: '../api/values/GetCombo',
        data: JSON.stringify(dataValue.relatorio),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        xhrFields: {
            withCredentials: true
        },
        beforeSend: function (xhr, opts) {
            xhr.setRequestHeader("Authorization", "Basic " + base64_encode('CSS'));
        },
        success: function (data, ajaxOptions, response) {

            if (data) {
                if (data.value) {
                    if (data.value.length > 0) {
                        opt = '';

                        opt += '<option value="">Selecione</option>';

                        $(data.value).each(function (index) {
                            opt += '<option value="' + this.value + '"> ' + this.text + ' </option>';
                        });

                        nomeCombo.each(function () {
                            $(this).empty().append(opt);
                        })

                    } else {
                        opt = '';

                        opt += '<option value="">Selecione</option>';

                        nomeCombo.each(function () {
                            $(this).empty().append(opt);
                        })
                    }
                } else {
                    Loader('hide');
                }
            }
            Loader('hide');

        },
        error: function (xhr, ajaxOptions, thrownError) {
            AlertServerError();
        }
    });
}

function clearFields() {

    relatorioKO.data("");
    relatorioKO.hora("");
    relatorioKO.pacienteId("");
   



}
