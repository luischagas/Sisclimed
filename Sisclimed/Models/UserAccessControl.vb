Public Class UserAccessControl

    Public Shared Function Authentication(auth As String) As Integer
        Try
            Dim actionAuth As String = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Split("\"c)(0)))

            Dim ret As Integer

            If actionAuth <> "CSS" Then
                Dim SID As String = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Split("\"c)(1)))
                Dim DATA As String = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Split("\"c)(2)))

                'Dim cry As New CryptoRS(SID, True, ConectionStringSSMonitriip, Modulo.ApplicationName)

                'Dim tmp As String = cry.Decrypt(RSAData)

                'If cry.tokenStatus = 0 Then
                '    Return UserMembership.UserStatus.ExpiredSession
                'End If

                If actionAuth = "LGN" Then
                    Dim user As String = Encoding.UTF8.GetString(Convert.FromBase64String(DATA.Split("\"c)(0)))
                    Dim pass As String = DATA.Split("\"c)(1)

                    ret = IsValidNameAndPassword(user, pass, SID)

                    Return ret
                End If
            End If
        Catch ex As Exception
        End Try
        Return -2
    End Function

    Public Shared Function GetCookieSession(Optional Token As String = "") As String
        Dim retorno As String = ""
        Try
            If String.IsNullOrEmpty(Token) Then Token = GetCookieSID()
            If Not String.IsNullOrEmpty(Token) Then retorno = Token
        Catch ex As Exception
        End Try
        Return retorno
    End Function

    Private Shared Function GetCookieSID() As String
        Try
            Dim myToken As HttpCookie = HttpContext.Current.Request.Cookies("SID")
            If Not myToken Is Nothing Then Return Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(myToken.Value)))))
        Catch ex As Exception
        End Try
        Return String.Empty
    End Function

    Private Shared Function GetTokenSID() As String
        Try
            Dim CookieSID As String = GetCookieSID()
            If Not String.IsNullOrEmpty(CookieSID) Then Return CookieSID
        Catch ex As Exception
        End Try
        Return String.Empty
    End Function

    Public Shared Function ValidToken() As Integer
        Dim retorno As Integer = -1
        Try
            Dim TokenSID As String = GetTokenSID()

            If Not String.IsNullOrEmpty(TokenSID) Then
                retorno = GetOrSetToken(TokenSID)
            End If
        Catch ex As Exception
            retorno = -1
        End Try
        Return retorno
    End Function

    Private Shared Function IsValidNameAndPassword(ByVal username As String, password As String, token As String) As Integer
        Dim mbsUsuario As New UsuarioMembership(SisclimedConnectionString, ApplicationName)

        Try
            mbsUsuario.GetMembershipPassword(username, token)

            If Not String.IsNullOrEmpty(mbsUsuario.Password) Then

                If Not mbsUsuario.Ativo Then
                    Return UsuarioMembership.UserStatus.UserLocked
                Else
                    If mbsUsuario.Password = password Then
                        mbsUsuario.UpdateMembershipStatus(token, UsuarioMembership.UserStatus.ValidLogin)
                        Return UsuarioMembership.UserStatus.ValidLogin
                    Else
                        Return UsuarioMembership.UserStatus.InvalidPassword
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

        Return mbsUsuario.Status
    End Function

    Public Shared Function GetOrSetToken(Token As String) As String
        Dim Data As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter), returnValue As String = ""

        Try
            Data.setConection(SisclimedConnectionString, ApplicationName)

            If String.IsNullOrEmpty(Token) Then

                Dim output As IDbDataParameter = Data.CreateOutPutParam("@Retorno", SqlDbType.UniqueIdentifier, Parameters)

                If Not Data.execProcedure("usp_Session_CreateToken", Parameters) Then
                    returnValue = 3
                Else
                    returnValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(output.Value.ToString))
                End If
            Else
                Dim retorno As New List(Of Object)

                Data.CreateInParam("@Token", SqlDbType.VarChar, Token, Parameters)

                Dim output As IDbDataParameter = Data.CreateOutPutParam("@Status", SqlDbType.Int, Parameters)

                If Not Data.execReaderProcedure("usp_Session_GetToken", Parameters, retorno) Then
                    returnValue = 3
                Else
                    returnValue = Convert.ToInt16(output.Value)
                End If
            End If

        Catch ex As Exception

        Finally
            If Not Data Is Nothing Then
                Data.Dispose() : Data = Nothing
            End If
            Vals = Nothing
        End Try

        Return returnValue
    End Function

    Public Shared Function LogoutSession(Token As String) As Integer
        Dim mbsUsuario As New UsuarioMembership(SisclimedConnectionString, ApplicationName), retorno As Integer = -1

        Try
            retorno = mbsUsuario.LogoutMembershipUser(Token)
        Catch ex As Exception
        End Try

        Return retorno
    End Function

End Class
