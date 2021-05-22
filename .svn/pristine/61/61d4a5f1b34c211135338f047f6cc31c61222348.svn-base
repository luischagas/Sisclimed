Imports System.Runtime.Serialization

<DataContract()>
Public Class atendimento

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


    <DataMember()>
    Public Property atendimentoId As Integer

    <DataMember()>
    Public Property statusComparecimento As String

    <DataMember()>
    Public Property statusPagamento As String

    <DataMember()>
    Public Property valor As String

    <DataMember()>
    Public Property formapgto As String

    <DataMember()>
    Public Property convenio As Integer

    <DataMember()>
    Public Property numCartao As String

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, obs As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String, statusComparecimento As String, statusPagamento As String, agendamentoId As Integer, valor As String, formaPgto As String, convenio As Integer, numCartao As String)
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
        Me.statusComparecimento = statusComparecimento
        Me.statusPagamento = statusPagamento
        Me.agendamentoId = agendamentoId
        Me.valor = valor
        Me.formapgto = formaPgto
        Me.convenio = convenio
        Me.numCartao = numCartao
    End Sub

    Public Sub New(data As Date, hora As String, pacienteId As Integer, nomePaciente As String, medicoId As Integer, nomeMedico As String, especialidade As String, obs As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String, statusComparecimento As String, statusPagamento As String, agendamentoId As Integer, valor As String, formaPgto As String, convenio As Integer, numCartao As String, atendimentoId As Integer)
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
        Me.statusComparecimento = statusComparecimento
        Me.statusPagamento = statusPagamento
        Me.agendamentoId = agendamentoId
        Me.valor = valor
        Me.convenio = convenio
        Me.numCartao = numCartao
        Me.atendimentoId = atendimentoId
    End Sub

    Public Function SaveInfo() As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.agendamentoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Agendamento_Id", SqlDbType.Int, Me.agendamentoId))
            Parameters.Add(DataAccess.CreateInParam("@StatusComparecimento", SqlDbType.VarChar, Me.statusComparecimento))
            Parameters.Add(DataAccess.CreateInParam("@Paciente_Id", SqlDbType.Int, Me.pacienteId))
            Parameters.Add(DataAccess.CreateInParam("@Valor", SqlDbType.Float, Me.valor))
            Parameters.Add(DataAccess.CreateInParam("@FormaPgto", SqlDbType.VarChar, Me.formapgto))
            Parameters.Add(DataAccess.CreateInParam("@ConvenioId", SqlDbType.Int, Me.convenio))
            Parameters.Add(DataAccess.CreateInParam("@NumCartao", SqlDbType.VarChar, Me.numCartao))
            Parameters.Add(DataAccess.CreateInParam("@Atendimento_Id", SqlDbType.Int, Me.atendimentoId))

            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_SetAtendimento", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

        If String.IsNullOrEmpty(Me.atendimentoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Atendimento_Id", SqlDbType.Int, ExameID))


            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_DeleteAtendimento", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

Public Class atendimentoList
    Inherits List(Of atendimento)

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
                    Me.Add(New atendimento(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11), info(12), info(13), info(14), info(15), info(16), info(17), info(18), info(19), info(20), info(21), info(22)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(Optional AtendimentoID As Integer = 0, Optional UsuarioID As Integer = 0, Optional Tipo As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Atendimento_Id", SqlDbType.Int, AtendimentoID))

            parametros.Add(DataAccess.CreateInParam("@Usuario_Id", SqlDbType.Int, UsuarioID))

            parametros.Add(DataAccess.CreateInParam("@Tipo", SqlDbType.Int, Tipo))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetAtendimento", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New atendimento With {.atendimentoId = v(0), .agendamentoId = v(1), .data = v(2), .hora = v(3), .pacienteId = v(4), .nomePaciente = v(5), .medicoId = v(6), .nomeMedico = v(7), .especialidade = v(8), .obs = v(9), .dataNasc = v(10), .endereco = v(11), .numero = v(12), .complemento = v(13), .bairro = v(14), .estado = v(15), .cidade = v(16), .statusComparecimento = v(17), .statusPagamento = v(18), .valor = v(19), .formapgto = v(20), .numCartao = v(21), .convenio = v(22)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class