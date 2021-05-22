Public Class HomeController
    Inherits System.Web.Mvc.Controller

    <AllowAnonymous()>
    Function Index() As ActionResult
        ViewData("Message") = "Página principal do sistema."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarMedico() As ActionResult
        ViewData("Message") = "Página de cadastro de médicos."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarAtendente() As ActionResult
        ViewData("Message") = "Página de cadastro de atendentes."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarPaciente() As ActionResult
        ViewData("Message") = "Página de cadastro de pacientes."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarExame() As ActionResult
        ViewData("Message") = "Página de cadastro de exames."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarConvenio() As ActionResult
        ViewData("Message") = "Página de cadastro de convênios."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarHistorico() As ActionResult
        ViewData("Message") = "Página de cadastro de históricos."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function CadastrarAgendamento() As ActionResult
        ViewData("Message") = "Página de cadastro de consultas."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function Atendimento() As ActionResult
        ViewData("Atendimento") = "Atendimento."

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function RelatorioAgendamento() As ActionResult
        ViewData("RelatorioAgendamento") = "Relatorio de agendamentos"

        Return View()
    End Function

    <ActionAuthorizationAttribute()>
    Function RelatorioAtendimento() As ActionResult
        ViewData("RelatorioAtendimento") = "Relatorio de atendimentos"

        Return View()
    End Function

End Class
