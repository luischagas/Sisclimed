<DataContract()>
Public Class jornadaResult

    <DataMember()>
    Public Property value As New JornadaList

    <DataMember()>
    Public Property message As String

    <DataMember()>
    Public Property code As Integer

    Public Sub New()

    End Sub

End Class
