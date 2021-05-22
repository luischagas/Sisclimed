Imports System.Runtime.Serialization

<DataContract()>
Public Class relatorioAtendimento

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
    Public Property medicoId As Integer

    <DataMember()>
    Public Property nomeMedico As String

    <DataMember()>
    Public Property statusComparecimento As String

    <DataMember()>
    Public Property statusPagamento As String


    <DataMember()>
    Public Property valorPagamento As String

    <DataMember()>
    Public Property atendimentoId As Integer

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(dataInicial As Date, dataFinal As Date, data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, statusComparecimento As String, statusPagamento As String, valorPagamento As String)
        Me.dataInicial = dataInicial
        Me.dataFinal = dataFinal
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.statusComparecimento = statusComparecimento
        Me.statusPagamento = statusPagamento
        Me.valorPagamento = valorPagamento
    End Sub

    Public Sub New(dataInicial As Date, dataFinal As Date, data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, statusComparecimento As String, statusPagamento As String, valorPagamento As String, atendimentoId As Integer)
        Me.dataInicial = dataInicial
        Me.dataFinal = dataFinal
        Me.data = data
        Me.hora = hora
        Me.pacienteId = pacienteId
        Me.nomePaciente = nomePaciente
        Me.medicoId = medicoId
        Me.nomeMedico = nomeMedico
        Me.statusComparecimento = statusComparecimento
        Me.statusPagamento = statusPagamento
        Me.atendimentoId = atendimentoId
        Me.valorPagamento = valorPagamento
    End Sub

End Class

Public Class relatorioAtendimentoList
    Inherits List(Of relatorioAtendimento)

    Private _Loaded As Boolean
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Sub New()
    End Sub

    Public Sub New(Load As Boolean, DataInicial As Date, DataFinal As Date, Optional Medico As Integer = 0, Optional Paciente As Integer = 0, Optional statusComparecimento As String = "", Optional statusPagamento As String = "")
        If Load Then LoadInfo(DataInicial, DataFinal, Medico, Paciente, statusComparecimento, statusPagamento)
    End Sub

    Public Sub New(Protocolo As String)
        Try
            Dim protocoloSplit As String() = Protocolo.Split("#")

            For Each ps In protocoloSplit
                Dim info As String() = ps.Split("|")
                If info.Count = 3 Then
                    Me.Add(New relatorioAtendimento(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(DataInicial As Date, DataFinal As Date, Optional Medico As Integer = 0, Optional Paciente As Integer = 0, Optional statusComparecimento As String = "", Optional statusPagamento As String = "")
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@DataInicial", SqlDbType.Date, DataInicial))
            parametros.Add(DataAccess.CreateInParam("@DataFinal", SqlDbType.Date, DataFinal))
            parametros.Add(DataAccess.CreateInParam("@Medico", SqlDbType.Int, Medico))
            parametros.Add(DataAccess.CreateInParam("@Paciente", SqlDbType.Int, Paciente))
            parametros.Add(DataAccess.CreateInParam("@StatusComparecimento", SqlDbType.VarChar, statusComparecimento))
            parametros.Add(DataAccess.CreateInParam("@StatusPagamento", SqlDbType.VarChar, statusPagamento))


            If DataAccess.execReaderProcedure("usp_Sisclimed_GetRelatorioAtendimento", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New relatorioAtendimento With {.atendimentoId = v(0), .data = v(1), .hora = v(2), .pacienteId = v(3), .nomePaciente = v(4), .medicoId = v(5), .nomeMedico = v(6), .statusComparecimento = v(7), .statusPagamento = v(8), .valorPagamento = v(9)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class