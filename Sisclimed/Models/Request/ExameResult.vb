<DataContract()>
Public Class exameResult

    <DataMember()>
    Public Property value As New exameList

    <DataMember()>
    Public Property message As String

    <DataMember()>
    Public Property code As Integer

    Public Sub New()

    End Sub

End Class
