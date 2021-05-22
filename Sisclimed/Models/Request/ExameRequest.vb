<DataContract()>
Public Class exameRequest

    <DataMember()>
    Public Property exameId As Integer

    <DataMember()>
    Public Property exame As New exame

    Public Sub New()

    End Sub

End Class
