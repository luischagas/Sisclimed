Imports System.Runtime.Serialization

<DataContract()>
Public Class relatorioAgendamento

    <DataMember()>
    Public Property dataInicial As Date

    <DataMember()>
    Public Property dataFinal As Date

    <DataMember()>
    Public Property data As Date

    <DataMember()>
    Public Property hora As String

    <DataMember()>
    Public Property pacienteId As Integer

    <DataMember()>
    Public Property nomePaciente As String

    <DataMember()>
    Public Property cpf As String

    <DataMember()>
    Public Property especialidadeId As Integer

    <DataMember()>
    Public Property especialidade As String

    <DataMember()>
    Public Property medicoId As Integer

    <DataMember()>
    Public Property nomeMedico As String

    <DataMember()>
    Public Property agendamentoId As Integer

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(dataInicial As Date, dataFinal As Date, data As Date, hora As String, pacienteId As Integer, nomePaciente As String, cpf As String, medicoId As Integer, nomeMedico As String, especialidadeId As Integer, especialidade As String)
        Me.dataInicial = dataInicial
        Me.dataFinal = dataFinal
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.cpf = cpf
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidadeId = especialidadeId
        Me.especialidade = especialidade
    End Sub

    Public Sub New(dataInicial As Date, dataFinal As Date, data As Date, hora As String, pacienteId As Integer, nomePaciente As String, cpf As String, medicoId As Integer, nomeMedico As String, especialidadeId As Integer, especialidade As String, agendamentoId As Integer)
        Me.dataInicial = dataInicial
        Me.dataFinal = dataFinal
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.cpf = cpf
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.especialidadeId = especialidadeId
        Me.especialidade = especialidade
        Me.agendamentoId = agendamentoId
    End Sub

End Class

Public Class relatorioAgendamentoList
    Inherits List(Of relatorioAgendamento)

    Private _Loaded As Boolean
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Sub New()
    End Sub

    Public Sub New(Load As Boolean, DataInicial As Date, DataFinal As Date, Optional Medico As Integer = 0, Optional Especialidade As Integer = 0, Optional Paciente As Integer = 0)
        If Load Then LoadInfo(DataInicial, DataFinal, Medico, Especialidade, Paciente)
    End Sub

    Public Sub New(Protocolo As String)
        Try
            Dim protocoloSplit As String() = Protocolo.Split("#")

            For Each ps In protocoloSplit
                Dim info As String() = ps.Split("|")
                If info.Count = 3 Then
                    Me.Add(New relatorioAgendamento(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(DataInicial As Date, DataFinal As Date, Optional Medico As Integer = 0, Optional Especialidade As Integer = 0, Optional Paciente As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@DataInicial", SqlDbType.Date, DataInicial))
            parametros.Add(DataAccess.CreateInParam("@DataFinal", SqlDbType.Date, DataFinal))
            parametros.Add(DataAccess.CreateInParam("@Medico", SqlDbType.Int, Medico))
            parametros.Add(DataAccess.CreateInParam("@Especialidade", SqlDbType.Int, Especialidade))
            parametros.Add(DataAccess.CreateInParam("@Paciente", SqlDbType.Int, Paciente))


            If DataAccess.execReaderProcedure("usp_Sisclimed_GetRelatorioAgendamento", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New relatorioAgendamento With {.agendamentoId = v(0), .data = v(1), .hora = v(2), .pacienteId = v(3), .nomePaciente = v(4), .cpf = v(5), .medicoId = v(6), .nomeMedico = v(7), .especialidadeId = v(8), .especialidade = v(9)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class