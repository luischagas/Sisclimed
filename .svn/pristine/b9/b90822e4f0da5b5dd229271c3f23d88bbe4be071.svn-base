Imports System.Web
Imports System.Web.Http

Public Class ActiveTokenStatus
    Public Property Validated As String
    Public Property Token As String
    Public Property Loaded As Boolean

    Public Sub New(Headers As RouteValueDictionary)
        If Not Headers Is Nothing Then
            Me.Validated = Headers("Validated")
            Me.Token = Headers("ValidToken")
            If Me.Validated = "OK" Then
                Me.Loaded = True
            End If
        End If
    End Sub
End Class