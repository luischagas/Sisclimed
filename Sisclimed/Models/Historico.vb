Imports System.Runtime.Serialization

<DataContract()>
Public Class historico

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
    Public Property dataNasc As Date

    <DataMember()>
    Public Property cpf As String

    <DataMember()>
    Public Property sintomas As String

    <DataMember()>
    Public Property evolucao As String

    <DataMember()>
    Public Property receita As String

    <DataMember()>
    Public Property exames As String

    <DataMember()>
    Public Property atendimentoId As Integer

    <DataMember()>
    Public Property historicoId As Integer

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, dataNasc As Date, cpf As Integer, sintomas As String, evolucao As String, receita As String, exames As String, atendimentoId As Integer)
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidade = especialidade
        Me.cpf = cpf
        Me.sintomas = sintomas
        Me.evolucao = evolucao
        Me.receita = receita
        Me.exames = exames
        Me.atendimentoId = atendimentoId
    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, dataNasc As Date, cpf As Integer, sintomas As String, evolucao As String, receita As String, exames As String, atendimentoId As Integer, historicoId As Integer)
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidade = especialidade
        Me.cpf = cpf
        Me.sintomas = sintomas
        Me.evolucao = evolucao
        Me.receita = receita
        Me.exames = exames
        Me.atendimentoId = atendimentoId
        Me.historicoId = historicoId
    End Sub

    Public Function SaveInfo() As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.historicoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Atendimento_Id", SqlDbType.Int, Me.atendimentoId))
            Parameters.Add(DataAccess.CreateInParam("@Sintomas", SqlDbType.VarChar, Me.sintomas))
            Parameters.Add(DataAccess.CreateInParam("@Evolucao", SqlDbType.VarChar, Me.evolucao))
            Parameters.Add(DataAccess.CreateInParam("@Receita", SqlDbType.VarChar, Me.receita))
            Parameters.Add(DataAccess.CreateInParam("@Exames", SqlDbType.VarChar, Me.exames))
            Parameters.Add(DataAccess.CreateInParam("@Historico_Id", SqlDbType.Int, Me.historicoId))

            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_SetHistorico", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

Public Class historicoList
    Inherits List(Of historico)

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
                    Me.Add(New historico(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11), info(12), info(13), info(14)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(Optional HistoricoID As Integer = 0, Optional UsuarioID As Integer = 0, Optional Tipo As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Historico_Id", SqlDbType.Int, HistoricoID))

            parametros.Add(DataAccess.CreateInParam("@Usuario_Id", SqlDbType.Int, UsuarioID))

            parametros.Add(DataAccess.CreateInParam("@Tipo", SqlDbType.Int, Tipo))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetHistorico", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New historico With {.historicoId = v(0), .atendimentoId = v(1), .data = v(2), .hora = v(3), .pacienteId = v(4), .nomePaciente = v(5), .medicoId = v(6), .nomeMedico = v(7), .especialidade = v(8), .dataNasc = v(9), .cpf = v(10), .sintomas = v(11), .evolucao = v(12), .receita = v(13), .exames = v(14)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class