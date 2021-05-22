Imports System.Runtime.Serialization

<DataContract()>
Public Class usuarioInfo

    <DataMember()>
    Public Property UsuarioId As Integer

    <DataMember()>
    Public Property Login As String

    <DataMember()>
    Public Property Nome As String

    <DataMember()>
    Public Property Tipo As String

    <DataMember()>
    Public Property NumeroAcesso As Integer


    Public Sub New()
    End Sub

    Public Sub SetUserData(Usuario As Usuario)
        Me.UsuarioId = Usuario.UsuarioId
        Me.Login = Usuario.Login
        Me.Nome = Usuario.Nome
        Me.Tipo = Usuario.Tipo
        Me.NumeroAcesso = Usuario.NumeroAcesso
    End Sub
End Class