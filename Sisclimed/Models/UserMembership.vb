﻿Public Class UsuarioMembership
    Public Property Password As String
    Public Property Ativo As Boolean
    Public Property Status As Integer
    Public Property ApplicationName As String
    Public Property ConnectionString As String

    Public Enum UserStatus
        None
        InvalidUser
        ValidLogin
        InvalidPassword
        UserLogged
        UserLocked
        ExpiredSession
        ChangePassword
        UserNotActived
        Unauthorized
        ServerOffine
    End Enum

    Public Sub New(ConnectionString As String, ApplicationName As String)
        Me.ConnectionString = ConnectionString
        Me.ApplicationName = ApplicationName
    End Sub

    Public Sub GetMembershipPassword(ByVal username As String, token As String)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(Me.ConnectionString, ApplicationName)

            DataAccess.CreateInParam("@Username", SqlDbType.VarChar, username, Parameters)
            DataAccess.CreateInParam("@Token", SqlDbType.VarChar, token, Parameters)

            Dim output As IDbDataParameter = DataAccess.CreateOutPutParam("@Return", SqlDbType.Int, Parameters)

            If DataAccess.execReaderProcedure("usp_Membership_Usuario_GetPassword", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                For Each v In Vals
                    Me.Password = v(0)
                    Me.Ativo = v(1)
                    Me.Status = output.Value
                Next
            Else
                Me.Status = UserStatus.ServerOffine
            End If

            If String.IsNullOrEmpty(Me.Password) Then Me.Status = UserStatus.Unauthorized
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then
                DataAccess.Dispose() : DataAccess = Nothing
            End If
            Vals = Nothing
        End Try

    End Sub

    Public Sub UpdateMembershipStatus(token As String, Status As UserStatus)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter), retorno As New List(Of Object)

        Try
            DataAccess.setConection(Me.ConnectionString, ApplicationName)

            DataAccess.CreateInParam("@Token", SqlDbType.VarChar, token, Parameters)
            DataAccess.CreateInParam("@Status", SqlDbType.Int, Status, Parameters)

            DataAccess.execProcedure("Membership_Usuario_UpdateStatus", Parameters)

        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then
                DataAccess.Dispose() : DataAccess = Nothing
            End If
            Vals = Nothing
        End Try
    End Sub

    Public Function LogoutMembershipUser(token As String) As Integer
        Dim Data As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)

        Try
            Data.setConection(Me.ConnectionString, ApplicationName)

            If Not String.IsNullOrEmpty(token) Then
                Dim retorno As New List(Of Object)

                Data.CreateInParam("@Token", SqlDbType.VarChar, Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(token))), Parameters)

                Dim output As IDbDataParameter = Data.CreateOutPutParam("@Return", SqlDbType.Int, Parameters)

                If Data.execProcedure("usp_Session_DeleteToken", Parameters) Then
                    Return output.Value
                End If
            End If
        Catch ex As Exception

        End Try

        Return -1
    End Function


End Class
