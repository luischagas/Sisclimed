<DataContract()>
Public Class historicoRequest

    <DataMember()>
    Public Property historicoId As Integer

    <DataMember()>
    Public Property historico As New historico

    Public Sub New()

    End Sub

End Class
