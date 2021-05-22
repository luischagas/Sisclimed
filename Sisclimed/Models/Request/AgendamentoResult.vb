<DataContract()>
Public Class agendamentoResult

    <DataMember()>
    Public Property value As New agendamentoList

    <DataMember()>
    Public Property message As String

    <DataMember()>
    Public Property code As Integer

    Public Sub New()

    End Sub

End Class
