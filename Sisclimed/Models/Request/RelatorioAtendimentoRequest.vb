<DataContract()>
Public Class relatorioAtendimentoRequest

    <DataMember()>
    Public Property AgendamentoId As Integer

    <DataMember()>
    Public Property relatorio As New relatorioAtendimento

    Public Sub New()

    End Sub

End Class
