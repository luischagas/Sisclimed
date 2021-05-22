<DataContract()>
Public Class relatorioAtendimentoResult

    <DataMember()>
    Public Property value As New relatorioAtendimentoList

    <DataMember()>
    Public Property message As String

    <DataMember()>
    Public Property code As Integer

    Public Sub New()

    End Sub

End Class
