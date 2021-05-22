﻿Imports System.Runtime.Serialization

<DataContract()>
Public Class medico

    <DataMember()>
    Public Property nome As String

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
    Public Property cpf As String

    <DataMember()>
    Public Property email As String

    <DataMember()>
    Public Property crm As String

    <DataMember()>
    Public Property especialidade As String

    <DataMember()>
    Public Property nomeEspecialidade As String

    <DataMember()>
    Public Property cep As String

    <DataMember()>
    Public Property tipo As String

    <DataMember()>
    Public Property medicoId As Integer

    <DataMember()>
    Public Property turnoSeg As String

    <DataMember()>
    Public Property turnoTer As String

    <DataMember()>
    Public Property turnoQua As String

    <DataMember()>
    Public Property turnoQui As String

    <DataMember()>
    Public Property turnoSex As String

    <DataMember()>
    Public Property password As String

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(nome As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String, cpf As String, email As String, crm As String, especialidade As String, nomeEspecialidade As String, cep As String, password As String, tipo As String, turnoSeg As String, turnoTer As String, turnoQua As String, turnoQui As String, turnoSex As String)
        Me.nome = nome
        Me.dataNasc = dataNasc
        Me.endereco = endereco
        Me.numero = numero
        Me.complemento = complemento
        Me.bairro = bairro
        Me.estado = estado
        Me.cidade = cidade
        Me.cpf = cpf
        Me.email = email
        Me.crm = crm
        Me.especialidade = especialidade
        Me.nomeEspecialidade = nomeEspecialidade
        Me.cep = cep
        Me.password = password
        Me.tipo = tipo
        Me.turnoSeg = turnoSeg
        Me.turnoTer = turnoTer
        Me.turnoQua = turnoQua
        Me.turnoQui = turnoQui
        Me.turnoSex = turnoSex
    End Sub

    Public Sub New(nome As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String, cpf As String, email As String, crm As String, especialidade As String, nomeEspecialidade As String, cep As String, password As String, tipo As String, turnoSeg As String, turnoTer As String, turnoQua As String, turnoQui As String, turnoSex As String, medicoId As Integer)
        Me.nome = nome
        Me.dataNasc = dataNasc
        Me.endereco = endereco
        Me.numero = numero
        Me.complemento = complemento
        Me.bairro = bairro
        Me.estado = estado
        Me.cidade = cidade
        Me.cpf = cpf
        Me.email = email
        Me.crm = crm
        Me.especialidade = especialidade
        Me.nomeEspecialidade = nomeEspecialidade
        Me.cep = cep
        Me.password = password
        Me.tipo = tipo
        Me.turnoSeg = turnoSeg
        Me.turnoTer = turnoTer
        Me.turnoQua = turnoQua
        Me.turnoQui = turnoQui
        Me.turnoSex = turnoSex
        Me.medicoId = medicoId
    End Sub

    Public Function SaveInfo() As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.nome) OrElse String.IsNullOrEmpty(Me.dataNasc) OrElse String.IsNullOrEmpty(Me.endereco) OrElse String.IsNullOrEmpty(Me.bairro) OrElse String.IsNullOrEmpty(Me.cpf) OrElse String.IsNullOrEmpty(Me.email) OrElse String.IsNullOrEmpty(Me.crm) OrElse String.IsNullOrEmpty(Me.especialidade) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Nome", SqlDbType.VarChar, Me.nome))
            Parameters.Add(DataAccess.CreateInParam("@DataNasc", SqlDbType.Date, Me.dataNasc))
            Parameters.Add(DataAccess.CreateInParam("@Endereco", SqlDbType.VarChar, Me.endereco))
            Parameters.Add(DataAccess.CreateInParam("@Numero", SqlDbType.VarChar, Me.numero))
            Parameters.Add(DataAccess.CreateInParam("@Complemento", SqlDbType.VarChar, Me.complemento))
            Parameters.Add(DataAccess.CreateInParam("@Bairro", SqlDbType.VarChar, Me.bairro))
            Parameters.Add(DataAccess.CreateInParam("@Estado", SqlDbType.VarChar, Me.estado))
            Parameters.Add(DataAccess.CreateInParam("@Cidade", SqlDbType.VarChar, Me.cidade))
            Parameters.Add(DataAccess.CreateInParam("@Cpf", SqlDbType.VarChar, Me.cpf))
            Parameters.Add(DataAccess.CreateInParam("@Email", SqlDbType.VarChar, Me.email))
            Parameters.Add(DataAccess.CreateInParam("@Crm", SqlDbType.Float, Me.crm))
            Parameters.Add(DataAccess.CreateInParam("@Especialidade", SqlDbType.VarChar, Me.especialidade))
            Parameters.Add(DataAccess.CreateInParam("@Cep", SqlDbType.VarChar, Me.cep))
            Parameters.Add(DataAccess.CreateInParam("@Password", SqlDbType.VarChar, Me.password))
            Parameters.Add(DataAccess.CreateInParam("@Seg", SqlDbType.VarChar, Me.turnoSeg))
            Parameters.Add(DataAccess.CreateInParam("@Ter", SqlDbType.VarChar, Me.turnoTer))
            Parameters.Add(DataAccess.CreateInParam("@Qua", SqlDbType.VarChar, Me.turnoQua))
            Parameters.Add(DataAccess.CreateInParam("@Qui", SqlDbType.VarChar, Me.turnoQui))
            Parameters.Add(DataAccess.CreateInParam("@Sex", SqlDbType.VarChar, Me.turnoSex))
            Parameters.Add(DataAccess.CreateInParam("@Medico_Id", SqlDbType.Int, Me.medicoId))

            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_SetMedico", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                If Not parRetorno Is Nothing And Not parRetorno.Value Is Nothing Then
                    retorno = parRetorno.Value
                    If retorno.ToUpper = "OK" Then
                        status = True

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

    Public Function DeleteInfo(MedicoID As Integer) As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.medicoId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Medico_Id", SqlDbType.Int, MedicoID))


            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_DeleteMedico", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

Public Class medicoList
    Inherits List(Of medico)

    Private _Loaded As Boolean
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Sub New()
    End Sub

    Public Sub New(Load As Boolean, Optional MedicoID As Integer = 0)
        If Load Then LoadInfo(MedicoID)
    End Sub

    Public Sub New(Protocolo As String)
        Try
            Dim protocoloSplit As String() = Protocolo.Split("#")

            For Each ps In protocoloSplit
                Dim info As String() = ps.Split("|")
                If info.Count = 3 Then
                    Me.Add(New medico(info(0), info(1), info(2), info(3), info(4), info(5), info(6), info(7), info(8), info(9), info(10), info(11), info(12), info(13), info(14), info(15), info(16), info(17), info(18), info(19), info(20)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(Optional MedicoID As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Medico_Id", SqlDbType.Int, MedicoID))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetMedico", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New medico With {.medicoId = v(0), .nome = v(1), .dataNasc = v(2), .endereco = v(3), .numero = v(4), .complemento = v(5), .bairro = v(6), .estado = v(7), .cidade = v(8), .cpf = v(9), .email = v(10), .crm = v(11), .especialidade = v(12), .nomeEspecialidade = v(13), .cep = v(14), .turnoSeg = v(15), .turnoTer = v(16), .turnoQua = v(17), .turnoQui = v(18), .turnoSex = v(18)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function GetComboList(Tipo As String, Optional Especialidade As Integer = 0, Optional Paciente As Integer = 0) As comboValueList
        Return Get_ComboList(Tipo, Especialidade, Paciente)
    End Function

End Class