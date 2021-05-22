Imports System.Runtime.Serialization

<DataContract()>
Public Class convenio

    <DataMember()>
    Public Property nome As String

    <DataMember()>
    Public Property tipo As String

    <DataMember()>
    Public Property convenioId As Integer

    Public status As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(nome As String, tipo As String)
        Me.nome = nome
        Me.tipo = tipo
    End Sub

    Public Sub New(nome As String, tipo As String, convenioId As Integer)
        Me.nome = nome
        Me.tipo = tipo
        Me.convenioId = convenioId
    End Sub

    Public Function SaveInfo() As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.nome) OrElse String.IsNullOrEmpty(Me.tipo) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Nome", SqlDbType.VarChar, Me.nome))
            Parameters.Add(DataAccess.CreateInParam("@Tipo", SqlDbType.VarChar, Me.tipo))
            Parameters.Add(DataAccess.CreateInParam("@Convenio_Id", SqlDbType.Int, Me.convenioId))

            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_SetConvenio", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

    Public Function DeleteInfo(ConvenioID As Integer) As String
        Dim retorno As String = ""

        If String.IsNullOrEmpty(Me.convenioId) Then Return retorno

        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter)
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@Convenio_Id", SqlDbType.Int, ConvenioID))


            Dim parRetorno As IDbDataParameter = DataAccess.CreateOutPutParam("@Resp", SqlDbType.VarChar, 4000)

            Parameters.Add(parRetorno)

            If DataAccess.execReaderProcedure("usp_Sisclimed_DeleteConvenio", Parameters, Vals) AndAlso Not Vals Is Nothing Then
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

Public Class convenioList
    Inherits List(Of convenio)

    Private _Loaded As Boolean
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Sub New()
    End Sub

    Public Sub New(Load As Boolean, Optional ConvenioID As Integer = 0)
        If Load Then LoadInfo(ConvenioID)
    End Sub

    Public Sub New(Protocolo As String)
        Try
            Dim protocoloSplit As String() = Protocolo.Split("#")

            For Each ps In protocoloSplit
                Dim info As String() = ps.Split("|")
                If info.Count = 3 Then
                    Me.Add(New convenio(info(0), info(1), info(2)))
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadInfo(Optional ConvenioID As Integer = 0)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Convenio_Id", SqlDbType.Int, ConvenioID))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetConvenio", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New convenio With {.convenioId = v(0), .nome = v(1), .tipo = v(2)})
                Next
                Me._Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class