Imports System.Runtime.Serialization

<DataContract()>
Public Class agendamento

    <DataMember()>
    Public Property data As Date

    <DataMember()>
    Public Property hora As String

    <DataMember()>
    Public Property pacienteId As Integer

    <DataMember()>
    Public Property nomePaciente As String

    <DataMember()>
    Public Property especialidade As String



    <DataMember()>
    Public Property medicoId As Integer

    <DataMember()>
    Public Property nomeMedico As String

    <DataMember()>
    Public Property obs As String

    <DataMember()>
    Public Property dataNasc As Date

    <DataMember()>
    Public Property endereco As String

    <DataMember()>
    Public Property numero As String

    <DataMember()>
    Public Property complemento As String

    <DataMember()>
    Public Property bairro As String

    <DataMember()>
    Public Property estado As String

    <DataMember()>
    Public Property cidade As String

    <DataMember()>
    Public Property agendamentoId As Integer

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, obs As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String)
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidade = especialidade
        Me.obs = obs
        Me.dataNasc = dataNasc
        Me.endereco = endereco
        Me.numero = numero
        Me.complemento = complemento
        Me.bairro = bairro
        Me.estado = estado
        Me.cidade = cidade
    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, obs As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String, agendamentoId As Integer)
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidade = especialidade
        Me.obs = obs
        Me.dataNasc = dataNasc
        Me.endereco = endereco
        Me.numero = numero
        Me.complemento = complemento
        Me.bairro = bairro
        Me.estado = estado
        Me.cidade = cidade
        Me.agendamentoId = agendamentoId
    End Sub

    Public Function SaveInfo() As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.data) OrElse String.IsNullOrEmpty(Me.hora) OrElse String.IsNullOrEmpty(Me.pacienteId) OrElse String.IsNullOrEmpty(Me.medicoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Data", SqlDbType.Date, Me.data))
            Parameters.Add(DataAccess.CreateInParam("@Hora", SqlDbType.VarChar, Me.hora))
            Parameters.Add(DataAccess.CreateInParam("@PacienteId", SqlDbType.Int, Me.pacienteId))
            Parameters.Add(DataAccess.CreateInParam("@MedicoId", SqlDbType.Int, Me.medicoId))
            Parameters.Add(DataAccess.CreateInParam("@Especialidade", SqlDbType.VarChar, Me.especialidade))
            Parameters.Add(DataAccess.CreateInParam("@Observacao", SqlDbType.VarChar, Me.obs))
            Parameters.Add(DataAccess.CreateInParam("@Agendamento_Id", SqlDbType.Int, Me.agendamentoId))

            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_SetAgendamento", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                If Not parRetorno Is Nothing And Not parRetorno.Value Is Nothing Then
                    retorno = parRetorno.Value
                    If retorno.ToUpper = "OK" Then status = True
                End If
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then DataAccess.Dispose() : DataAccess = Nothing
            Vals = Nothing
        End Try

        Return retorno
    End Function

    Public Function DeleteInfo(ExameID As Integer) As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.agendamentoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Agendamento_Id", SqlDbType.Int, ExameID))


            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_DeleteAgendamento", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                If Not parRetorno Is Nothing And Not parRetorno.Value Is Nothing Then
                    retorno = parRetorno.Value
                    If retorno.ToUpper = "OK" Then status = True
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

Public Class agendamentoList
    Inherits List(Of agendamento)

    Private _Loaded As Boolean
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Sub New()
    End Sub

    Public Sub New(Load As Boolean, Optional ExameID As Integer = 0)
        If Load Then LoadInfo(ExameID)
    End Sub

    Public Sub New(Protocolo As String)
        Try
            Dim protocoloSplit As String() = Protocolo.Split("#")

            For Each ps In protocoloSplit
                Dim info As String() = ps.Split("|")
                If info.Count = 3 Then
                    Me.Add(New agendamento(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11), info(12), info(13), info(14), info(15)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(Optional AgendamentoID As Integer = 0, Optional UsuarioID As Integer = 0, Optional Tipo As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Agendamento_Id", SqlDbType.Int, AgendamentoID))

            parametros.Add(DataAccess.CreateInParam("@Usuario_Id", SqlDbType.Int, UsuarioID))

            parametros.Add(DataAccess.CreateInParam("@Tipo", SqlDbType.Int, Tipo))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetAgendamento", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New agendamento With {.agendamentoId = v(0), .data = v(1), .hora = v(2), .pacienteId = v(3), .nomePaciente = v(4), .medicoId = v(5), .nomeMedico = v(6), .especialidade = v(7), .obs = v(8), .dataNasc = v(9), .endereco = v(10), .numero = v(11), .complemento = v(12), .bairro = v(13), .estado = v(14), .cidade = v(15)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class