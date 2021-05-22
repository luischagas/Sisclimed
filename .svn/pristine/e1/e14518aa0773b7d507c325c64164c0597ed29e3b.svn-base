Imports System.Net.Http

Public Module Modulo

    Public SisclimedConnectionString As String, ApplicationName As String = "Sisclimed"
    Public HMTL_ActiveUser As String = Get_HMTL_ActiveUser()
    Public HMTL_ActiveUser_OffLine As String
    Public SendEmailsTo As String, SendEmailsDebug As String, SendEmailHost As String, SendEmailPort As Integer, SendEmailUsername As String, SendEmailPassword As String, SendEmailSSL As Boolean, TipoEnvioSMS_Normal As String, TipoEnvioSMS_Prioritario As String
    Public site As String
    Sub New()
        Try
            If DEBUG_MODE() Then
                SisclimedConnectionString = ConfigurationManager.AppSettings("SisclimedConnectionString_Debug")
                SendEmailsTo = ConfigurationManager.AppSettings("SendEmailsTo")
                SendEmailsDebug = ConfigurationManager.AppSettings("SendEmailsDebug")
                SendEmailHost = ConfigurationManager.AppSettings("SendEmailHost")
                SendEmailPort = ConfigurationManager.AppSettings("SendEmailPort")
                SendEmailUsername = ConfigurationManager.AppSettings("SendEmailUsername")
                SendEmailPassword = ConfigurationManager.AppSettings("SendEmailPassword")
                SendEmailSSL = ConfigurationManager.AppSettings("SendEmailSSL")
                site = ConfigurationManager.AppSettings("Site")
            Else
                SisclimedConnectionString = ConfigurationManager.AppSettings("SisclimedConnectionString")
                SendEmailsTo = ConfigurationManager.AppSettings("SendEmailsTo")
                SendEmailsDebug = ConfigurationManager.AppSettings("SendEmailsDebug")
                SendEmailHost = ConfigurationManager.AppSettings("SendEmailHost")
                SendEmailPort = ConfigurationManager.AppSettings("SendEmailPort")
                SendEmailUsername = ConfigurationManager.AppSettings("SendEmailUsername")
                SendEmailPassword = ConfigurationManager.AppSettings("SendEmailPassword")
                SendEmailSSL = ConfigurationManager.AppSettings("SendEmailSSL")
                site = ConfigurationManager.AppSettings("Site")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function DEBUG_MODE() As Boolean
        Dim retorno As Boolean = False
        Try
            Dim debugMode As String = ConfigurationManager.AppSettings("DEBUG_MODE")
            retorno = (Not debugMode Is Nothing AndAlso debugMode.ToUpper = "TRUE")
        Catch ex As Exception
        End Try
        Return retorno
    End Function

    Public Function Get_ComboList(Tipo As String, Optional Especialidade As Integer = 0, Optional Paciente As Integer = 0) As comboValueList
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter), retorno As New comboValueList

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Tipo", SqlDbType.VarChar, Tipo))
            parametros.Add(DataAccess.CreateInParam("@Especialidade", SqlDbType.Int, Especialidade))
            parametros.Add(DataAccess.CreateInParam("@PacienteId", SqlDbType.Int, Paciente))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetComboList", parametros, Vals) AndAlso Not Vals Is Nothing Then
                For Each v In Vals
                    retorno.Add(New comboValue With {.text = v(0), .value = v(1)})
                Next
            End If

        Catch ex As Exception
        End Try

        Return retorno
    End Function

    Public Function Get_PacienteAgenda(Nome As String) As pacienteAgendaList
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter), retorno As New pacienteAgendaList

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Nome", SqlDbType.VarChar, Nome))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetPacienteAgenda", parametros, Vals) AndAlso Not Vals Is Nothing Then
                For Each v In Vals
                    retorno.Add(New pacienteAgenda With {.id = v(0), .nome = v(1), .dataNasc = v(2), .endereco = v(3), .numero = v(4), .complemento = v(5), .bairro = v(6), .estado = v(7), .cidade = v(8)})
                Next
            End If

        Catch ex As Exception
        End Try

        Return retorno
    End Function


    Public Function EnviarEmail(ByVal Assunto As String, ByVal Mensagem As String, ByVal Destinatario As String, ByVal HtmlBody As Boolean, Optional ByVal Bcc As String = Nothing, Optional ByVal MailAdicional As String = Nothing, Optional ByVal Anexo As ArrayList = Nothing) As Boolean
        Dim oEmail As Net.Mail.MailMessage = Nothing
        Try
            oEmail = New Net.Mail.MailMessage

            oEmail.From = New Net.Mail.MailAddress("sisclimed@gmail.com", Assunto)

            oEmail.To.Add(Destinatario)

            If Not MailAdicional Is Nothing Then
                Dim _MailAdicional As String() = Split(MailAdicional, ";")

                For i = 0 To _MailAdicional.Length - 1
                    oEmail.To.Add(_MailAdicional(i))
                Next
            End If

            If Not Bcc Is Nothing Then
                Dim _MailBcc As String() = Split(Bcc, ";")

                For i = 0 To _MailBcc.Length - 1
                    oEmail.Bcc.Add(_MailBcc(i))
                Next
            End If

            oEmail.Priority = Net.Mail.MailPriority.High

            oEmail.IsBodyHtml = HtmlBody

            If Not IsNothing(Anexo) Then
                For i = 0 To Anexo.Count - 1
                    oEmail.Attachments.Add(New Net.Mail.Attachment(Anexo(i).ToString))
                Next
            End If

            oEmail.Subject = Assunto
            oEmail.Body = Mensagem
            oEmail.SubjectEncoding = System.Text.Encoding.UTF8
            oEmail.BodyEncoding = System.Text.Encoding.UTF8

            Dim thd As New System.Threading.Thread(AddressOf SendEmailTHD)
            thd.Start(oEmail)

            Return True

        Catch ex As Exception
            oEmail.Dispose()
            Return False
        End Try

    End Function


    Private Sub SendEmailTHD(oEmail As Net.Mail.MailMessage)
        Try
            Dim oSmtp As Net.Mail.SmtpClient = New Net.Mail.SmtpClient(SendEmailHost, SendEmailPort)
            With oSmtp
                .EnableSsl = True
                .UseDefaultCredentials = True
                .Credentials = New Net.NetworkCredential(SendEmailUsername, SendEmailPassword)
                .Timeout = 100000000
                .Send(oEmail)
            End With
        Catch ex As Exception
            Dim erro As String = ex.Message
        End Try
    End Sub

    Public Function Get_HMTL_ActiveUser() As String
        Try
            Dim client As HttpClient = New HttpClient()
            Return client.GetStringAsync("http://localhost:14459/content/messages/email.html").Result
        Catch ex As Exception
            Return HMTL_ActiveUser_OffLine
        End Try
    End Function



    Public Function GetMessage_ActiveEmail(objUser As String, password As String) As String

        Dim mensagem As String = HMTL_ActiveUser

        mensagem = Replace(mensagem, "#USUARIO#", objUser)
        mensagem = Replace(mensagem, "#PWS#", password)
        mensagem = Replace(mensagem, "#LNK#", "http://localhost:14459/")

        Return mensagem

    End Function

End Module