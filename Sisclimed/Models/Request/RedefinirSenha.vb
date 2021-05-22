Imports System.Runtime.Serialization

<DataContract()>
Public Class RedefinirSenha

    <DataMember()>
    Public Property code As String

    <DataMember()>
    Public Property password As String


    <DataMember()>
    Public Property Newpassword As String

    Public Sub New()
    End Sub
End Class