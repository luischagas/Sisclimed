Public Class Usuario

    Public Property UsuarioId As Integer

    Public Property Login As String

    Public Property Nome As String

    Public Property Tipo As String

    Public Property password As String

    Public Property Newpassword As String

    Public Property NumeroAcesso As Integer
    Public Property Loaded As Boolean

    Public Sub New()
    End Sub

    Public Sub New(Token As String)
        Me.LoadInfo(Token)
    End Sub

    Public Sub LoadInfo(Token As String)

        If String.IsNullOrEmpty(Token) Then Exit Sub

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            DataAccess.CreateInParam("@Token", SqlDbType.VarChar, Token, Parameters)

            If DataAccess.execReaderProcedure("usp_Membership_Usuario_GetInfo", Parameters, Vals) AndAlso Not Vals Is Nothing AndAlso Vals.Count > 0 Then
                For Each v In Vals
                    Me.UsuarioId = v(0)
                    Me.Login = v(1)
                    Me.Nome = v(2)
                    Me.Tipo = v(3)
                    Me.NumeroAcesso = v(4)
                Next
                Me.Loaded = True
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then
                DataAccess.Dispose() : DataAccess = Nothing
            End If
            Vals = Nothing
        End Try
    End Sub


    Public Function TrocaSenha(UsuarioId As Integer, password As String, Newpassword As String) As String
        Dim retorno As String = ""



        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@UsuarioId", SqlDbType.Int, UsuarioId))
            Parameters.Add(DataAccess.CreateInParam("@Password", SqlDbType.VarChar, password))
            Parameters.Add(DataAccess.CreateInParam("@NewPassword", SqlDbType.VarChar, Newpassword))


            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("ChangePassword", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                If Not parRetorno Is Nothing And Not parRetorno.Value Is Nothing Then
                    retorno = parRetorno.Value
                    If retorno.ToUpper = "OK" Then


                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then DataAccess.Dispose() : DataAccess = Nothing
            Vals = Nothing
        End Try

        Return retorno
    End Function

End Class
